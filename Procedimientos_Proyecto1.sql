use Proyecto1;

-------------------------------------------------------------------------------------------------------------------
------------------------------------------------ ESTUDIANTE -------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------

--Se envían los datos de un estudiante y los almacena en la base de datos
CREATE PROCEDURE INSERCION_ESTUDIANTE(   
	@carnet int, 
	@nombre varchar(255),
	@apellido varchar(255),
	@telefono varchar(255),
	@telefono_tutor varchar(255),
	@direccion varchar(255),
	@correo varchar(255),
	@fecha_nacimiento date,
	@numero_partida varchar(255),
	@fotografia varchar(255),
	@password varchar(255)
)AS
BEGIN
	DECLARE @X varbinary(1)
	Set @X = CAST(@password As varbinary(1))

	INSERT INTO estudiante(carnet, nombre, apellido, telefono, telefono_tutor, direccion, correo, fecha_nacimiento, numero_partida, fotografia, password)
	VALUES(@carnet, @nombre, @apellido, @telefono, @telefono_tutor, @direccion, @correo, @fecha_nacimiento, @numero_partida, @fotografia, @X)
END

--Devuelve la lista de todos los estudiantes que están almacenados en la base de datos
CREATE PROCEDURE VER_ESTUDIANTES		
AS
BEGIN
	SELECT * FROM estudiante
END

--Elimina un estudiante recibiendo un carnét como parámetro   *****NO EJECUTAR AÚN
--CREATE PROCEDURE ELIMINAR_ESTUDIANTE( 
--	@carnet int
--)AS
--BEGIN
--	DELETE FROM estudiante WHERE carnet = @carnet
--END

-------------------------------------------------------------------------------------------------------------------
--------------------------------------------------- MAESTRO -------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------

--Se envían los datos de un maestro y los almacena en la base de datos
CREATE PROCEDURE INSERCION_MAESTRO(
	@registro int,
	@dpi int,
	@nombre varchar(255),
	@apellido varchar(255),
	@telefono varchar(255),
	@direccion varchar(255),
	@correo varchar(255),
	@fecha_nacimiento date,
	@fotografia varchar(255),
	@password varchar(255)
)AS
BEGIN
	DECLARE @X varbinary(1)
	Set @X = CAST(@password As varbinary(1))

	INSERT INTO maestro(registro, dpi, nombre, apellido, telefono, direccion, correo, fecha_nacimiento, fotografia, password)
	VALUES(@registro, @dpi, @nombre, @apellido, @telefono, @direccion, @correo, @fecha_nacimiento, @fotografia, @X)
END

--Devuelve la lista de todos los maestros y todos sus datos que están almacenados en la base de datos
CREATE PROCEDURE VER_MAESTROS		
AS
BEGIN
	SELECT * FROM maestro
END

--Elimina un mestro de la base de datos recibiendo el registro

-------------------------------------------------------------------------------------------------------------------
--------------------------------------------------- CARRERA -------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------

--Almacenamiento de una Carrera en la base de datos.
CREATE PROCEDURE INSERCION_CARRERA(
	@nombre varchar(255),
	@descripcion varchar(255)
)AS
BEGIN
	INSERT INTO carrera(nombre,descripcion)
	VALUES(@nombre, @descripcion)
END

--Ver Listado de Carreras de Diversificado
CREATE PROCEDURE VER_CARRERAS		
AS
BEGIN
	SELECT * FROM carrera
END

-------------------------------------------------------------------------------------------------------------------
----------------------------------------------- MAESTRO_CARRERA ---------------------------------------------------
-------------------------------------------------------------------------------------------------------------------

----Asignarle a un maestro el curso y la carrera en la que dará en curso
CREATE PROCEDURE ASIGNAR_MAETRO_CARRERA_CURSO(
	@registro int,				--registro del maestro
	@carrera int,				--carrera
	@curso int					--curso 	
)AS
BEGIN
	INSERT INTO maestro_carrera(registro, carrera, curso)
	VALUES(@registro, @carrera, @curso)
END

