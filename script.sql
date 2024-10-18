-- Crear la base de datos
CREATE DATABASE asf;
GO

-- Usar la base de datos creada
USE asf;
GO

-- Tabla de AFP
CREATE TABLE AFP (
                     Id_AFP INT PRIMARY KEY IdENTITY(1,1),
                     Nombre NVARCHAR(100) NOT NULL,
                     Tasa_Contribución DECIMAL(5, 2) NOT NULL,
                     Status BIT NOT NULL DEFAULT(1)  -- 1 = Activo, 0 = Inactivo
);

-- Tabla de Cargo
CREATE TABLE Cargo (
                       Id_Cargo INT PRIMARY KEY IdENTITY(1,1),
                       Nombre NVARCHAR(100) NOT NULL,
                       Descripción NVARCHAR(255) NULL,
                       Status BIT NOT NULL DEFAULT(1)  -- 1 = Activo, 0 = Inactivo
);

-- Tabla de Empleado
CREATE TABLE Empleado (
                          Id_Empleado INT PRIMARY KEY IdENTITY(1,1),
                          Nombres NVARCHAR(100) NOT NULL,
                          ApellIdos NVARCHAR(100) NOT NULL,
                          Fecha_Nacimiento DATE NOT NULL,
                          Fecha_Ingreso DATE NOT NULL,
                          Id_AFP INT,
                          Id_Cargo INT,
                          Sueldo DECIMAL(10, 2) NOT NULL,
                          Status BIT NOT NULL DEFAULT(1),  -- 1 = Activo, 0 = Inactivo,
                          Deleted BIT NOT NULL DEFAULT(0),  -- 1 = Eliminado, 0 = No Eliminado,
                          FOREIGN KEY (Id_AFP) REFERENCES AFP(Id_AFP),
                          FOREIGN KEY (Id_Cargo) REFERENCES Cargo(Id_Cargo)
);

-- Tabla de Historial de Sueldo
CREATE TABLE Historial_Sueldo (
                                  Id_Historial INT PRIMARY KEY IdENTITY(1,1),
                                  Id_Empleado INT NOT NULL,
                                  Sueldo DECIMAL(10, 2) NOT NULL,
                                  Fecha_EfectivIdad DATE NOT NULL,
                                  Status BIT NOT NULL DEFAULT(1),  -- 1 = Activo, 0 = Inactivo
                                  FOREIGN KEY (Id_Empleado) REFERENCES Empleado(Id_Empleado)
);

-- Tabla de Registro de Horas
CREATE TABLE Registro_Horas (
                                Id_Registro INT PRIMARY KEY IdENTITY(1,1),
                                Id_Empleado INT NOT NULL,
                                Fecha DATE NOT NULL,
                                Horas_Trabajadas INT NOT NULL,
                                Status BIT NOT NULL DEFAULT(1),  -- 1 = Activo, 0 = Inactivo
                                FOREIGN KEY (Id_Empleado) REFERENCES Empleado(Id_Empleado)
);
GO


-- Registros fake
INSERT INTO AFP (Nombre, Tasa_Contribución, Status) VALUES
('AFP Integra', 10.00, 1),
('AFP Prima', 10.50, 1),
('AFP Habitat', 9.75, 1),
('AFP Profuturo', 11.20, 1),
('AFP Capital', 10.25, 1);

GO

INSERT INTO Cargo (Nombre, Descripción, Status) VALUES
('Desarrollador', 'Responsable del desarrollo de software.', 1),
('Analista de Sistemas', 'Encargado de analizar requisitos y diseñar soluciones.', 1),
('Gerente de Proyectos', 'Lidera y gestiona proyectos tecnológicos.', 1),
('Diseñador UX/UI', 'Diseña la experiencia de usuario y la interfaz.', 1),
('Tester de Calidad', 'Asegura la calidad del software a través de pruebas.', 1);

GO

INSERT INTO Empleado (Nombres, ApellIdos, Fecha_Nacimiento, Fecha_Ingreso, Id_AFP, Id_Cargo, Sueldo, Status) VALUES
('Juan', 'Pérez', '1985-04-15', '2020-01-10', 1, 1, 3000.00, 1),
('Ana', 'Gómez', '1990-06-22', '2021-05-15', 2, 2, 3500.00, 1),
('Luis', 'Martínez', '1983-09-30', '2019-03-20', 3, 3, 4000.00, 1),
('María', 'López', '1995-11-05', '2022-07-01', 4, 4, 3200.00, 1),
('Carlos', 'Fernández', '1988-02-18', '2020-09-15', 5, 5, 2800.00, 1);

GO

INSERT INTO Historial_Sueldo (Id_Empleado, Sueldo, Fecha_EfectivIdad, Status) VALUES
(1, 3000.00, '2020-01-10', 1),
(2, 3500.00, '2021-05-15', 1),
(3, 4000.00, '2019-03-20', 1),
(4, 3200.00, '2022-07-01', 1),
(5, 2800.00, '2020-09-15', 1);

GO

INSERT INTO Registro_Horas (Id_Empleado, Fecha, Horas_Trabajadas, Status) VALUES
(1, '2023-10-01', 8, 1),
(2, '2023-10-01', 7, 1),
(3, '2023-10-01', 9, 1),
(4, '2023-10-01', 6, 1),
(5, '2023-10-01', 8, 1);
