using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeRegistrationApp
{
    public partial class MainWindow : Window
    {
        // Backing list — sumber kebenaran data pegawai (terpisah dari ListBox)
        private readonly List<string> _allEmployees = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        // ─── Simpan ────────────────────────────────────────────────────────────
        private void BtnSimpan_Click(object sender, RoutedEventArgs e)
        {
            // --- Validasi NIK ---
            if (string.IsNullOrWhiteSpace(txtNik.Text))
            {
                MessageBox.Show("NIK harus diisi!", "Peringatan",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNik.Focus();
                return;
            }

            if (txtNik.Text.Length < 5 || txtNik.Text.Length > 20)
            {
                MessageBox.Show("NIK harus terdiri dari 5 hingga 20 karakter!", "Peringatan",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNik.Focus();
                return;
            }

            if (!IsAllDigits(txtNik.Text))
            {
                MessageBox.Show("NIK hanya boleh berisi angka!", "Peringatan",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNik.Focus();
                return;
            }

            // --- Validasi Nama ---
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Nama harus diisi!", "Peringatan",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNama.Focus();
                return;
            }

            if (txtNama.Text.Trim().Length < 3)
            {
                MessageBox.Show("Nama harus terdiri dari minimal 3 karakter!", "Peringatan",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNama.Focus();
                return;
            }

            // --- Validasi Departemen ---
            if (cmbDepartemen.SelectedItem == null)
            {
                MessageBox.Show("Pilih departemen!", "Peringatan",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbDepartemen.Focus();
                return;
            }

            // --- Validasi Jenis Kelamin ---
            if (rbLaki.IsChecked != true && rbPerempuan.IsChecked != true)
            {
                MessageBox.Show("Pilih jenis kelamin!", "Peringatan",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // --- Cek NIK duplikat (di backing list) ---
            string nikBaru = txtNik.Text.Trim();
            foreach (string existingData in _allEmployees)
            {
                string[] parts = existingData.Split('|');
                if (parts.Length > 0 && parts[0].Trim() == nikBaru)
                {
                    MessageBox.Show($"NIK {nikBaru} sudah terdaftar!", "Peringatan",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNik.Focus();
                    return;
                }
            }

            // --- Proses Penyimpanan ---
            string nik  = nikBaru;
            string nama = txtNama.Text.Trim();

            string departemen = string.Empty;
            if (cmbDepartemen.SelectedItem is ComboBoxItem selectedItem)
                departemen = selectedItem.Content?.ToString() ?? string.Empty;

            string jenisKelamin = rbLaki.IsChecked == true ? "Laki-laki" : "Perempuan";

            string data = $"{nik} | {nama} | {departemen} | {jenisKelamin}";

            // Simpan ke backing list lalu perbarui tampilan ListBox
            _allEmployees.Add(data);
            ApplySearch();

            // Tampilkan ringkasan data yang disimpan
            var sb = new StringBuilder();
            sb.AppendLine("Data pegawai berhasil disimpan!");
            sb.AppendLine();
            sb.AppendLine($"NIK          : {nik}");
            sb.AppendLine($"Nama         : {nama}");
            sb.AppendLine($"Departemen   : {departemen}");
            sb.AppendLine($"Jenis Kelamin: {jenisKelamin}");

            MessageBox.Show(sb.ToString(), "Informasi",
                MessageBoxButton.OK, MessageBoxImage.Information);

            // Reset form setelah simpan berhasil
            ResetForm();
        }

        // ─── Reset ─────────────────────────────────────────────────────────────
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
        }

        // ─── Hapus ─────────────────────────────────────────────────────────────
        private void BtnHapus_Click(object sender, RoutedEventArgs e)
        {
            if (lstPegawai.SelectedItem == null)
            {
                MessageBox.Show("Pilih data yang ingin dihapus!", "Peringatan",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedData = lstPegawai.SelectedItem.ToString() ?? string.Empty;
            MessageBoxResult konfirmasi = MessageBox.Show(
                $"Yakin ingin menghapus data berikut?\n\n{selectedData}",
                "Konfirmasi Hapus",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (konfirmasi == MessageBoxResult.Yes)
            {
                // Hapus dari backing list, lalu perbarui ListBox
                _allEmployees.Remove(selectedData);
                ApplySearch();

                MessageBox.Show("Data berhasil dihapus.", "Informasi",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // ─── Search ────────────────────────────────────────────────────────────

        /// <summary>
        /// Dipanggil setiap kali teks di txtSearch berubah.
        /// Memfilter ListBox secara real-time berdasarkan kata kunci.
        /// </summary>
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplySearch();
        }

        /// <summary>
        /// Membersihkan kotak pencarian dan menampilkan kembali semua data.
        /// </summary>
        private void BtnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            txtSearch.Focus();
        }

        /// <summary>
        /// Memfilter isi ListBox berdasarkan kata kunci di txtSearch.
        /// Pencarian bersifat case-insensitive dan mencakup semua kolom
        /// (NIK, Nama, Departemen, Jenis Kelamin).
        /// </summary>
        private void ApplySearch()
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            lstPegawai.Items.Clear();

            if (string.IsNullOrEmpty(keyword))
            {
                // Tampilkan semua data
                foreach (string emp in _allEmployees)
                    lstPegawai.Items.Add(emp);

                lblSearchInfo.Text = _allEmployees.Count > 0
                    ? $"Total: {_allEmployees.Count} pegawai"
                    : string.Empty;
            }
            else
            {
                // Tampilkan hanya yang cocok
                int count = 0;
                foreach (string emp in _allEmployees)
                {
                    if (emp.ToLower().Contains(keyword))
                    {
                        lstPegawai.Items.Add(emp);
                        count++;
                    }
                }

                lblSearchInfo.Text = count > 0
                    ? $"Ditemukan: {count} dari {_allEmployees.Count} pegawai"
                    : $"Tidak ada pegawai yang cocok dengan \"{txtSearch.Text.Trim()}\"";
            }
        }

        // ─── Helper Methods ────────────────────────────────────────────────────

        /// <summary>
        /// Mereset seluruh input form ke kondisi awal.
        /// </summary>
        private void ResetForm()
        {
            txtNik.Clear();
            txtNama.Clear();
            cmbDepartemen.SelectedIndex = -1;
            rbLaki.IsChecked           = false;
            rbPerempuan.IsChecked      = false;
            lstPegawai.SelectedIndex   = -1;
            txtNik.Focus();
        }

        /// <summary>
        /// Memeriksa apakah seluruh karakter dalam string adalah digit angka.
        /// </summary>
        private static bool IsAllDigits(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }
    }
}