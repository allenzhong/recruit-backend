IF DB_ID('CardManagement') IS NOT NULL
BEGIN
    SELECT 'Database Name already Exist' AS Message
END
ELSE
BEGIN
    CREATE DATABASE [CardManagement]
    SELECT 'Database is Created'
END