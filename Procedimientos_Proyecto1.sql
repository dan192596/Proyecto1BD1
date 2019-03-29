use Proyecto1;

-------------------------------------------------------------------------------------------------------------------
------------------------------------------------ ESTUDIANTE -------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------

--Se env�an los datos de un estudiante y los almacena en la base de datos
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

--Devuelve la lista de todos los estudiantes que est�n almacenados en la base de datos
CREATE PROCEDURE VER_ESTUDIANTES		
AS
BEGIN
	SELECT * FROM estudiante
END

EXEC INSERCION_ESTUDIANTE 201404232, 'Joss', 'Alvarez', '30904112','54148078','villa nueva','j_a@gmail.com','12-14-1994','13456','c://','789'

--Comprobacion de Login de un maestro
CREATE PROCEDURE LOGIN_ESTUDIANTE(
	@carnet int,
	@password varchar(255)
)AS
BEGIN
	IF EXISTS (SELECT 1 FROM estudiante E WHERE E.carnet = @carnet AND E.password = @password)
	BEGIN
		SELECT E.carnet, E.nombre, E.apellido, E.fotografia FROM estudiante E WHERE E.carnet = @carnet AND E.password = @password
	END
END

SELECT * FROM estudiante
EXEC LOGIN_ESTUDIANTE 20140,'789'
drop procedure LOGIN_ESTUDIANTE
-------------------------------------------------------------------------------------------------------------------
--------------------------------------------------- MAESTRO -------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------

--Se inserta un maestro y los almacena en la base de datos
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

CREATE PROCEDURE LOGIN_MAESTRO(
	@registro int,
	@password varchar(255)
)AS
BEGIN
	IF EXISTS (SELECT M.registro FROM maestro M WHERE M.registro = @registro AND M.password = @password)
	BEGIN
		SELECT M.registro, M.nombre, M.apellido, M.fotografia FROM maestro M WHERE M.registro = @registro AND M.password = @password
	END
END

--Devuelve la lista de todos los maestros y todos sus datos que est�n almacenados en la base de datos
CREATE PROCEDURE VER_MAESTROS		
AS
BEGIN
	SELECT * FROM maestro
END

--Se muestra los cursos y la carrera de esos cursos que imparte un maestro enviando como parámetro el registro del maestro
CREATE PROCEDURE VER_CURSOS_CARRERA_MAESTRO(
	@registro_maestro int
)AS
BEGIN
	SELECT U.nombre, C.nombre FROM curso U
	INNER JOIN maestro_carrera R 
			   ON U.curso = R.curso
	INNER JOIN carrera C
			   ON C.carrera = R.carrera
	WHERE R.registro = @registro_maestro
END

--Mostrar carreras en las que imparte cursos cierto maestro
CREATE PROCEDURE VER_CARRERAS_MAESTRO(
	@registro_maestro int
)AS
BEGIN
	SELECT C.nombre FROM carrera C
	INNER JOIN maestro_carrera R
			   ON C.carrera = R.carrera
    WHERE R.registro = @registro_maestro
END

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
--------------------------------------------------- CURSO -------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
--Almacenamiento de un Curso en la base de datos.
CREATE PROCEDURE INSERCION_CURSO(
	@nombre varchar(255)
)AS
BEGIN
	INSERT INTO curso(nombre)
	VALUES(@nombre)
END

--Ver listado de cursos 
CREATE PROCEDURE VER_CURSOS	
AS
BEGIN
	SELECT * FROM curso
END

--Mostrar los cursos que se imparten en cierta carrera
CREATE PROCEDURE VER_CURSOS_CARRERA(
	@carrera int
)AS
BEGIN
	SELECT C.nombre FROM curso C
	INNER JOIN maestro_carrera R
			   ON C.curso = R.curso
	WHERE R.carrera = @carrera
END

-------------------------------------------------------------------------------------------------------------------
----------------------------------------------- MAESTRO_CARRERA ---------------------------------------------------
-------------------------------------------------------------------------------------------------------------------

