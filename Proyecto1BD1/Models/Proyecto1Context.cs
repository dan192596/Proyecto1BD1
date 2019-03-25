using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proyecto1BD1.Models
{
    public partial class Proyecto1Context : DbContext
    {
        public Proyecto1Context()
        {
        }

        public Proyecto1Context(DbContextOptions<Proyecto1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividad { get; set; }
        public virtual DbSet<Aviso> Aviso { get; set; }
        public virtual DbSet<Carrera> Carrera { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<CursoEstudiante> CursoEstudiante { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<Estudiante> Estudiante { get; set; }
        public virtual DbSet<EstuResp> EstuResp { get; set; }
        public virtual DbSet<Examen> Examen { get; set; }
        public virtual DbSet<Maestro> Maestro { get; set; }
        public virtual DbSet<MaestroCarrera> MaestroCarrera { get; set; }
        public virtual DbSet<NotaZona> NotaZona { get; set; }
        public virtual DbSet<ParticipacionExamen> ParticipacionExamen { get; set; }
        public virtual DbSet<Pregunta> Pregunta { get; set; }
        public virtual DbSet<Publicacion> Publicacion { get; set; }
        public virtual DbSet<Respuesta> Respuesta { get; set; }
        public virtual DbSet<TipoAviso> TipoAviso { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Proyecto1;Trusted_Connection=True;User ID=bases1;Password=123456789;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => e.Actividad1);

                entity.ToTable("actividad");

                entity.Property(e => e.Actividad1).HasColumnName("actividad");

                entity.Property(e => e.Curso).HasColumnName("curso");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaLimite)
                    .HasColumnName("fecha_limite")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaPublicacion)
                    .HasColumnName("fecha_publicacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.Publicacion).HasColumnName("publicacion");

                entity.Property(e => e.Titulo)
                    .HasColumnName("titulo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Valor).HasColumnName("valor");

                entity.HasOne(d => d.CursoNavigation)
                    .WithMany(p => p.Actividad)
                    .HasForeignKey(d => d.Curso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("actividad_curso_fk");

                entity.HasOne(d => d.PublicacionNavigation)
                    .WithMany(p => p.Actividad)
                    .HasForeignKey(d => d.Publicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("actividad_publicacion_fk");
            });

            modelBuilder.Entity<Aviso>(entity =>
            {
                entity.HasKey(e => e.Aviso1);

                entity.ToTable("aviso");

                entity.Property(e => e.Aviso1).HasColumnName("aviso");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaHora)
                    .HasColumnName("fecha_hora")
                    .HasColumnType("datetime");

                entity.Property(e => e.Publicacion).HasColumnName("publicacion");

                entity.Property(e => e.TipoAviso).HasColumnName("tipo_aviso");

                entity.Property(e => e.Titulo)
                    .HasColumnName("titulo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.PublicacionNavigation)
                    .WithMany(p => p.Aviso)
                    .HasForeignKey(d => d.Publicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("aviso_publicacion_fk");

                entity.HasOne(d => d.TipoAvisoNavigation)
                    .WithMany(p => p.Aviso)
                    .HasForeignKey(d => d.TipoAviso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("aviso_tipo_aviso_fk");
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.HasKey(e => e.Carrera1);

                entity.ToTable("carrera");

                entity.Property(e => e.Carrera1).HasColumnName("carrera");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.Curso1);

                entity.ToTable("curso");

                entity.Property(e => e.Curso1).HasColumnName("curso");

                entity.Property(e => e.Carrera).HasColumnName("carrera");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Registro).HasColumnName("registro");

                entity.HasOne(d => d.MaestroCarrera)
                    .WithMany(p => p.Curso)
                    .HasForeignKey(d => new { d.Registro, d.Carrera })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("curso_maestro_carrera_fk");
            });

            modelBuilder.Entity<CursoEstudiante>(entity =>
            {
                entity.HasKey(e => new { e.Carnet, e.Curso });

                entity.ToTable("curso_estudiante");

                entity.Property(e => e.Carnet).HasColumnName("carnet");

                entity.Property(e => e.Curso).HasColumnName("curso");

                entity.HasOne(d => d.CarnetNavigation)
                    .WithMany(p => p.CursoEstudiante)
                    .HasForeignKey(d => d.Carnet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("curso_estudiante_estudiante_fk");

                entity.HasOne(d => d.CursoNavigation)
                    .WithMany(p => p.CursoEstudiante)
                    .HasForeignKey(d => d.Curso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("curso_estudiante_curso_fk");
            });

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.HasKey(e => e.Documento1);

                entity.ToTable("documento");

                entity.Property(e => e.Documento1).HasColumnName("documento");

                entity.Property(e => e.Curso).HasColumnName("curso");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Publicacion).HasColumnName("publicacion");

                entity.Property(e => e.Titulo)
                    .HasColumnName("titulo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.CursoNavigation)
                    .WithMany(p => p.Documento)
                    .HasForeignKey(d => d.Curso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("documento_curso_fk");

                entity.HasOne(d => d.PublicacionNavigation)
                    .WithMany(p => p.Documento)
                    .HasForeignKey(d => d.Publicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("documento_publicacion_fk");
            });

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.Carnet);

                entity.ToTable("estudiante");

                entity.Property(e => e.Carnet)
                    .HasColumnName("carnet")
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellido)
                    .HasColumnName("apellido")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasColumnName("correo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fecha_nacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.Fotografia)
                    .HasColumnName("fotografia")
                    .HasColumnType("image");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPartida)
                    .HasColumnName("numero_partida")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(1);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoTutor)
                    .HasColumnName("telefono_tutor")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstuResp>(entity =>
            {
                entity.HasKey(e => new { e.Respuesta, e.Pregunta, e.Examen, e.Carnet });

                entity.ToTable("estu_resp");

                entity.Property(e => e.Respuesta).HasColumnName("respuesta");

                entity.Property(e => e.Pregunta).HasColumnName("pregunta");

                entity.Property(e => e.Examen).HasColumnName("examen");

                entity.Property(e => e.Carnet).HasColumnName("carnet");

                entity.HasOne(d => d.CarnetNavigation)
                    .WithMany(p => p.EstuResp)
                    .HasForeignKey(d => d.Carnet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("estu_resp_estudiante_fk");

                entity.HasOne(d => d.RespuestaNavigation)
                    .WithMany(p => p.EstuResp)
                    .HasForeignKey(d => new { d.Respuesta, d.Pregunta, d.Examen })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("estu_resp_respuesta_fk");
            });

            modelBuilder.Entity<Examen>(entity =>
            {
                entity.HasKey(e => e.Examen1);

                entity.ToTable("examen");

                entity.Property(e => e.Examen1).HasColumnName("examen");

                entity.Property(e => e.Curso).HasColumnName("curso");

                entity.Property(e => e.FechaHoraFinal)
                    .HasColumnName("fecha_hora_final")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaHoraInicio)
                    .HasColumnName("fecha_hora_inicio")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CursoNavigation)
                    .WithMany(p => p.Examen)
                    .HasForeignKey(d => d.Curso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("examen_curso_fk");
            });

            modelBuilder.Entity<Maestro>(entity =>
            {
                entity.HasKey(e => e.Registro);

                entity.ToTable("maestro");

                entity.Property(e => e.Registro)
                    .HasColumnName("registro")
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellido)
                    .HasColumnName("apellido")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasColumnName("correo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Dpi).HasColumnName("dpi");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fecha_nacimiento")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fotografia)
                    .HasColumnName("fotografia")
                    .HasColumnType("image");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(1);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MaestroCarrera>(entity =>
            {
                entity.HasKey(e => new { e.Registro, e.Carrera });

                entity.ToTable("maestro_carrera");

                entity.Property(e => e.Registro).HasColumnName("registro");

                entity.Property(e => e.Carrera).HasColumnName("carrera");

                entity.HasOne(d => d.CarreraNavigation)
                    .WithMany(p => p.MaestroCarrera)
                    .HasForeignKey(d => d.Carrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maestro_carrera_carrera_fk");

                entity.HasOne(d => d.RegistroNavigation)
                    .WithMany(p => p.MaestroCarrera)
                    .HasForeignKey(d => d.Registro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maestro_carrera_maestro_fk");
            });

            modelBuilder.Entity<NotaZona>(entity =>
            {
                entity.HasKey(e => new { e.Actividad, e.Curso, e.Carnet });

                entity.ToTable("nota_zona");

                entity.Property(e => e.Actividad).HasColumnName("actividad");

                entity.Property(e => e.Curso).HasColumnName("curso");

                entity.Property(e => e.Carnet).HasColumnName("carnet");

                entity.Property(e => e.Nota)
                    .HasColumnName("nota")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Observacion)
                    .HasColumnName("observacion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.ActividadNavigation)
                    .WithMany(p => p.NotaZona)
                    .HasForeignKey(d => d.Actividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("nota_zona_actividad_fk");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.NotaZona)
                    .HasForeignKey(d => new { d.Carnet, d.Curso })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("nota_zona_curso_estudiante_fk");
            });

            modelBuilder.Entity<ParticipacionExamen>(entity =>
            {
                entity.HasKey(e => new { e.Examen, e.Curso, e.Carnet });

                entity.ToTable("participacion_examen");

                entity.Property(e => e.Examen).HasColumnName("examen");

                entity.Property(e => e.Curso).HasColumnName("curso");

                entity.Property(e => e.Carnet).HasColumnName("carnet");

                entity.Property(e => e.Nota)
                    .HasColumnName("nota")
                    .HasColumnType("numeric(5, 2)");

                entity.HasOne(d => d.ExamenNavigation)
                    .WithMany(p => p.ParticipacionExamen)
                    .HasForeignKey(d => d.Examen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("participacion_examen_examen_fk");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.ParticipacionExamen)
                    .HasForeignKey(d => new { d.Carnet, d.Curso })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("participacion_examen_curso_estudiante_fk");
            });

            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.HasKey(e => new { e.Pregunta1, e.Examen });

                entity.ToTable("pregunta");

                entity.Property(e => e.Pregunta1)
                    .HasColumnName("pregunta")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Examen).HasColumnName("examen");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExamenNavigation)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.Examen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pregunta_examen_fk");
            });

            modelBuilder.Entity<Publicacion>(entity =>
            {
                entity.HasKey(e => e.Publicacion1);

                entity.ToTable("publicacion");

                entity.Property(e => e.Publicacion1).HasColumnName("publicacion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FecaHora)
                    .HasColumnName("feca_hora")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Respuesta>(entity =>
            {
                entity.HasKey(e => new { e.Respuesta1, e.Pregunta, e.Examen });

                entity.ToTable("respuesta");

                entity.Property(e => e.Respuesta1)
                    .HasColumnName("respuesta")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Pregunta).HasColumnName("pregunta");

                entity.Property(e => e.Examen).HasColumnName("examen");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.PreguntaNavigation)
                    .WithMany(p => p.Respuesta)
                    .HasForeignKey(d => new { d.Pregunta, d.Examen })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("respuesta_pregunta_fk");
            });

            modelBuilder.Entity<TipoAviso>(entity =>
            {
                entity.HasKey(e => e.TipoAviso1);

                entity.ToTable("tipo_aviso");

                entity.Property(e => e.TipoAviso1).HasColumnName("tipo_aviso");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
