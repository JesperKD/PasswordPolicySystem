USE [master]
GO

DECLARE @kill varchar(8000) = ''
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'
FROM sys.dm_exec_sessions
WHERE database_id = DB_ID('PasswordPolicyDB')

EXEC(@kill)
GO

DROP DATABASE IF EXISTS [PasswordPolicyDB]
GO

CREATE DATABASE [PasswordPolicyDB]
GO

USE [PasswordPolicyDB]
GO


/*##################################################
				## Drop Tables ##
####################################################*/

DROP TABLE IF EXISTS [Logins]
GO


/*##################################################
				## Setup ##
####################################################*/

CREATE TABLE [Logins](
[Username] VarChar(100) NOT NULL,
[Password] VarChar(100) NOT NULL,
[Attempts] INT DEFAULT 1)
GO


/*##################################################
				## Alter Data Tables ##
####################################################*/

ALTER TABLE [Logins]
ADD
	PRIMARY KEY ([Username])
GO