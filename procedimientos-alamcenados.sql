-- Crear la base de datos
CREATE DATABASE InventoryManagement
GO

-- Cambiar a la nueva base de datos
USE InventoryManagement
GO

-- Crear las tablas
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL
)

CREATE TABLE Usuarios (
    UserID INT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL,
    Contraseña VARCHAR(50) NOT NULL,
    RoleID INT NOT NULL,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
)

CREATE TABLE Productos (
    ProductID INT PRIMARY KEY,
    NombreProducto VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(200),
    Precio DECIMAL(10, 2)
)

CREATE TABLE Ubicaciones (
    LocationID INT PRIMARY KEY,
    NombreUbicacion VARCHAR(50) NOT NULL
)

CREATE TABLE InventarioProducto (
    ProductID INT PRIMARY KEY,
    Cantidad INT,
    LocationID INT,
    FOREIGN KEY (ProductID) REFERENCES Productos(ProductID),
    FOREIGN KEY (LocationID) REFERENCES Ubicaciones(LocationID)
)

CREATE TABLE EntradasStock (
    EntryID INT PRIMARY KEY,
    ProductID INT,
    Cantidad INT,
    FechaEntrada DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProductID) REFERENCES Productos(ProductID)
)

CREATE TABLE SalidasStock (
    ExitID INT PRIMARY KEY,
    ProductID INT,
    Cantidad INT,
    FechaSalida DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProductID) REFERENCES Productos(ProductID)
)

CREATE TABLE Pedidos (
    OrderID INT PRIMARY KEY,
    UserID INT,
    FechaPedido DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(10, 2),
    FOREIGN KEY (UserID) REFERENCES Usuarios(UserID)
)

CREATE TABLE Transaccion (
    TransactionID INT PRIMARY KEY,
    UserID INT,
    OrderID INT,
    Codigo VARCHAR(100) NOT NULL,
    Tipo SMALLINT NOT NULL DEFAULT 0,
    Modo SMALLINT NOT NULL DEFAULT 0,
    Estado SMALLINT NOT NULL DEFAULT 0,
    CreadoEn DATETIME NOT NULL DEFAULT GETDATE(),
    ActualizadoEn DATETIME NULL,
    Contenido TEXT NULL,
    FOREIGN KEY (UserID) REFERENCES Usuarios(UserID),
    FOREIGN KEY (OrderID) REFERENCES Pedidos(OrderID)
)

CREATE TABLE MetaProducto (
    MetaID INT PRIMARY KEY,
    ProductID INT,
    Clave VARCHAR(50) NOT NULL,
    Contenido TEXT NULL,
    FOREIGN KEY (ProductID) REFERENCES Productos(ProductID)
)

-- Crear procedimientos almacenados para aumentar y disminuir el stock
CREATE PROCEDURE AumentarStock
    @ProductID INT,
    @Cantidad INT
AS
BEGIN
    BEGIN TRANSACTION;
    UPDATE InventarioProducto
    SET Cantidad = Cantidad + @Cantidad
    WHERE ProductID = @ProductID;

    IF @@ERROR <> 0
    BEGIN
        ROLLBACK TRANSACTION;
        RETURN;
    END

    COMMIT TRANSACTION;
END
GO

CREATE PROCEDURE DisminuirStock
    @ProductID INT,
    @Cantidad INT
AS
BEGIN
    BEGIN TRANSACTION;
    UPDATE InventarioProducto
    SET Cantidad = Cantidad - @Cantidad
    WHERE ProductID = @ProductID;

    IF @@ERROR <> 0
    BEGIN
        ROLLBACK TRANSACTION;
        RETURN;
    END

    COMMIT TRANSACTION;
END
GO

-- Disparadores para ajustar el stock automáticamente
CREATE TRIGGER trg_InsertarEntradaStock
ON EntradasStock
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ProductID INT, @Cantidad INT;

    SELECT @ProductID = ProductID, @Cantidad = Cantidad
    FROM inserted;

    EXEC AumentarStock @ProductID = @ProductID, @Cantidad = @Cantidad;
