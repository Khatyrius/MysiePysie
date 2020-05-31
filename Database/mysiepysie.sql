-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Wersja serwera:               8.0.18 - MySQL Community Server - GPL
-- Serwer OS:                    Win64
-- HeidiSQL Wersja:              10.2.0.5599
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Zrzut struktury bazy danych mysiepysie2
CREATE DATABASE IF NOT EXISTS `mysiepysie2` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `mysiepysie2`;

-- Zrzut struktury tabela mysiepysie2.classes
CREATE TABLE IF NOT EXISTS `classes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Zrzucanie danych dla tabeli mysiepysie2.classes: ~3 rows (około)
/*!40000 ALTER TABLE `classes` DISABLE KEYS */;
REPLACE INTO `classes` (`id`, `name`) VALUES
	(10, 'Test Class'),
	(14, 'ProKlass 2'),
	(15, 'Test');
/*!40000 ALTER TABLE `classes` ENABLE KEYS */;

-- Zrzut struktury tabela mysiepysie2.students
CREATE TABLE IF NOT EXISTS `students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `forename` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `surname` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `age` int(11) NOT NULL,
  `status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `classid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_Students_classid` (`classid`),
  CONSTRAINT `FK_Students_Classes_classid` FOREIGN KEY (`classid`) REFERENCES `classes` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Zrzucanie danych dla tabeli mysiepysie2.students: ~5 rows (około)
/*!40000 ALTER TABLE `students` DISABLE KEYS */;
REPLACE INTO `students` (`id`, `forename`, `surname`, `age`, `status`, `classid`) VALUES
	(6, 'Kuba', 'Wojewodzki', 50, 'Show maker', 14),
	(13, 'Bartosz', 'Musielak', 23, 'Still coding', 14),
	(14, 'Testus', 'Niemyślikus', 21, 'Pali', 15),
	(15, 'Test', 'Test', 1, 'Test', NULL),
	(16, 'Dariusz', 'Fazidar', 15, 'Drawing', 15);
/*!40000 ALTER TABLE `students` ENABLE KEYS */;

-- Zrzut struktury tabela mysiepysie2.subjects
CREATE TABLE IF NOT EXISTS `subjects` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ECTSpoints` int(11) NOT NULL,
  `teacherid` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_Subjects_teacherid` (`teacherid`),
  CONSTRAINT `FK_Subjects_Teachers_teacherid` FOREIGN KEY (`teacherid`) REFERENCES `teachers` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Zrzucanie danych dla tabeli mysiepysie2.subjects: ~3 rows (około)
/*!40000 ALTER TABLE `subjects` DISABLE KEYS */;
REPLACE INTO `subjects` (`id`, `name`, `ECTSpoints`, `teacherid`) VALUES
	(1, 'Informatyka', 20, 1),
	(24, 'Muzyka', 5, 2),
	(26, 'Programowanie', 20, 13);
/*!40000 ALTER TABLE `subjects` ENABLE KEYS */;

-- Zrzut struktury tabela mysiepysie2.teachers
CREATE TABLE IF NOT EXISTS `teachers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `forename` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `surname` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `age` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Zrzucanie danych dla tabeli mysiepysie2.teachers: ~6 rows (około)
/*!40000 ALTER TABLE `teachers` DISABLE KEYS */;
REPLACE INTO `teachers` (`id`, `forename`, `surname`, `age`) VALUES
	(1, 'Bartosz', 'Musielak', 23),
	(2, 'Mateusz', 'Zmuda', 23),
	(3, 'Maciej', 'Sabik', 22),
	(13, 'Pioter', 'Powroznikus', 41),
	(15, 'Pawel', 'Osiwala', 13),
	(36, 'Testus', 'Myślikus', 16);
/*!40000 ALTER TABLE `teachers` ENABLE KEYS */;

-- Zrzut struktury tabela mysiepysie2.users
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `firstname` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `lastname` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `phone` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `userStatus` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Zrzucanie danych dla tabeli mysiepysie2.users: ~2 rows (około)
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
REPLACE INTO `users` (`id`, `username`, `firstname`, `lastname`, `email`, `password`, `phone`, `userStatus`) VALUES
	(1, 'KhatAdmin', 'Bartosz', 'Musielak', 'bartosz_musielak@hotmail.com', 'SilneHasl@12', '506064938', 666),
	(2, 'dampero', 'Staszek', 'Tester', 'dampero18@hotmail.com', 'SilneHasl@1', NULL, 0);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

-- Zrzut struktury tabela mysiepysie2.__efmigrationshistory
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Zrzucanie danych dla tabeli mysiepysie2.__efmigrationshistory: ~3 rows (około)
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
REPLACE INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20200519044141_FirstMigration', '3.1.4'),
	('20200522084358_SecondMigration', '3.1.4'),
	('20200522111754_classesMigration', '3.1.4');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
