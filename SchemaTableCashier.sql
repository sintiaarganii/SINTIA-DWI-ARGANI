<?xml version="1.0" encoding="UTF-8"?>
<schemadesigner version="6.5">
<source>
<database charset="utf8mb4" collation="utf8mb4_general_ci">cashier</database>
</source>
<canvas zoom="100">
<tables>
<table name="auth" view="colnames">
<left>61</left>
<top>52</top>
<width>128</width>
<height>214</height>
<sql_create_table>CREATE TABLE `auth` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Salt` longtext NOT NULL,
  `PasswordHash` longtext NOT NULL,
  `Role` longtext NOT NULL,
  `CashierStatus` int(11) NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci</sql_create_table>
</table>
<table name="cashiers" view="colnames">
<left>233</left>
<top>50</top>
<width>112</width>
<height>197</height>
<sql_create_table>CREATE TABLE `cashiers` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Username` longtext NOT NULL,
  `salt` longtext NOT NULL,
  `pwd_hash` longtext NOT NULL,
  `AccessDate` datetime(6) NOT NULL,
  `Status` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci</sql_create_table>
</table>
<table name="categoris" view="colnames">
<left>372</left>
<top>49</top>
<width>131</width>
<height>129</height>
<sql_create_table>CREATE TABLE `categoris` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CategoriName` varchar(100) NOT NULL,
  `StatusCategori` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci</sql_create_table>
</table>
<table name="orderdetails" view="colnames">
<left>379</left>
<top>300</top>
<width>101</width>
<height>163</height>
<sql_create_table>CREATE TABLE `orderdetails` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OrderId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `UnitPrice` decimal(65,30) NOT NULL DEFAULT 0.000000000000000000000000000000,
  PRIMARY KEY (`Id`),
  KEY `IX_OrderDetails_OrderId` (`OrderId`),
  KEY `IX_OrderDetails_ProductId` (`ProductId`),
  CONSTRAINT `FK_OrderDetails_Orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_OrderDetails_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci</sql_create_table>
</table>
<table name="products" view="colnames">
<left>639</left>
<top>29</top>
<width>125</width>
<height>197</height>
<sql_create_table>CREATE TABLE `products` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NameProduct` varchar(100) NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  `Stock` int(11) NOT NULL,
  `Description` varchar(500) NOT NULL,
  `IdCategori` int(11) NOT NULL,
  `StatusProduct` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Products_IdCategori` (`IdCategori`),
  CONSTRAINT `FK_Products_Categoris_IdCategori` FOREIGN KEY (`IdCategori`) REFERENCES `categoris` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci</sql_create_table>
</table>
<table name="orders" view="colnames">
<left>643</left>
<top>286</top>
<width>135</width>
<height>180</height>
<sql_create_table>CREATE TABLE `orders` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OrderDate` datetime(6) NOT NULL,
  `CustomerName` longtext NOT NULL,
  `TotalPrice` decimal(65,30) NOT NULL DEFAULT 0.000000000000000000000000000000,
  `CashierId` longtext NOT NULL,
  `CashierId1` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `IX_Orders_CashierId1` (`CashierId1`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci</sql_create_table>
</table>
</tables>
</canvas>
</schemadesigner>