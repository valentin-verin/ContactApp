-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost
-- Généré le : mer. 15 déc. 2021 à 14:26
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
-- Structure de la table `Admin`
--

CREATE TABLE `Admin` (
  `Id_Admin` int(11) NOT NULL,
  `password` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `Employee`
--

CREATE TABLE `Employee` (
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
-- Déchargement des données de la table `Employee`
--

INSERT INTO `Employee` (`Id_Employee`, `firstname`, `lastname`, `phone_number`, `id_Site_FK`, `Id_Service_FK`, `mail`, `cellphone_number`) VALUES
(1, 'Valentin', 'Verin', '0606060606', 3, 2, 'valentin.verin@viacesi.fr', '0327272727');

-- --------------------------------------------------------

--
-- Structure de la table `Service`
--

CREATE TABLE `Service` (
  `Id_Service` int(11) NOT NULL,
  `service_name` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `Service`
--

INSERT INTO `Service` (`Id_Service`, `service_name`) VALUES
(3, 'accueil'),
(5, 'commercial'),
(1, 'Comptabilité'),
(4, 'informatique'),
(2, 'production');

-- --------------------------------------------------------

--
-- Structure de la table `Site`
--

CREATE TABLE `Site` (
  `Id_Site` int(11) NOT NULL,
  `site_city` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `Site`
--

INSERT INTO `Site` (`Id_Site`, `site_city`) VALUES
(5, 'Lille'),
(2, 'Nantes'),
(4, 'Nice'),
(1, 'Paris'),
(3, 'Toulouse');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `Admin`
--
ALTER TABLE `Admin`
  ADD PRIMARY KEY (`Id_Admin`);

--
-- Index pour la table `Employee`
--
ALTER TABLE `Employee`
  ADD PRIMARY KEY (`Id_Employee`),
  ADD UNIQUE KEY `phone_number` (`phone_number`),
  ADD UNIQUE KEY `mail` (`mail`),
  ADD UNIQUE KEY `cellphone_number` (`cellphone_number`),
  ADD KEY `Id_Service_FK` (`Id_Service_FK`),
  ADD KEY `id_Site_FK` (`id_Site_FK`);

--
-- Index pour la table `Service`
--
ALTER TABLE `Service`
  ADD PRIMARY KEY (`Id_Service`),
  ADD UNIQUE KEY `service_name` (`service_name`);

--
-- Index pour la table `Site`
--
ALTER TABLE `Site`
  ADD PRIMARY KEY (`Id_Site`),
  ADD UNIQUE KEY `site_city` (`site_city`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `Admin`
--
ALTER TABLE `Admin`
  MODIFY `Id_Admin` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `Employee`
--
ALTER TABLE `Employee`
  MODIFY `Id_Employee` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT pour la table `Service`
--
ALTER TABLE `Service`
  MODIFY `Id_Service` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT pour la table `Site`
--
ALTER TABLE `Site`
  MODIFY `Id_Site` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `Employee`
--
ALTER TABLE `Employee`
  ADD CONSTRAINT `employee_ibfk_1` FOREIGN KEY (`Id_Service_FK`) REFERENCES `Service` (`Id_Service`),
  ADD CONSTRAINT `employee_ibfk_2` FOREIGN KEY (`id_Site_FK`) REFERENCES `Site` (`Id_Site`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
