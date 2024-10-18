-- Registrar
CREATE OR ALTER PROCEDURE SP_InsertarEmpleado
    @Nombres NVARCHAR(100),
    @ApellIdos NVARCHAR(100),
    @Fecha_Nacimiento DATE,
    @Fecha_Ingreso DATE,
    @Id_AFP INT,
    @Id_Cargo INT,
    @Sueldo DECIMAL(18, 2)
AS
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

INSERT INTO Empleado (Nombres, ApellIdos, Fecha_Nacimiento, Fecha_Ingreso, Id_AFP, Id_Cargo, Sueldo, Status)
VALUES (@Nombres, @ApellIdos, @Fecha_Nacimiento, @Fecha_Ingreso, @Id_AFP, @Id_Cargo, @Sueldo, 1);

COMMIT TRANSACTION;
END TRY
BEGIN CATCH
ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
END CATCH
END;
GO

-- Obtener empleado por Id
CREATE OR ALTER PROCEDURE SP_ObtenerEmpleadoPorId
    @Id_Empleado INT
AS
BEGIN
BEGIN TRY
SELECT * FROM Empleado WHERE Id_Empleado = @Id_Empleado AND Deleted = 0;
END TRY
BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
END CATCH
END;
GO

-- Modificar
CREATE OR ALTER PROCEDURE SP_ActualizarEmpleado
    @Id_Empleado INT,
    @Nombres NVARCHAR(100),
    @ApellIdos NVARCHAR(100),
    @Fecha_Nacimiento DATE,
    @Fecha_Ingreso DATE,
    @Id_AFP INT,
    @Id_Cargo INT,
    @Sueldo DECIMAL(18, 2),
    @Status BIT
AS
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

UPDATE Empleado
SET Nombres = @Nombres,
    ApellIdos = @ApellIdos,
    Fecha_Nacimiento = @Fecha_Nacimiento,
    Fecha_Ingreso = @Fecha_Ingreso,
    Id_AFP = @Id_AFP,
    Id_Cargo = @Id_Cargo,
    Sueldo = @Sueldo,
    Status = @Status
WHERE Id_Empleado = @Id_Empleado AND Deleted = 0;

COMMIT TRANSACTION;
END TRY
BEGIN CATCH
ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
END CATCH
END;
GO

-- Borrado logico
CREATE OR ALTER PROCEDURE SP_EliminarEmpleado
    @Id_Empleado INT
AS
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

UPDATE Empleado
SET Deleted = 1
WHERE Id_Empleado = @Id_Empleado AND Deleted = 0;

COMMIT TRANSACTION;
END TRY
BEGIN CATCH
ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
END CATCH
END;
GO

-- Listar
CREATE OR ALTER PROCEDURE SP_ObtenerEmpleados
    AS
BEGIN
BEGIN TRY
SELECT E.*, A.Nombre as AFP, C.Nombre as Cargo FROM Empleado E
    INNER JOIN AFP A ON E.Id_AFP = A.Id_AFP
    INNER JOIN Cargo C ON E.Id_Cargo = C.Id_Cargo AND E.Deleted = 0;
END TRY
BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
END CATCH
END;
GO