-------------------------------------------------------------------------------------------------------------------
-------------------------------------------------- TIPO_AVISO -----------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
--Inserción de un tipo de aviso
CREATE PROCEDURE INSERCION_TIPO_AVISO(
	@descripcion varchar(255)
)AS
BEGIN
	INSERT INTO tipo_aviso(descripcion)
	VALUES(@descripcion)
END

-------------------------------------------------------------------------------------------------------------------
----------------------------------------------------- AVISO -------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
--Creación de un Aviso en la base de datos.
CREATE PROCEDURE INSERCION_AVISO(
	@titulo varchar(255),						--TITULO DEL AVISO
	@descripcion_publicacion varchar(255),		--DESCRIPCION DE LA PUBLICACION
	@fecha_hora datetime,						--Fecha y hora de publicacion
	@tipo_aviso int,							--Tipo de aviso
	@descripcion_aviso varchar(255)				--DESCRIPCION DEL AVISO
)AS
BEGIN
	DECLARE
		@publicacion int

	EXEC @publicacion = INSERCION_PUBLICACION @descripcion_publicacion, @fecha_hora

	INSERT INTO aviso(titulo, descripcion, fecha_hora, tipo_aviso, publicacion)
	VALUES(@titulo, @descripcion_aviso, @fecha_hora, @tipo_aviso, @publicacion)
END

--Inserta una publicacion en la base de datos cuando una actividad, documento o aviso son publicados
CREATE PROCEDURE INSERCION_PUBLICACION(
	@descripcion varchar(255),
	@fecha_hora datetime
)AS
BEGIN
	DECLARE
		@publicacion int

	INSERT INTO publicacion(descripcion, feca_hora)
	VALUES(@descripcion, @fecha_hora)

	SELECT @publicacion = SCOPE_IDENTITY()
	RETURN @publicacion
END


-------------------------------------------------------------------------------------------------------------------
----------------------------------------------------- ACTIVIDAD -------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
--Creacion de una Actividad en la base de datos
CREATE PROCEDURE INSERCION_ACTIVIDAD(
	
)AS
BEGIN
	DECLARE
		@publicacion int

	EXEC @publicacion = INSERCION_PUBLICACION @descripcion_publicacion, @fecha_hora

	
END





--CREATE PROCEDURE ASIGNAR_MAESTRO_CARRERA(
--	@nombre_maestro varchar(255),
--	@apellido_maestro varchar(255),
--	@nombre_carrera varchar(255)
--)AS
--BEGIN
--	DECLARE
--		@registro int,
--		@carrera int

--	EXEC @registro = GET_REGISTRO_MAESTRO @nombre_maestro, @apellido_maestro
--	EXEC @carrera = GET_CODIGO_CARRERA @nombre_carrera

--	INSERT INTO maestro_carrera(registro, carrera)
--	VALUES(@registro, @carrera)
--END

----Obteniendo el Registro de un maestro a travez de su nombre y apellido
--CREATE PROCEDURE GET_REGISTRO_MAESTRO(
--	@nombre varchar(255),
--	@apellido varchar(255)
--)AS
--BEGIN
--	DECLARE
--		@ID int

--	SELECT @ID = registro FROM maestro
--	WHERE nombre = @nombre AND apellido = @apellido
--	RETURN @ID
--END

----Obteniendo el código de la carrera a travez de su nombre
--CREATE PROCEDURE GET_CODIGO_CARRERA(
--	@nombre varchar(255)
--)AS
--BEGIN
--	DECLARE
--		@CARRERA int

--	SELECT @CARRERA = carrera FROM carrera
--	WHERE nombre = @nombre
--	RETURN @CARRERA
--END


--drop table participacion_examen
--drop table nota_zona
--drop table estu_resp
--drop table estu_resp
--drop table respuesta
--drop table pregunta
--drop table examen
--drop table estudiante
--drop table documento
--drop table aviso
--drop table actividad
--drop table tipo_aviso
--drop table curso_estudiante
--drop table publicacion
--drop table curso
--drop table maestro_carrera
--drop table maestro
--drop table carrera