END
GO

CREATE TRIGGER trg_InsertarSalidaStock
ON SalidasStock
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ProductID INT, @Cantidad INT;

    SELECT @ProductID = ProductID, @Cantidad = Cantidad
    FROM inserted;

    EXEC DisminuirStock @ProductID = @ProductID, @Cantidad = @Cantidad;
END
GO

CREATE PROCEDURE sp_InsertarProducto
    @ProductID INT,
    @NombreProducto VARCHAR(50),
    @Descripcion VARCHAR(200),
    @Precio DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO Productos (ProductID, NombreProducto, Descripcion, Precio)
    VALUES (@ProductID, @NombreProducto, @Descripcion, @Precio);
END;


CREATE PROCEDURE sp_ActualizarProducto
    @ProductID INT,
    @NuevoNombreProducto VARCHAR(50),
    @NuevaDescripcion VARCHAR(200),
    @NuevoPrecio DECIMAL(10, 2)
AS
BEGIN
    UPDATE Productos
    SET NombreProducto = @NuevoNombreProducto,
        Descripcion = @NuevaDescripcion,
        Precio = @NuevoPrecio
    WHERE ProductID = @ProductID;
END;

CREATE PROCEDURE sp_EliminarProducto
    @ProductID INT
AS
BEGIN
    DELETE FROM Productos
    WHERE ProductID = @ProductID;
END;

CREATE PROCEDURE sp_MostrarProductos
AS
BEGIN
    SELECT ProductID, NombreProducto, Descripcion, Precio
    FROM Productos;
END;

CREATE PROCEDURE sp_MostrarStockProductos
AS
BEGIN
    SELECT P.ProductID, P.NombreProducto, P.Descripcion, P.Precio, IP.Cantidad, U.NombreUbicacion
    FROM Productos P
    INNER JOIN InventarioProducto IP ON P.ProductID = IP.ProductID
    INNER JOIN Ubicaciones U ON IP.LocationID = U.LocationID;
END;


CREATE PROCEDURE sp_InsertarUsuario
    @UserID INT,
    @Username VARCHAR(50),
    @Contraseña VARCHAR(50),
    @RoleID INT
AS
BEGIN
    INSERT INTO Usuarios (UserID, Username, Contraseña, RoleID)
    VALUES (@UserID, @Username, @Contraseña, @RoleID);
END;


CREATE PROCEDURE sp_ActualizarUsuario
    @UserID INT,
    @NuevoUsername VARCHAR(50),
    @NuevaContraseña VARCHAR(50),
    @NuevoRoleID INT
AS
BEGIN
    UPDATE Usuarios
    SET Username = @NuevoUsername,
        Contraseña = @NuevaContraseña,
        RoleID = @NuevoRoleID
    WHERE UserID = @UserID;
END;

CREATE PROCEDURE sp_EliminarUsuario
    @UserID INT
AS
BEGIN
    DELETE FROM Usuarios
    WHERE UserID = @UserID;
END;

CREATE PROCEDURE sp_MostrarUsuariosConRoles
AS
BEGIN
    SELECT U.UserID, U.Username, U.Contraseña, U.RoleID, R.RoleName
    FROM Usuarios U
    INNER JOIN Roles R ON U.RoleID = R.RoleID;
END;


CREATE PROCEDURE sp_login
    @Username VARCHAR(50),
    @Contraseña VARCHAR(50)
AS
BEGIN
    DECLARE @UserID INT;
    DECLARE @RoleID INT;

    SELECT @UserID = UserID, @RoleID = RoleID
    FROM Usuarios
    WHERE Username = @Username AND Contraseña = @Contraseña;

    IF @UserID IS NOT NULL
    BEGIN
        SELECT 'Login successful' AS Message, @UserID AS UserID, @RoleID AS RoleID;
    END
    ELSE
    BEGIN
        SELECT 'Login failed. Invalid username or password.' AS Message;
    END
END;


