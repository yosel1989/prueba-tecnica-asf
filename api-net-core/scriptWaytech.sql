/**

CREATE DATABASE "waytech";
USE "waytech";


*/

/**** Tabla Usuario ****/
DROP TABLE IF EXISTS Usuario;
CREATE TABLE Usuario(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(150) NOT NULL,
	Phone VARCHAR(15) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	Company VARCHAR(150) NOT NULL,
	Address VARCHAR(150) NOT NULL,
	Latitude DECIMAL(10,8) NOT NULL,
	Longitude DECIMAL(10,8) NOT NULL
);
GO




/*** PROCEDIMIENTO LISTAR UsuarioS ***/
CREATE OR ALTER PROCEDURE SP_Usuario_Listar
AS
BEGIN
	SELECT 
	*
	FROM Usuario ;
END;
GO
-- EXEC SP_Usuario_Listar





/**** PROCEDIMIENTO CREAR Usuario***/
CREATE OR ALTER PROCEDURE SP_Usuario_Crear
	@pNombre VARCHAR(150),
	@pTelefono VARCHAR(15),
	@pEmail VARCHAR(50),
	@pCompany VARCHAR(150),
	@pAddress VARCHAR(150),
	@pLatitude DECIMAL(10,8),
	@pLongitude DECIMAL(10,8)
AS
BEGIN
	DECLARE @vFechaRegistra DATETIME = GETDATE();
	DECLARE @Id INT = 0;
	DECLARE @ErrorNumero INT = 0, @ErrorDetalle VARCHAR(MAX) = ''
	DECLARE @vMensajeError VARCHAR(MAX) = 'Ocurrio un error al intentar registrar el Usuario'

	BEGIN TRY
		BEGIN TRANSACTION

		INSERT INTO Usuario(Name, Phone, Email, Company, Address, Latitude, Longitude) VALUES(
			@pNombre,
			@pTelefono,
			@pEmail,
			@pCompany,
			@pAddress,
			@pLatitude,
			@pLongitude
		);

		SET @Id = @@IDENTITY;

						  
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		SELECT @ErrorNumero = ERROR_NUMBER(), @ErrorDetalle = ERROR_MESSAGE()
		GOTO Error_Rollback
	END CATCH

	Error_Rollback:
	IF @@TRANCOUNT > 0
	BEGIN
		SELECT 
			'Error' = 1, 
			'ErrorNumero' = @ErrorNumero,
			'ErrorDetalle' = @ErrorDetalle,
			'Mensaje' = @vMensajeError,
			'IdVenta' = 0;
		ROLLBACK TRAN
	END
	ELSE
	BEGIN
		SELECT 
			*
			FROM Usuario
		WHERE Id = @Id;
	END
END;
GO










/**** PROCEDIMIENTO MODIFICAR Usuario***/
CREATE OR ALTER PROCEDURE SP_Usuario_Modificar
	@pId INT,
	@pNombre VARCHAR(150),
	@pTelefono VARCHAR(15),
	@pEmail VARCHAR(50),
	@pCompany VARCHAR(150),
	@pAddress VARCHAR(150),
	@pLatitude DECIMAL(10,8),
	@pLongitude DECIMAL(10,8)
AS
BEGIN
	DECLARE @vFechaRegistra DATETIME = GETDATE();
	DECLARE @ErrorNumero INT = 0, @ErrorDetalle VARCHAR(MAX) = ''
	DECLARE @vMensajeError VARCHAR(MAX) = 'Ocurrio un error al intentar modificar el Usuario'

	BEGIN TRY
		BEGIN TRANSACTION

		UPDATE Usuario SET 
			Name = @pNombre, 
			Phone = @pTelefono, 
			Email = @pEmail, 
			Company = @pCompany, 
			Address = @pAddress, 
			Latitude = @pLatitude, 
			Longitude = @pLongitude
		WHERE Id = @pId;

						  
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		SELECT @ErrorNumero = ERROR_NUMBER(), @ErrorDetalle = ERROR_MESSAGE()
		GOTO Error_Rollback
	END CATCH

	Error_Rollback:
	IF @@TRANCOUNT > 0
	BEGIN
		SELECT 
			'Error' = 1, 
			'ErrorNumero' = @ErrorNumero,
			'ErrorDetalle' = @ErrorDetalle,
			'Mensaje' = @vMensajeError,
			'IdVenta' = 0;
		ROLLBACK TRAN
	END
	ELSE
	BEGIN
		SELECT 
			*
			FROM Usuario
		WHERE Id = @pId;
	END
END;
GO







/**** PROCEDIMIENTO ELIMINAR Usuario ***/
CREATE OR ALTER PROCEDURE SP_Usuario_Eliminar
	@pId INT
AS
BEGIN
	DECLARE @vFechaRegistra DATETIME = GETDATE();
	DECLARE @ErrorNumero INT = 0, @ErrorDetalle VARCHAR(MAX) = ''
	DECLARE @vMensajeError VARCHAR(MAX) = 'Ocurrio un error al intentar eliminar el Usuario'

	BEGIN TRY
		BEGIN TRANSACTION

		DELETE Usuario WHERE Id = @pId;

						  
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		SELECT @ErrorNumero = ERROR_NUMBER(), @ErrorDetalle = ERROR_MESSAGE()
		GOTO Error_Rollback
	END CATCH

	Error_Rollback:
	IF @@TRANCOUNT > 0
	BEGIN
		SELECT 
			'Error' = 1, 
			'ErrorNumero' = @ErrorNumero,
			'ErrorDetalle' = @ErrorDetalle,
			'Mensaje' = @vMensajeError,
			'IdVenta' = 0;
		ROLLBACK TRAN
	END
	ELSE
	BEGIN
		SELECT Exito = 1;
	END
END;
GO









