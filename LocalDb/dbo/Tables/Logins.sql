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