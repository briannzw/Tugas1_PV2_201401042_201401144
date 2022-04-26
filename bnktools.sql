-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 18 Des 2021 pada 15.39
-- Versi server: 10.4.18-MariaDB
-- Versi PHP: 8.0.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bnktools`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `transaksi`
--

CREATE TABLE `transaksi` (
  `id` int(10) NOT NULL,
  `kode` varchar(20) NOT NULL,
  `total` double(30,10) NOT NULL,
  `currency` varchar(10) NOT NULL,
  `buy` double(30,10) NOT NULL,
  `sell` double(30,10) NOT NULL,
  `date` datetime NOT NULL,
  `profit` double(30,10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `transaksi`
--

INSERT INTO `transaksi` (`id`, `kode`, `total`, `currency`, `buy`, `sell`, `date`, `profit`) VALUES
(1, 'GTC', 19.4180000000, 'USDT', 10.2200000000, 11.2620000000, '2021-12-17 00:00:00', 10.1956947162),
(2, 'CHESS', 21.1200000000, 'USDT', 1.9200000000, 1.9220000000, '2021-12-18 00:00:00', 0.1041666667),
(3, 'QI', 19.8730000000, 'USDT', 0.1670000000, 0.1682000000, '2021-12-18 00:00:00', 0.7185628743),
(4, 'SAND', 31.0200000000, 'USDT', 5.1700000000, 5.1156000000, '2021-12-17 00:00:00', -1.0522243714);

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `transaksi`
--
ALTER TABLE `transaksi`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `transaksi`
--
ALTER TABLE `transaksi`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
