use BdPrueba;

-- Tabla de Categor�as de Ingredientes
CREATE TABLE CategoriasIngredientes (
    IdCategoria INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE -- Ej. 'Verdura', 'Fruta', 'Prote�na', 'Condimento'
);

-- Tabla de Ingredientes
CREATE TABLE Ingredientes (
    IdIngrediente INT IDENTITY PRIMARY KEY,
    IdCategoria INT NOT NULL,
    Nombre NVARCHAR(50) NOT NULL UNIQUE, --VERIFICA QUE CADA INGREDEINTE QUE AGREGUES INICIE CON LA PRIMERA LETRA MAYUSCULA.
    FOREIGN KEY (IdCategoria) REFERENCES CategoriasIngredientes(IdCategoria)
);

-- Tabla de Platillos
CREATE TABLE Platillos (
    IdPlatillo INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE, -- Ej. 'Huevo con jam�n'
    Porcion NVARCHAR(50),                 -- Ej. '2 Personas'
    Descripcion NVARCHAR(255),			-- Ej. 'Huevo revuelto con jam�n decorado con frutas'
    Imagen NVARCHAR(255)     ,            -- URL o nombre de archivo de la imagen
	TipoPlatillo VARCHAR(50)
);

-- Relaci�n entre Ingredientes y Platillos (Muchos a Muchos)
--la relacion entre ingredientes y platillos es por ID
-- es decir el platillo1 esta relacionado con ingrediente1, ingrediente2, etc
CREATE TABLE IngredientesPlatillos (
    IdIngredientePlatillo INT IDENTITY PRIMARY KEY,
    IdPlatillo INT NOT NULL,
    IdIngrediente INT NOT NULL,
    Cantidad INT NOT NULL,      -- Ej. '2 piezas', '100 gramos'
    FOREIGN KEY (IdPlatillo) REFERENCES Platillos(IdPlatillo),
    FOREIGN KEY (IdIngrediente) REFERENCES Ingredientes(IdIngrediente),
    CONSTRAINT UQ_IngredientesPlatillos UNIQUE (IdPlatillo, IdIngrediente) -- Evitar duplicados
);

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY PRIMARY KEY,
    Correo NVARCHAR(100) NOT NULL UNIQUE,
    Contrasena NVARCHAR(255) NOT NULL,  -- Encriptar las contrase�as
    FechaRegistro DATETIME DEFAULT GETDATE()
);

INSERT INTO Usuarios (Correo, Contrasena, FechaRegistro, UsuarioNombre)
VALUES
    ('correo123@gmail.com','123456','02/01/2025', 'Usuario1')

-- Tabla de Salud de Usuarios
CREATE TABLE Salud (
    IdSalud INT IDENTITY PRIMARY KEY,
    IdUsuario INT NOT NULL,
    Edad INT NOT NULL,
    Peso DECIMAL(5, 2) NOT NULL,        -- Precisi�n adecuada para pesos
    Alergias VARCHAR(255) NOT NULL DEFAULT 'Ninguna', 
    Enfermedades VARCHAR(255) NOT NULL DEFAULT 'Ninguna',
    FechaActualizacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE
);

-- Tabla de Registros de Consumo
CREATE TABLE Registros (
    IdRegistro INT IDENTITY PRIMARY KEY,
    IdUsuario INT NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE
);

-- Relaci�n entre Categor�as y Consumo Diario
CREATE TABLE DetalleRegistroConsumo (
    IdDetalle INT IDENTITY PRIMARY KEY,
    IdRegistro INT NOT NULL,
    IdCategoria INT NOT NULL,
    Cantidad NVARCHAR(50) NOT NULL,      -- Ej. '2 piezas', '300 gramos'
    FOREIGN KEY (IdRegistro) REFERENCES Registros(IdRegistro),
    FOREIGN KEY (IdCategoria) REFERENCES CategoriasIngredientes(IdCategoria)
);

SELECT * FROM Usuarios;

SELECT * FROM Salud;

SELECT * FROM Ingredientes;

SELECT * FROM Platillos;

SELECT * FROM DetalleRegistroConsumo;

SELECT * FROM IngredientesPlatillos;


SELECT
    p.IdPlatillo,
    p.Nombre AS Platillo,
    p.Porcion,
    p.Descripcion,
    p.Imagen,
    p.TipoPlatillo,
    STRING_AGG(i.Nombre, ', ') AS Ingredientes -- Concatenar los ingredientes del platillo
FROM
    Platillos p
JOIN
    IngredientesPlatillos ip ON p.IdPlatillo = ip.IdPlatillo
JOIN
    Ingredientes i ON ip.IdIngrediente = i.IdIngrediente
GROUP BY
    p.IdPlatillo, p.Nombre, p.Porcion, p.Descripcion, p.Imagen, p.TipoPlatillo
ORDER BY
    p.Nombre;

--Platillos e ingredientes de referencia:
--Atun con verduras salteadas (Cebolla, Zanahoria, Calabaza, Chile Serrano, Atun, Ajo, Oregano, Sal)
--Huevo con jamon	(Desayuno	Huevo, Sal, Jamon)