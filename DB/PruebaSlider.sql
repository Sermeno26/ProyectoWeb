USE MercaUca;

select*from Usuario;

--Usuario para las fotos Destacadas;
INSERT INTO Usuario (id_usuario, email_usuario, password_hash, nombre_usuario, telefono_usuario, id_estado, id_rol) VALUES
('leguzman14', 'rodrigoguzman1412@gmail.com', 'HASH123', 'Luis Guzman', '0000-0000', 'ACT', 'ADM');

INSERT INTO Usuario (id_usuario, email_usuario, password_hash, nombre_usuario, telefono_usuario, id_estado, id_rol) VALUES
('ProductoMediaSlider', 'ProductoMediaSlider@gmail.com', 'HASH123', 'Product Media', '0000-0000', 'ACT', 'ADM');


--Datos extra de los productos
INSERT INTO Categoria (id_categoria, nombre_categoria, categoria_activa)
VALUES
('CAT001', 'Relojes', 1),
('CAT002', 'Joyería', 1),
('CAT003', 'Electrónica', 1),
('CAT004', 'Moda', 1),
('CAT005', 'Hogar y decoración', 1);
('CATS01', 'Slider y Ofertas', 1);

INSERT INTO Condicion_producto (id_condicion, nombre)
VALUES
('C001', 'Nuevo'),
('C002', 'Usado - Como nuevo'),
('C003', 'Usado - Buen estado'),
('C004', 'Usado - Regular'),
('C005', 'Reacondicionado');


EXEC sp_configure 'show advanced options', 1; RECONFIGURE;
EXEC sp_configure 'Ad Hoc Distributed Queries', 1; RECONFIGURE;

--insercion de producto con foto 
--PSD## para productos del slider
INSERT INTO Productos (
    id_producto,
    id_vendedor,
    titulo_producto,
    descripcion,
    marca,
    id_condicion,
    foto,
    id_categoria
)
SELECT 
    'PSD01',
    'ProductoMediaSlider',
    'Reloj Bulova Classic',
    'Reloj de pulsera con correa de cuero y caja de acero inoxidable.',
    'Bulova',
    'C001', -- Nuevo
    BulkColumn AS foto,
    'CAT001' -- Relojes
FROM OPENROWSET(BULK 'C:\IMGSQL\BULOVAWATCH.jpg', SINGLE_BLOB) AS imagen;
--/////////////////

INSERT INTO Productos (
    id_producto,
    id_vendedor,
    titulo_producto,
    descripcion,
    marca,
    id_condicion,
    foto,
    id_categoria
)
SELECT 
    'PSD02',
    'ProductoMediaSlider',
    'Anillo de Slider',
    'Anillo de oro',
	'Anillos Arguello',
    'C001', -- Nuevo
    BulkColumn AS foto,
    'CAT002' -- Relojes
FROM OPENROWSET(BULK 'C:\IMGSQL\ANILLOSLIDER.jpg', SINGLE_BLOB) AS imagen;
--//////////////////////////
INSERT INTO Productos (
    id_producto,
    id_vendedor,
    titulo_producto,
    descripcion,
    marca,
    id_condicion,
    foto,
    id_categoria
)
SELECT 
    'PSD03',
    'ProductoMediaSlider',
    'Ofertas',
    'Ofertas',
    'Slider',
    'C001', -- Nuevo
    BulkColumn AS foto,
    'CATS01' -- Relojes
FROM OPENROWSET(BULK 'C:\IMGSQL\OFERTASSLIDER.jpg', SINGLE_BLOB) AS imagen;



--Comprarcion del PATH
DECLARE @ruta NVARCHAR(4000) = 'C:\IMGSQL\BULOVAWATCH.jpg';
DECLARE @existe INT;

EXEC master..xp_fileexist @ruta, @existe OUTPUT;
SELECT Ruta=@ruta, Existe=@existe; -- 1 = existe, 0 = no


SELECT foto FROM Productos P
INNER JOIN Usuario U ON U.id_usuario = P.id_vendedor
WHERE U.id_usuario = 'ProductoMediaSlider';


--////////////
INSERT INTO Productos (
    id_producto,
    id_vendedor,
    titulo_producto,
    descripcion,
    marca,
    id_condicion,
    foto,
    id_categoria,
	precio
)
SELECT 
    'PB02',
    'Vale48Sebas',
    'Sram G2 R',
    'Ofertas',
    'SRAM',
    'C001', -- Nuevo
    BulkColumn AS foto,
    'CATS01', -- Relojes
	120.99
FROM OPENROWSET(BULK 'C:\IMGSQL\SramG2R.jpg', SINGLE_BLOB) AS imagen;

INSERT INTO Productos (
    id_producto,
    id_vendedor,
    titulo_producto,
    descripcion,
    marca,
    id_condicion,
    foto,
    id_categoria,
	precio
)
SELECT 
    'PB03',
    'Vale48Sebas',
    'Sram GX EAGLE 12s',
    'Ofertas',
    'SRAM',
    'C001', -- Nuevo
    BulkColumn AS foto,
    'CATS01', -- Relojes
	493.25
FROM OPENROWSET(BULK 'C:\IMGSQL\SramGroupsetGX.jpg', SINGLE_BLOB) AS imagen;


INSERT INTO Productos (
    id_producto,
    id_vendedor,
    titulo_producto,
    descripcion,
    marca,
    id_condicion,
    foto,
    id_categoria,
	precio
)
SELECT 
    'PB04',
    'Vale48Sebas',
    'FOX FACTORY 36',
    'Ofertas',
    'SRAM',
    'C001', -- Nuevo
    BulkColumn AS foto,
    'CATS01', -- Relojes
	836.81
FROM OPENROWSET(BULK 'C:\IMGSQL\FOXFACTORY36.jpg', SINGLE_BLOB) AS imagen;

INSERT INTO Productos (
    id_producto,
    id_vendedor,
    titulo_producto,
    descripcion,
    marca,
    id_condicion,
    foto,
    id_categoria,
	precio
)
SELECT 
    'PB05',
    'Vale48Sebas',
    'MINION DHF 3C',
    'Ofertas',
    'MAXXIS',
    'C001', -- Nuevo
    BulkColumn AS foto,
    'CATS01', -- Relojes
	49.99
FROM OPENROWSET(BULK 'C:\IMGSQL\maxxisminionsDHF.jpg', SINGLE_BLOB) AS imagen;


SELECT*FROM Producto_media;

INSERT INTO Producto_media VALUES ('PB02','PB02',0,0,1)
INSERT INTO Producto_media VALUES ('PB03M','PB03',0,0,1)
INSERT INTO Producto_media VALUES ('PB04M','PB04',0,0,1)
INSERT INTO Producto_media VALUES ('PB05M','PB05',0,0,1)