----Asignarle a un maestro el curso y la carrera en la que dar� en curso
CREATE PROCEDURE ASIGNAR_MAETRO_CARRERA_CURSO(
	@registro int,				--registro del maestro
	@carrera int,				--carrera
	@curso int					--curso 	
)AS
BEGIN
	INSERT INTO maestro_carrera(registro, carrera, curso)
	VALUES(@registro, @carrera, @curso)
END

--EXEC ASIGNAR_MAETRO_CARRERA_CURSO 123, 6, 1
--EXEC ASIGNAR_MAETRO_CARRERA_CURSO 123, 7, 2
--EXEC ASIGNAR_MAETRO_CARRERA_CURSO 456, 7, 3

-------------------------------------------------------------------------------------------------------------------
-------------------------------------------------- TIPO_AVISO -----------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
--Inserci�n de un tipo de aviso
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
--Creaci�n de un Aviso en la base de datos.
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


----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------- ACTIVIDAD -------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------
--Creacion de una Actividad en la base de datos
CREATE PROCEDURE INSERCION_ACTIVIDAD(
	@titulo varchar(255),
	@descripcion_actividad varchar(255),
	@fecha_publicacion datetime,
	@valor int,
	@fecha_limite datetime,
	@registro int,
	@carrera int,
	@curso int,
	@descripcion_publicacion varchar(255)
)AS
BEGIN
	DECLARE
		@publicacion int

	EXEC @publicacion = INSERCION_PUBLICACION @descripcion_publicacion, @fecha_publicacion

	INSERT INTO actividad(titulo, descripcion, fecha_publicacion, valor, fecha_limite, registro, carrera, curso, publicacion)
	VALUES(@titulo, @descripcion_actividad, @fecha_limite, @registro, @carrera,@curso, @publicacion)
END

--Ver todas las actividades publicadas, el maestro que lo publico, la carrera a la que va dirigido y el curso
CREATE PROCEDURE VER_ACTIVIDADES_PUBLICADAS
AS
BEGIN
	SELECT A.titulo, A.descripcion, A.fecha_publicacion, A.valor, A.fecha_limite, M.nombre, M.apellido, C.nombre, K.nombre
	FROM actividad A, maestro M, curso C, carrera K
	WHERE A.registro = M.registro
	AND A.curso = C.curso
	AND A.carrera = K.carrera
END

--########### Ver las actividades que ha publicado cierto maestro

-------------------------------------------------------------------------------------------------------------------
----------------------------------------------------- DOCUMENTO -------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
--Creación de un Documento o Material de Apoyo a la base de datos
CREATE PROCEDURE INSERCION_DOCUMENTO(
	@titulo varchar(255),
	@descripcion_documento varchar(255),
	@fecha_publicacion datetime,
	@registro int,
	@carrera int,
	@curso int,
	@descripcion_publicacion varchar(255)
)AS
BEGIN
	DECLARE
		@publicacion int

	EXEC @publicacion = INSERCION_PUBLICACION @descripcion_publicacion, @fecha_publicacion

	INSERT INTO documento(titulo, descripcion, registro, carrera, curso, publicacion)
	VALUES(@titulo, @descripcion_documento, @registro, @carrera, @curso, @publicacion)
END

--Ver todos el material de apoyo publicado y el maestro que lo publicó, la carrera a la que va dirigido y el curso
CREATE PROCEDURE VER_DOCUMENTOS_PUBLICADOS
AS
BEGIN
	SELECT D.titulo, D.descripcion, M.nombre, M.apellido, C.nombre, A.nombre FROM documento D, maestro M, curso C, carrera A
	WHERE D.registro = M.registro
	AND D.curso = C.curso
	AND D.carrera = A.carrera
END

--Eliminar Material de apoyo enviando como parámetro el identificador
CREATE PROCEDURE ELIMINAR_DOCUMENTO(
	@documento int
)AS
BEGIN
	DELETE FROM documento
	WHERE documento = @documento
END










--CREATE PROCEDURE ASIGNAR_MAESTRO_CARRERA(
--	@nombre_maestro varchar(255),
--	@apellido_maestro varchar(255),
--	@nombre_carrera varchar(255)
--)A
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

----Obteniendo el c�digo de la carrera a travez de su nombre
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