DROP Database EmployeeManagement

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'EmployeeManagement')
CREATE DATABASE EmployeeManagement;
GO
USE EmployeeManagement
GO

--dropping tables
IF OBJECT_ID('tblEmployees') IS NOT NULL 
DROP TABLE tblEmployees;

IF OBJECT_ID('tblManagers') IS NOT NULL 
DROP TABLE tblManagers;

IF OBJECT_ID('tblReports') IS NOT NULL 
DROP TABLE tblReports;

IF OBJECT_ID('vwReports') IS NOT NULL 
DROP VIEW vwReports

--creating tables
CREATE TABLE tblEmployees(
	EmployeeId INT PRIMARY KEY IDENTITY(1,1),
	FirstName VARCHAR(30),
	LastName VARCHAR(30),
	DateOfBirth DATE,
	JMBG CHAR(13),
	AccountNumber VARCHAR(20),
	Email VARCHAR(30),
	Salary NUMERIC,	
	Position VARCHAR(25),	
	Username VARCHAR(20) UNIQUE NOT NULL,
	Password VARCHAR(20) NOT NULL,
	);

CREATE TABLE tblManagers(
	ManagerId INT PRIMARY KEY IDENTITY(1,1),
	EmployeeId INT FOREIGN KEY REFERENCES  tblEmployees(EmployeeId) ON DELETE SET NULL,
	Sector VARCHAR(30) NOT NULL,
	AccessLevel VARCHAR(15) NOT NULL
);

CREATE TABLE tblReports(
    ReportId INT PRIMARY KEY IDENTITY(1,1),
	EmployeeId INT FOREIGN KEY REFERENCES  tblEmployees(EmployeeId),
	ReportDate DATE NOT NULL,
	Project VARCHAR(30) NOT NULL,	
	WorkHours INT NOT NULL
);

GO
--creating view Reports
CREATE VIEW vwReports
as
Select r.ReportId,r.EmployeeId,CONCAT(e.FirstName, ' ', e.LastName) AS FullName,R.ReportDate, 
r.Project,e.Position,r.WorkHours
from tblReports r
inner join tblEmployees e
on e.EmployeeId = r.EmployeeId 