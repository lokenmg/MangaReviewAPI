using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace interfacesAPI.Models;

public partial class InterfacesContext : DbContext
{
    public InterfacesContext()
    {
    }

    public InterfacesContext(DbContextOptions<InterfacesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calificacione> Calificaciones { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Manga> Mangas { get; set; }

    public virtual DbSet<MangaGenero> MangaGeneros { get; set; }

    public virtual DbSet<Resenium> Resenia { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-AI4KSMG\\SQLEXPRESS03; DataBase=interfaces; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calificacione>(entity =>
        {
            entity.HasKey(e => e.IdCalificacion).HasName("PK__califica__38CEF35C577AB794");

            entity.ToTable("calificaciones");

            entity.Property(e => e.IdCalificacion).HasColumnName("id_calificacion");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.Manga).HasColumnName("manga");

            entity.HasOne(d => d.MangaNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.Manga)
                .HasConstraintName("FK__calificac__manga__34C8D9D1");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__generos__99A8E4F9C679236A");

            entity.ToTable("generos");

            entity.Property(e => e.IdGenero).HasColumnName("id_genero");
            entity.Property(e => e.Genero1)
                .HasMaxLength(255)
                .HasColumnName("genero");
        });

        modelBuilder.Entity<Manga>(entity =>
        {
            entity.HasKey(e => e.IdManga).HasName("PK__mangas__73434B30BC6E0560");

            entity.ToTable("mangas");

            entity.Property(e => e.IdManga).HasColumnName("id_manga");
            entity.Property(e => e.AñoSalida)
                .HasMaxLength(255)
                .HasColumnName("año_salida");
            entity.Property(e => e.Dislikes)
                .HasDefaultValue(0)
                .HasColumnName("dislikes");
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .HasColumnName("estado");
            entity.Property(e => e.Likes)
                .HasDefaultValue(0)
                .HasColumnName("likes");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Portada)
                .HasMaxLength(255)
                .HasColumnName("portada");
            entity.Property(e => e.Sinopsis)
                .HasColumnType("text")
                .HasColumnName("sinopsis");
            entity.Property(e => e.Tomo)
                .HasMaxLength(255)
                .HasColumnName("tomo");
        });

        modelBuilder.Entity<MangaGenero>(entity =>
        {
            entity.HasKey(e => e.IdMangagenero).HasName("PK__manga_ge__3B6E86EF594FBC73");

            entity.ToTable("manga_generos");

            entity.Property(e => e.IdMangagenero).HasColumnName("id_mangagenero");
            entity.Property(e => e.Genero).HasColumnName("genero");
            entity.Property(e => e.Manga).HasColumnName("manga");

            entity.HasOne(d => d.GeneroNavigation).WithMany(p => p.MangaGeneros)
                .HasForeignKey(d => d.Genero)
                .HasConstraintName("FK__manga_gen__gener__30F848ED");

            entity.HasOne(d => d.MangaNavigation).WithMany(p => p.MangaGeneros)
                .HasForeignKey(d => d.Manga)
                .HasConstraintName("FK__manga_gen__manga__31EC6D26");
        });

        modelBuilder.Entity<Resenium>(entity =>
        {
            entity.HasKey(e => e.IdResenia).HasName("PK__resenia__4ECC37049C3F7C77");

            entity.ToTable("resenia");

            entity.Property(e => e.IdResenia).HasColumnName("id_resenia");
            entity.Property(e => e.Dislike)
                .HasDefaultValue(0)
                .HasColumnName("dislike");
            entity.Property(e => e.Likes)
                .HasDefaultValue(0)
                .HasColumnName("likes");
            entity.Property(e => e.Manga).HasColumnName("manga");
            entity.Property(e => e.Reseña)
                .HasColumnType("text")
                .HasColumnName("reseña");
            entity.Property(e => e.Usuario).HasColumnName("usuario");

            entity.HasOne(d => d.MangaNavigation).WithMany(p => p.Resenia)
                .HasForeignKey(d => d.Manga)
                .HasConstraintName("FK__resenia__manga__33D4B598");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Resenia)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK__resenia__usuario__32E0915F");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuarios).HasName("PK__usuarios__854B73B394AD526B");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUsuarios).HasColumnName("id_usuarios");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .HasColumnName("contraseña");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .HasColumnName("img");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
