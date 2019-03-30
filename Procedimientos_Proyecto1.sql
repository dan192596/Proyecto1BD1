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
	INSERT INTO estudiante(carnet, nombre, apellido, telefono, telefono_tutor, direccion, correo, fecha_nacimiento, numero_partida, fotografia, password)
	VALUES(@carnet, @nombre, @apellido, @telefono, @telefono_tutor, @direccion, @correo, @fecha_nacimiento, @numero_partida, @fotografia, @password)
END

--Devuelve la lista de todos los estudiantes que est�n almacenados en la base de datos
CREATE PROCEDURE VER_ESTUDIANTES		
AS
BEGIN
	SELECT * FROM estudiante
END

EXEC INSERCION_ESTUDIANTE 201403780, 'Amanda', 'Chavez', '30904112','54148078','villa nueva','nicoll@gmail.com','12-14-1994','13456','c://a.jpg','1234'

--Comprobacion de Login de un maestro
CREATE PROCEDURE LOGIN_ESTUDIANTE(
	@carnet int,
	@password varchar(255)
)AS
BEGIN
	IF EXISTS (SELECT E.carnet FROM estudiante E WHERE E.carnet = @carnet AND E.password = @password)
	BEGIN
		SELECT E.carnet, E.nombre, E.apellido, E.fotografia FROM estudiante E WHERE E.carnet = @carnet AND E.password = @password
	END
END

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
	INSERT INTO maestro(registro, dpi, nombre, apellido, telefono, direccion, correo, fecha_nacimiento, fotografia, password)
	VALUES(@registro, @dpi, @nombre, @apellido, @telefono, @direccion, @correo, @fecha_nacimiento, @fotografia, @password)
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

exec VER_CURSOS_CARRERA_MAESTRO 123

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

CREATE PROCEDURE VER_MAESTRO_CARRERA_CURSO
AS
BEGIN
	SELECT DISTINCT maestro_carrera.registro, maestro_carrera.carrera, maestro_carrera.curso, maestro.nombre, carrera.nombre, curso.cu
	FROM maestro_carrera, maestro,carrera,curso
	WHERE maestro_carrera.registro =maestro.registro
	AND maestro_carrera.carrera = carrera.carrera
	AND maestro_carrera.curso = curso.curso
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
--#####Creacion de una Actividad en la base de datos, Se crea una publicación y Ademas se asigna la zona en Nota_Zona
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
		@publicacion int,
		@actividad int

	EXEC @publicacion = INSERCION_PUBLICACION @descripcion_publicacion, @fecha_publicacion

	INSERT INTO actividad(titulo, descripcion, fecha_publicacion, valor, fecha_limite, registro, carrera, curso, publicacion)
	VALUES(@titulo, @descripcion_actividad, @fecha_publicacion, @valor, @fecha_limite, @registro, @carrera, @curso, @publicacion)

	SELECT @actividad = SCOPE_IDENTITY()

	INSERT INTO nota_zona(nota, observacion, actividad, carnet, registro, carrera, curso)
	SELECT 0,'',@actividad, C.carnet, C.registro, C.carrera, C.curso FROM curso_estudiante C
	WHERE @curso = C.curso
	AND @carrera = C.carrera
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

--########### Ver las actividades que ha publicado cierto maestro de cierta carrera de un curso
CREATE PROCEDURE VER_ACTIVIDADES_MAESTRO(
	@registro int,     --maestro que la publico
	@carrera int,	   --carrera
	@curso int		   --curso al que va dirigido
)AS
BEGIN
	SELECT A.titulo, A.fecha_publicacion, A.valor FROM actividad A
	WHERE A.registro = @registro
	AND A.curso = @curso
	AND A.carrera = @carrera
END

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

-------------------------------------------------------------------------------------------------------------------
----------------------------------------------- CURSO_ESTUDIANTE --------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
--########Asignar un estudiante a una carrera, se asignan automaticamente los cursos del estudiante en CURSO_ESTUDIANTE
CREATE PROCEDURE ASIGNAR_ESTUDIANTE_CARRERA(
	@carnet int,
	@carrera int
)AS
BEGIN
	INSERT INTO curso_estudiante(carnet, registro, carrera, curso)
	SELECT @carnet, M.registro, M.carrera, M.curso 
	FROM maestro_carrera M
	WHERE M.carrera = @carrera
END


CREATE PROCEDURE VER_ESTUDIANTE_CARRERA
AS
BEGIN
	SELECT DISTINCT curso_estudiante.carrera, curso_estudiante.carnet, carrera.nombre, estudiante.nombre
	FROM curso_estudiante,carrera,estudiante
	WHERE curso_estudiante.carrera = carrera.carrera
	AND curso_estudiante.carnet = estudiante.carnet
END

select * from estudiante

select * from curso_estudiante


-------------------------------------------------------------------------------------------------------------------
--------------------------------------------------- NOTA_ZONA -----------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
--####Ver notas de las actividades de un estudiante de cierto curso
CREATE PROCEDURE VER_NOTAS_ACTIVIDADES_CURSO(
	@carnet int,
	@curso int	
)AS
BEGIN
	SELECT A.titulo, A.valor, N.nota FROM actividad A, nota_zona N
	WHERE N.curso = @curso
	AND A.curso = @curso
	AND N.carnet = @carnet
END

-------------------------------------------------------------------------------------------------------------------
--------------------------------------------------- EXAMEN -----------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
--##Ingresando un examen a la base de datos
CREATE PROCEDURE INSERTAR_EXAMEN(
	@fecha_hora_inicio datetime,
	@fecha_hora_final datetime,
	@registro int,						--maestro que sube el examen (logueado)
	@carrera int,						--selecciona para qué carrera (en las carreras que imparte)
	@curso int							--selecciona para qué curso (de los que el maestro imparte)
)AS
BEGIN
	DECLARE 
		@examen int

	INSERT INTO examen(fecha_hora_inicio, fecha_hora_final, registro, carrera, curso)
	VALUES(@fecha_hora_inicio, @fecha_hora_final, @registro, @carrera, @curso)

	SELECT @examen = SCOPE_IDENTITY()
	RETURN @examen
END



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