-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost
-- Généré le : mar. 18 jan. 2022 à 01:17
-- Version du serveur : 10.4.21-MariaDB
-- Version de PHP : 8.0.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `Projet_Individuel`
--

-- --------------------------------------------------------

--
-- Structure de la table `admin`
--

CREATE TABLE `admin` (
  `Id_Admin` int(11) NOT NULL,
  `password` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `admin`
--

INSERT INTO `admin` (`Id_Admin`, `password`) VALUES
(1, 'password');

-- --------------------------------------------------------

--
-- Structure de la table `employee`
--

CREATE TABLE `employee` (
  `Id_Employee` int(11) NOT NULL,
  `firstname` varchar(50) DEFAULT NULL,
  `lastname` varchar(50) DEFAULT NULL,
  `phone_number` varchar(10) DEFAULT NULL,
  `id_Site_FK` int(11) DEFAULT NULL,
  `Id_Service_FK` int(11) DEFAULT NULL,
  `mail` varchar(50) DEFAULT NULL,
  `cellphone_number` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `employee`
--

INSERT INTO `employee` (`Id_Employee`, `firstname`, `lastname`, `phone_number`, `id_Site_FK`, `Id_Service_FK`, `mail`, `cellphone_number`) VALUES
(1, 'Valentin', 'Verin', '0606060606', 3, 2, 'valentin.verin@viacesi.fr', '0327272727'),
(2, 'François', 'Dupont', '0707070707', 4, 1, 'francois.dupont@viacesi.fr', '0327030303'),
(4, 'patrick', 'coquelle', '0607060706', 5, 1, 'patrick.coquelle@viacesi.fr', '0303272727'),
(7, 'Iron', 'Man', '0607060707', 8, 4, 'iron.man@avengers.fr', '0327032727');

-- --------------------------------------------------------

--
-- Structure de la table `service`
--

CREATE TABLE `service` (
  `Id_Service` int(11) NOT NULL,
  `service_name` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `service`
--

INSERT INTO `service` (`Id_Service`, `service_name`) VALUES
(7, 'Accueil'),
(5, 'Commercial'),
(1, 'Comptabilité'),
(4, 'Informatique'),
(2, 'Production');

-- --------------------------------------------------------

--
-- Structure de la table `site`
--

CREATE TABLE `site` (
  `Id_site` int(11) NOT NULL,
  `site_city` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `site`
--

INSERT INTO `site` (`Id_site`, `site_city`) VALUES
(9, 'Bruxelles'),
(33, 'Cambrai'),
(5, 'Lille'),
(8, 'Marseille'),
(2, 'Nantes'),
(4, 'Nice'),
(1, 'Paris'),
(3, 'Toulouse');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`Id_Admin`);

--
-- Index pour la table `employee`
--
ALTER TABLE `employee`
  ADD PRIMARY KEY (`Id_Employee`),
  ADD UNIQUE KEY `phone_number` (`phone_number`),
  ADD UNIQUE KEY `mail` (`mail`),
  ADD UNIQUE KEY `cellphone_number` (`cellphone_number`),
  ADD KEY `Id_Service_FK` (`Id_Service_FK`),
  ADD KEY `id_Site_FK` (`id_Site_FK`);

--
-- Index pour la table `service`
--
ALTER TABLE `service`
  ADD PRIMARY KEY (`Id_Service`),
  ADD UNIQUE KEY `service_name` (`service_name`);

--
-- Index pour la table `site`
--
ALTER TABLE `site`
  ADD PRIMARY KEY (`Id_site`),
  ADD UNIQUE KEY `site_city` (`site_city`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `admin`
--
ALTER TABLE `admin`
  MODIFY `Id_Admin` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT pour la table `employee`
--
ALTER TABLE `employee`
  MODIFY `Id_Employee` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT pour la table `service`
--
ALTER TABLE `service`
  MODIFY `Id_Service` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT pour la table `site`
--
ALTER TABLE `site`
  MODIFY `Id_site` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=42;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `employee`
--
ALTER TABLE `employee`
  ADD CONSTRAINT `employee_ibfk_1` FOREIGN KEY (`Id_Service_FK`) REFERENCES `Service` (`Id_Service`),
  ADD CONSTRAINT `employee_ibfk_2` FOREIGN KEY (`id_Site_FK`) REFERENCES `Site` (`Id_Site`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
