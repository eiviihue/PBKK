# Dokumentasi Pertemuan 3

## CalculatorApp
Project ini merupakan aplikasi kalkulator yang dibuat dengan menggunakan Windows Forms (.NET framework). Aplikasi ini dibuat untuk melakukan operasi aritmatika dasar, dilengkapi dengan tombol angka, tombol operator, tombol titik desimal, fitur reset, serta penanganan kesalahan (*error handling*).

Aplikasi ini memiliki beberapa fitur dan alur kerja yang dapat dijalankan, yaitu:

### 1. Tampilan Utama Kalkulator
Menampilkan antarmuka kalkulator saat pertama kali dijalankan dengan layar *display* berangka awal "0", susunan tombol digit angka (0–9), tombol operasi aritmatika (+, −, ×, ÷), tombol titik desimal (.), tombol *Clear* (C), dan tombol sama dengan (=).
![Calc UI](image.png)

### 2. Input Angka dan Desimal
Memungkinkan pengguna memasukkan kombinasi angka. Terdapat pula tombol titik desimal (.) untuk menginput angka pecahan dengan validasi otomatis agar tanda desimal tidak dapat dimasukkan berulang kali pada angka yang sama.
![input angka & decimal](image-1.png)

### 3. Operasi Penjumlahan (+)
Melakukan perhitungan penjumlahan antara dua bilangan ketika pengguna menekan tombol operator tambah (+) dan menekan tombol sama dengan (=) untuk menampilkan hasilnya.
<video controls src="20260923-1623-43.2861931.mp4" title="Operasi Penjumlahan"></video>

### 4. Operasi Pengurangan (−)
Melakukan operasi pengurangan antara bilangan pertama dan bilangan kedua yang kemudian hasilnya langsung ditampilkan pada layar *display*.
<video controls src="20260923-1635-51.6664629.mp4" title="Operasi Pengurangan"></video>

### 5. Operasi Perkalian (×)
Melakukan perhitungan perkalian antara dua bilangan dan menampilkan hasil kalkulasi secara akurat.
<video controls src="20260923-1632-04.5255166.mp4" title="Operasi Perkalian"></video>

### 6. Operasi Pembagian (÷)
Melakukan pembagian bilangan pertama terhadap bilangan kedua dan menampilkan nilai hasilnya pada layar.
<video controls src="20260923-1632-57.9511297.mp4" title="Operasi Pembagian"></video>

### 7. Penanganan Error Pembagian Nol (*Divide by Zero*)
Sistem dilengkapi dengan penanganan eksepsi (*try-catch*). Jika pengguna mencoba membagi suatu bilangan dengan angka nol (0), aplikasi akan menangkap `DivideByZeroException` dan menampilkan kotak dialog pesan error (*MessageBox*) tanpa membuat aplikasi keluar/crash.
<video controls src="20260923-1634-07.8705386.mp4" title="Error Pembagian Nol"></video>

### 8. Fitur Reset / Clear (C)
Mengembalikan kalkulator ke kondisi awal dengan mereset seluruh variabel hitung (*firstNumber*, *secondNumber*, *result*, dan jenis operasi) serta mengembalikan tampilan layar *display* ke nilai "0".
<video controls src="20260923-1634-57.3000233.mp4" title="Clear"></video>

### Refleksi Mahasiswa:

1. Apa fungsi object sender pada event handler?
Parameter object sender merepresentasikan kontrol antarmuka (dalam hal ini, sebuah Button) yang memicu atau memanggil event tersebut. Pada kode kalkulator, sender memungkinkan aplikasi mengidentifikasi secara spesifik tombol mana yang sedang diklik oleh pengguna dengan melakukan casting objek menjadi (Button)sender.

2. Mengapa semua tombol angka dapat memakai satu NumberButton_Click?
Semua tombol angka memiliki logika yang identik, yaitu menambahkan teks (angka) dari tombol tersebut ke dalam layar tampilan txtDisplay. Dengan menggunakan object sender untuk mengekstrak nilai teks (0-9) dari tombol yang memicu event, Anda menghindari penulisan ulang kode yang sama untuk 10 tombol berbeda.

3. Apa perbedaan firstNumber, secondNumber, dan result?
firstNumber: Menyimpan angka pertama yang dimasukkan oleh pengguna sebelum menekan tombol operator.
secondNumber: Menyimpan angka kedua yang diekstrak dari layar saat tombol sama dengan (=) ditekan.
result: Menyimpan hasil kalkulasi aritmatika antara firstNumber dan secondNumber berdasarkan operator yang dipilih.

4. Mengapa pembagian dengan nol perlu divalidasi?
Dalam pemrograman C#, operasi pembagian dengan nilai nol tidak terdefinisi dan secara otomatis akan memicu runtime error yang disebut DivideByZeroException. Jika tidak divalidasi dengan melempar exception atau memberikan peringatan, operasi ini akan membuat aplikasi mengalami kegagalan sistem (crash).

5. Bagaimana try-catch membantu menjaga aplikasi tetap stabil?
Blok try-catch melindungi aplikasi dari crash saat terjadi kesalahan fatal (seperti pembagian dengan nol atau format input yang salah) selama eksekusi di blok try. Alih-alih aplikasi tertutup paksa, blok catch akan menangkap exception tersebut dan menginstruksikan program untuk tetap berjalan sembari menampilkan pesan kesalahan (error message) kepada pengguna melalui MessageBox.
