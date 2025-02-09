USE DBG
GO

-- Validación para crear la tabla Usuarios si no existe
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Usuarios')
BEGIN
    CREATE TABLE dbo.Usuarios(
        Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
        NombreUsuario nvarchar(50) NOT NULL,
        Pass nvarchar(255) NOT NULL,
        FechaCreacion datetime2(7) NOT NULL
      )
        
END
GO

-- Validación para crear la tabla Personas si no existe
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Personas')
BEGIN
    CREATE TABLE dbo.Personas(
        Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Nombres nvarchar(100) NOT NULL,
        Apellidos nvarchar(100) NOT NULL,
        NumeroIdentificacion nvarchar(20) NOT NULL,
        Email nvarchar(150) NOT NULL,
        TipoIdentificacion nvarchar(50) NOT NULL,
        FechaCreacion datetime2(7) NOT NULL,
        FechaModificacion datetime2(7) NULL,
        Estado nvarchar(max) NOT NULL,
        IdentificacionCompleta AS (TipoIdentificacion + ' - ' + NumeroIdentificacion),
        NombreCompleto AS (Nombres + ' ' + Apellidos))
       


-- Insertar 5 personas 
INSERT INTO dbo.Personas (Nombres, Apellidos, NumeroIdentificacion, Email, TipoIdentificacion, FechaCreacion, Estado)
VALUES 
('Juan', 'Pérez', '123456789', 'juan.perez@email.com', 'Cédula', GETDATE(), 'A'),
('Ana', 'González', '987654321', 'ana.gonzalez@email.com', 'Cédula', GETDATE(), 'A'),
('Carlos', 'Martínez', '456789123', 'carlos.martinez@email.com', 'Pasaporte', GETDATE(), 'A'),
('María', 'López', '321654987', 'maria.lopez@email.com', 'Cédula', GETDATE(), 'A'),
('Luis', 'Ramírez', '159753468', 'luis.ramirez@email.com', 'Pasaporte', GETDATE(), 'A');

END
GO

-- Insertar el usuario 'admin' si no existe
IF NOT EXISTS (SELECT 1 FROM dbo.Usuarios WHERE NombreUsuario = 'admin')
BEGIN
    INSERT INTO dbo.Usuarios (NombreUsuario, Pass, FechaCreacion)
    VALUES ('admin', '12345', GETDATE());
END
GO



