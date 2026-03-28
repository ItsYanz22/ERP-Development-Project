CREATE DATABASE IF NOT EXISTS `HierarchicalItemDb` CHARACTER SET utf8mb4;
USE `HierarchicalItemDb`;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;
ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Items` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Weight` decimal(18,2) NOT NULL,
    `ParentId` int NULL,
    `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Items` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Items_Items_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `Items` (`Id`) ON DELETE RESTRICT
) CHARACTER SET=utf8mb4;

CREATE TABLE `Users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
    `PasswordHash` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `Users` (`Id`, `Email`, `PasswordHash`)
VALUES (1, 'test@user.com', 'pass');

CREATE INDEX `IX_Items_ParentId` ON `Items` (`ParentId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260324183750_InitialCreate', '9.0.2');

INSERT INTO `Items` (`Id`, `Name`, `Weight`, `ParentId`, `Status`, `CreatedAt`)
VALUES 
(1, 'Raw Steel Block', 150.00, NULL, 'Unprocessed', '2026-03-24 18:37:50'),
(2, 'Wooden Log', 45.50, NULL, 'Unprocessed', '2026-03-24 18:37:50');

COMMIT;

