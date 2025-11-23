CREATE DATABASE MercaUca;
USE MercaUca;

CREATE TABLE Usuario
(
	id_usuario varchar(20) NOT NULL,
	email_usuario varchar(max) NOT NULL,
	password_hash NVARCHAR(255) NOT NULL,
	nombre_usuario VARCHAR(175) NOT NULL,
	telefono_usuario VARCHAR(15) NOT NULL,
	id_estado VARCHAR(10) NOT NULL,
	id_rol VARCHAR(10) NOT NULL
);

CREATE TABLE Estado_usuario
(
	id_estado VARCHAR(10) NOT NULL,
	nombre_estado VARCHAR(80) NULL
);

CREATE TABLE Roles
(
	id_rol VARCHAR(10) NOT NULL,
	nombre_rol VARCHAR(80) NULL
);

CREATE TABLE Direcciones
(
	id_direccion VARCHAR(30) NOT NULL,
	nombre_direccion VARCHAR(80) NULL,
	id_usuario varchar(20) NOT NULL,
	nombre_receptor VARCHAR(170) NULL,
	telefono_receptor VARCHAR(15) NULL,
	direccion_default BIT NOT NULL,
	id_pais VARCHAR(10) NOT NULL
);

CREATE TABLE Pais
(
	id_pais VARCHAR(10) NOT NULL,
	nombre_pais VARCHAR(75) NOT NULL
);

CREATE TABLE Linea_envio_direccion
(
	id_direccion VARCHAR(30) NOT NULL,
	linea_direccion VARCHAR(MAX) NOT NULL
);


CREATE TABLE Productos
(
	id_producto VARCHAR(25) NOT NULL,
	id_vendedor VARCHAR(20) NOT NULL, /* FK de Usuario */
	titulo_producto VARCHAR(100) NOT NULL,
	descripcion VARCHAR(MAX) NOT NULL,
	marca VARCHAR(50) NOT NULL,
	id_condicion VARCHAR(25) NOT NULL,
	fechaCreacion DATETIME2 DEFAULT SYSDATETIME(),
	fechaModificacion DATETIME2 DEFAULT SYSDATETIME(),
	foto VARBINARY(MAX) NOT NULL,
	id_categoria VARCHAR(25) NOT NULL
);

alter table Productos add precio float;
CREATE TABLE Categoria
(
	id_categoria VARCHAR(25) NOT NULL,
	nombre_categoria VARCHAR(75) NOT NULL,
	categoria_activa BIT NOT NULL
);

