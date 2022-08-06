
-- Creamos un nuevo procedimiento almacenado, llamado SelectClient

CREATE PROCEDURE SelectClient
    @ClientID INT NULL
AS
    IF @ClientID IS NULL
        SELECT * FROM Client;
    ELSE
        SELECT * FROM Client WHERE ID = @ClientID;
GO

EXEC SelectClient @ClientID = 4;
EXEC SelectClient @ClientID = NULL;

-- Modifica el procedimiento almacenado InsertClient

EXEC InsertClient @Name = 'Jose Pedro', @PhoneNumber = '8415578456', @Email = 'pedro@hotmail.com';
SELECT * FROM Client;
EXEC InsertClient @Name = 'Jose', @PhoneNumber = '8415578456', @Email = 'jose@gmail.com';
SELECT * FROM Client;

-- Modificamos el procedimiento almacenado InsertBankTransaction

EXEC InsertBankTransaction @AccountID = 2, @TransactionType = 2, @Amount = 12000;
SELECT * FROM Account;
EXEC InsertBankTransaction @AccountID = 2, @TransactionType = 2, @Amount = 2000;
SELECT * FROM Account;

-- Generamos un full backup de la base de datos Bank
BACKUP DATABASE Bank TO DISK = 'C:\Users\juanc\OneDrive\Documentos\CDIS-2022\.NET\Semana_3\bank22072022.bak' WITH INIT;