CREATE TABLE Condicion_producto 
(
	id_condicion VARCHAR(25) NOT NULL,
	nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Producto_media
(
	id_media VARCHAR(25) NOT NULL,
	id_producto VARCHAR(25) NOT NULL,
	posicion_galeria int not null,
	producto_primario BIT NOT NULL,
	Novedades BIT not null
	/* AQUI SE PUEDEN INCLUIR MODIFICACIONES DE LA VISTA DEL PRODUCT0*/
);


CREATE TABLE Variantes_productos
(
	id_producto_variante VARCHAR(25) NOT NULL,
	id_producto VARCHAR(25) NOT NULL,
	talla VARCHAR(45) NULL,
	color VARCHAR(55) NULL,
	stock INT NOT NULL
);

CREATE TABLE Carrito
(
	id_carrito VARCHAR(55) NOT NULL,
	id_comprador varchar(20) NOT NULL,	/* FK de Usuario */
	fechaCreacion DATETIME2 DEFAULT SYSDATETIME(),
	fechaModificacion DATETIME2 DEFAULT SYSDATETIME(),
);

CREATE TABLE Articulos_carrito
(
	id_articulos_carrito VARCHAR(55) NOT NULL,
	id_carrito VARCHAR(55) NOT NULL,	/* FK de Carrito */
	id_producto_variante VARCHAR(25) NOT NULL,
	cantidad INT NOT NULL,
	precio FLOAT NOT NULL,
	moneda VARCHAR(10) NOT NULL	/* coherencia al mostrar totales. */
);

CREATE TABLE Orden
(
	id_orden VARCHAR(25) NOT NULL,
	id_comprador varchar(20) NOT NULL,	/* FK de Usuario */
	id_estado_orden VARCHAR(25) NOT NULL,
	monto_total FLOAT NOT NULL,
	moneda VARCHAR(10) NOT NULL,	/* coherencia al mostrar totales. */
	id_direccion VARCHAR(30) NOT NULL,
	fechaCreacion DATETIME2 DEFAULT SYSDATETIME(),
	fecha_pago DATETIME2 NULL,
	fecha_cierre DATETIME2 NULL
);

CREATE TABLE Estados_orden
(
	id_estado_orden VARCHAR(25) NOT NULL,
	nombre_estado VARCHAR(75) NOT NULL,
);


CREATE TABLE Articulos_orden
(
	id_articulo_orden VARCHAR(25) NOT NULL,
	id_orden VARCHAR(25) NOT NULL,
	id_vendedor VARCHAR(20) NOT NULL, /* FK de Usuario */
	id_producto_variante VARCHAR(25) NOT NULL,
	id_estado_articulo_orden VARCHAR(25) NOT  NULL,
	cantidad INT NOT NULL,
	sub_total FLOAT NOT NULL,
	porcentaje_comision FLOAT NOT NULL,
	subtotal_comision FLOAT NOT NULL,
	tracking_number VARCHAR(250) NOT  NULL,
	fecha_entrega DATETIME2 DEFAULT SYSDATETIME()
);

CREATE TABLE Estado_articulos_orden
(
	id_estado_articulo_orden VARCHAR(25) NOT  NULL,
	nombre_estado VARCHAR(50) NOT NULL
);

CREATE TABLE Proveedor_payments
(
	id_proveedor_pago VARCHAR(25) NOT NULL,
	nombre_proveedor VARCHAR(75) NOT NULL
);

CREATE TABLE Estado_payment
(
	id_estado_payment VARCHAR(25) NOT NULL,
	nombre_estado VARCHAR(85) NOT NULL
);

CREATE TABLE PAYMENTS
(
	payment_id VARCHAR(25) NOT NULL,
	id_orden VARCHAR(25) NOT NULL,
	id_proveedor_pago VARCHAR(25) NOT NULL,
	monto FLOAT NOT NULL,
	moneda VARCHAR(10) NOT NULL,	/* coherencia al mostrar totales. */
	id_estado_payment VARCHAR(25) NOT NULL,
	refrencia_transaccion VARCHAR(150) NOT NULL,
	fecha_realizacion DATETIME2 DEFAULT SYSDATETIME(),
	fecha_autorizacion DATETIME2 DEFAULT SYSDATETIME()
);

CREATE TABLE Payouts
(
	payout_id VARCHAR(25) NOT NULL,
	id_vendedor VARCHAR(20) NOT NULL, /* FK de Usuario */
	id_orden VARCHAR(25) NOT NULL,
	porcentaje_comision FLOAT NOT NULL,
	monto_neto FLOAT NOT NULL,
	id_payout_estado VARCHAR(25) NOT  NULL,
	id_metodo_abono VARCHAR(25) NOT NULL,
	fecha_realizacion DATETIME2 NULL
);

CREATE TABLE Payout_estado 
(
	id_payout_estado VARCHAR(25) NOT  NULL,
	nombre_estado VARCHAR(85) NOT NULL
);

CREATE TABLE Metodo_abono
(
	id_metodo_abono VARCHAR(25) NOT NULL,
	nombre_metodo VARCHAR(85) NOT NULL
);

CREATE TABLE Review
(
	review_id VARCHAR(25) NOT NULL,
	id_articulo_orden VARCHAR(25) NOT NULL,
	id_vendedor VARCHAR(20) NOT NULL, /* FK de Usuario */
	id_comprador varchar(20) NOT NULL,	/* FK de Usuario */
	id_producto VARCHAR(25) NOT NULL,
	puntuacion INT NOT NULL,
	comentario VARCHAR(200) NOT NULL,
	fecha_creacion DATETIME2 DEFAULT SYSDATETIME(),
	publico BIT NOT NULL
);


--Llaves primarias
ALTER TABLE Usuario ADD CONSTRAINT PK_Usuario PRIMARY KEY(id_usuario);
ALTER TABLE Estado_usuario ADD CONSTRAINT PK_Estado_usuario PRIMARY KEY(id_estado);
ALTER TABLE Roles ADD CONSTRAINT PK_Roles PRIMARY KEY(id_rol);
ALTER TABLE Direcciones ADD CONSTRAINT PK_Direcciones PRIMARY KEY(id_direccion);
ALTER TABLE Pais ADD CONSTRAINT PK_Pais PRIMARY KEY(id_pais);
ALTER TABLE Categoria ADD CONSTRAINT PK_Categoria PRIMARY KEY(id_categoria);
ALTER TABLE Condicion_producto ADD CONSTRAINT PK_Condicion_producto PRIMARY KEY(id_condicion);
ALTER TABLE Productos ADD CONSTRAINT PK_Productos PRIMARY KEY(id_producto);
ALTER TABLE Producto_media ADD CONSTRAINT PK_Producto_media PRIMARY KEY(id_media);
ALTER TABLE Variantes_productos ADD CONSTRAINT PK_Variantes_productos PRIMARY KEY(id_producto_variante);
ALTER TABLE Carrito ADD CONSTRAINT PK_Carrito PRIMARY KEY(id_carrito);
ALTER TABLE Articulos_carrito ADD CONSTRAINT PK_Articulos_carrito PRIMARY KEY(id_articulos_carrito);
ALTER TABLE Orden ADD CONSTRAINT PK_Orden PRIMARY KEY(id_orden);
ALTER TABLE Estados_orden ADD CONSTRAINT PK_Estados_orden PRIMARY KEY(id_estado_orden);
ALTER TABLE Articulos_orden ADD CONSTRAINT PK_Articulos_orden PRIMARY KEY(id_articulo_orden);
ALTER TABLE Estado_articulos_orden ADD CONSTRAINT PK_Estado_articulos_orden PRIMARY KEY(id_estado_articulo_orden);
ALTER TABLE Proveedor_payments ADD CONSTRAINT PK_Proveedor_payments PRIMARY KEY(id_proveedor_pago);
ALTER TABLE Estado_payment ADD CONSTRAINT PK_Estado_payment PRIMARY KEY(id_estado_payment);
ALTER TABLE PAYMENTS ADD CONSTRAINT PK_PAYMENTS PRIMARY KEY(payment_id);
ALTER TABLE Payouts ADD CONSTRAINT PK_Payouts PRIMARY KEY(payout_id);
ALTER TABLE Payout_estado ADD CONSTRAINT PK_Payout_estado PRIMARY KEY(id_payout_estado);
ALTER TABLE Metodo_abono ADD CONSTRAINT PK_Metodo_abono PRIMARY KEY(id_metodo_abono);
ALTER TABLE Review ADD CONSTRAINT PK_Review PRIMARY KEY(review_id);


--Foreign Keys
--Usuario
ALTER TABLE Usuario ADD CONSTRAINT FK_Estado_usuario_Usuario FOREIGN KEY(id_estado)
REFERENCES Estado_usuario(id_estado);

ALTER TABLE Usuario ADD CONSTRAINT FK_Roleso_Usuario FOREIGN KEY(id_rol)
REFERENCES Roles(id_rol);

--Direcciones
ALTER TABLE Direcciones ADD CONSTRAINT FK_Usuario_Direcciones FOREIGN KEY(id_usuario)
REFERENCES Usuario(id_usuario);

ALTER TABLE Direcciones ADD CONSTRAINT FK_Pais_Direcciones FOREIGN KEY(id_pais)
REFERENCES Pais(id_pais);

--Linea de envio 'etiqueta'
ALTER TABLE Linea_envio_direccion ADD CONSTRAINT FK_Direcciones_Linea_envio_direccion FOREIGN KEY(id_direccion)
REFERENCES Direcciones(id_direccion);

--Producto
ALTER TABLE Productos ADD CONSTRAINT FK_Usuario_Productos FOREIGN KEY(id_vendedor)
REFERENCES Usuario(id_usuario);

ALTER TABLE Productos ADD CONSTRAINT FK_Condicion_producto_Productos FOREIGN KEY(id_condicion)
REFERENCES Condicion_producto(id_condicion);

ALTER TABLE Productos ADD CONSTRAINT FK_Categoria_producto_Productos FOREIGN KEY(id_categoria)
REFERENCES Categoria(id_categoria);

--Producto @media
ALTER TABLE Producto_media ADD CONSTRAINT FK_Productos_Producto_media FOREIGN KEY(id_producto)
REFERENCES Productos(id_producto);

--Producto variante
ALTER TABLE Variantes_productos ADD CONSTRAINT FK_Productos_Variantes_productos FOREIGN KEY(id_producto)
REFERENCES Productos(id_producto);

--Carrito
ALTER TABLE Carrito ADD CONSTRAINT FK_Usuario_Carrito FOREIGN KEY(id_comprador)
REFERENCES Usuario(id_usuario);

ALTER TABLE Articulos_carrito ADD CONSTRAINT FK_Carrito_Articulos_carrito FOREIGN KEY(id_carrito)
REFERENCES Carrito(id_carrito);

ALTER TABLE Articulos_carrito ADD CONSTRAINT FK_Variantes_productos_Articulos_carrito FOREIGN KEY(id_producto_variante)
REFERENCES Variantes_productos(id_producto_variante);

--Orden
ALTER TABLE Orden ADD CONSTRAINT FK_Usuario_Orden FOREIGN KEY(id_comprador)
REFERENCES Usuario(id_usuario);

ALTER TABLE Orden ADD CONSTRAINT FK_Estados_orden_Orden FOREIGN KEY(id_estado_orden)
REFERENCES Estados_orden(id_estado_orden);

ALTER TABLE Articulos_orden ADD CONSTRAINT FK_Orden_Articulos_orden FOREIGN KEY(id_orden)
REFERENCES Orden(id_orden);

ALTER TABLE Articulos_orden ADD CONSTRAINT FK_Variantes_productos_Articulos_orden FOREIGN KEY(id_producto_variante)
REFERENCES Variantes_productos(id_producto_variante);

ALTER TABLE Articulos_orden ADD CONSTRAINT FK_Estado_articulos_orden_Articulos_orden FOREIGN KEY(id_estado_articulo_orden)
REFERENCES Estado_articulos_orden(id_estado_articulo_orden);

--Payments
ALTER TABLE PAYMENTS ADD CONSTRAINT FK_Orden_PAYMENTS FOREIGN KEY(id_orden)
REFERENCES Orden(id_orden);

ALTER TABLE PAYMENTS ADD CONSTRAINT FK_Proveedor_payments_PAYMENTS FOREIGN KEY(id_proveedor_pago)
REFERENCES Proveedor_payments(id_proveedor_pago);

ALTER TABLE PAYMENTS ADD CONSTRAINT FK_Estado_payment_PAYMENTS FOREIGN KEY(id_estado_payment)
REFERENCES Estado_payment(id_estado_payment);

--Payouts
ALTER TABLE Payouts ADD CONSTRAINT FK_Usuario_Payouts FOREIGN KEY(id_vendedor)
REFERENCES Usuario(id_usuario);

ALTER TABLE Payouts ADD CONSTRAINT FK_Orden_Payouts FOREIGN KEY(id_orden)
REFERENCES Orden(id_orden);

ALTER TABLE Payouts ADD CONSTRAINT FK_Payout_estado_Payouts FOREIGN KEY(id_payout_estado)
REFERENCES Payout_estado(id_payout_estado);

ALTER TABLE Payouts ADD CONSTRAINT FK_Metodo_abono_estado_Payouts FOREIGN KEY(id_metodo_abono)
REFERENCES Metodo_abono(id_metodo_abono);


-- Estados mínimos
INSERT INTO Estado_usuario (id_estado, nombre_estado) VALUES ('ACT', 'Activo');
INSERT INTO Estado_usuario (id_estado, nombre_estado) VALUES ('INA', 'Inactivo');

-- Roles mínimos
INSERT INTO Roles (id_rol, nombre_rol) VALUES ('CMP', 'Comprador');
INSERT INTO Roles (id_rol, nombre_rol) VALUES ('VEN', 'Vendedor');
INSERT INTO Roles (id_rol, nombre_rol) VALUES ('ADM', 'Administrador');

-- 20 usuarios de ejemplo
INSERT INTO Usuario (id_usuario, email_usuario, password_hash, nombre_usuario, telefono_usuario, id_estado, id_rol) VALUES
('U001', 'juan.perez@example.com', 'HASH123', 'Juan Pérez', '77770001', 'ACT', 'CMP'),
('U002', 'maria.garcia@example.com', 'HASH124', 'María García', '77770002', 'ACT', 'VEN'),
('U003', 'carlos.lopez@example.com', 'HASH125', 'Carlos López', '77770003', 'INA', 'CMP'),
('U004', 'laura.mendez@example.com', 'HASH126', 'Laura Méndez', '77770004', 'ACT', 'VEN'),
('U005', 'pedro.rojas@example.com', 'HASH127', 'Pedro Rojas', '77770005', 'ACT', 'CMP'),
('U006', 'sofia.martinez@example.com', 'HASH128', 'Sofía Martínez', '77770006', 'ACT', 'VEN'),
('U007', 'andres.ramirez@example.com', 'HASH129', 'Andrés Ramírez', '77770007', 'INA', 'CMP'),
('U008', 'camila.fuentes@example.com', 'HASH130', 'Camila Fuentes', '77770008', 'ACT', 'CMP'),
('U009', 'ricardo.diaz@example.com', 'HASH131', 'Ricardo Díaz', '77770009', 'ACT', 'VEN'),
('U010', 'valentina.gomez@example.com', 'HASH132', 'Valentina Gómez', '77770010', 'ACT', 'CMP'),
('U011', 'miguel.ortiz@example.com', 'HASH133', 'Miguel Ortiz', '77770011', 'ACT', 'VEN'),
('U012', 'ana.torres@example.com', 'HASH134', 'Ana Torres', '77770012', 'ACT', 'CMP'),
('U013', 'fernando.vargas@example.com', 'HASH135', 'Fernando Vargas', '77770013', 'INA', 'VEN'),
('U014', 'lucia.santos@example.com', 'HASH136', 'Lucía Santos', '77770014', 'ACT', 'CMP'),
('U015', 'jorge.castro@example.com', 'HASH137', 'Jorge Castro', '77770015', 'ACT', 'VEN'),
('U016', 'paula.reyes@example.com', 'HASH138', 'Paula Reyes', '77770016', 'ACT', 'CMP'),
('U017', 'sebastian.vera@example.com', 'HASH139', 'Sebastián Vera', '77770017', 'ACT', 'CMP'),
('U018', 'martina.pineda@example.com', 'HASH140', 'Martina Pineda', '77770018', 'INA', 'VEN'),
('U019', 'felipe.silva@example.com', 'HASH141', 'Felipe Silva', '77770019', 'ACT', 'ADM'),
('U020', 'carolina.morales@example.com', 'HASH142', 'Carolina Morales', '77770020', 'ACT', 'CMP');

select*from Usuario;
INSERT INTO Usuario (id_usuario, email_usuario, password_hash, nombre_usuario, telefono_usuario, id_estado, id_rol) VALUES
('Holi', 'Hola Mundo', 'HASH142', 'Carolina Morales', '77770020', 'ACT', 'CMP');