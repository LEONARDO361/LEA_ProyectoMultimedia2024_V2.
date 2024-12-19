using System;
using System.Collections.Generic;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Contexts;

public partial class GimnasioContext : DbContext
{
    public GimnasioContext()
    {
    }

    public GimnasioContext(DbContextOptions<GimnasioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Canasta> Canasta { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<Descuento> Descuento { get; set; }

    public virtual DbSet<DetalleCanasta> DetalleCanasta { get; set; }

    public virtual DbSet<DetalleOrden> DetalleOrden { get; set; }

    public virtual DbSet<DireccionEnvio> DireccionEnvio { get; set; }

    public virtual DbSet<LogUsuario> LogUsuario { get; set; }

    public virtual DbSet<MetodoPago> MetodoPago { get; set; }

    public virtual DbSet<Orden> Orden { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }

    public virtual DbSet<ReseñaProducto> ReseñaProducto { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= LAPTOP-7O2LALR3; Database= Gimnasio;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Canasta>(entity =>
        {
            entity.HasOne(d => d.Cliente).WithMany(p => p.Canasta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Canasta_Cliente");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK_Producto_Fuerza");

            entity.Property(e => e.Descripcion).IsFixedLength();
            entity.Property(e => e.Nombre).IsFixedLength();
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.Property(e => e.Apellido).IsFixedLength();
            entity.Property(e => e.CorreoElectronico).IsFixedLength();
            entity.Property(e => e.Direccion).IsFixedLength();
            entity.Property(e => e.Nombre).IsFixedLength();
            entity.Property(e => e.Sexo).IsFixedLength();

            entity.HasOne(d => d.LogUsuario).WithMany(p => p.Cliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_LogUsuario");
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.Property(e => e.TipoDescuento).IsFixedLength();
        });

        modelBuilder.Entity<DetalleCanasta>(entity =>
        {
            entity.Property(e => e.DetalleCanastaId).ValueGeneratedNever();
            entity.Property(e => e.SubTotal).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Canasta).WithMany(p => p.DetalleCanasta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCanasta_Canasta");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleCanasta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCanasta_Producto");
        });

        modelBuilder.Entity<DetalleOrden>(entity =>
        {
            entity.HasOne(d => d.Orden).WithMany(p => p.DetalleOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleOrden_Orden");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleOrden_Producto");
        });

        modelBuilder.Entity<DireccionEnvio>(entity =>
        {
            entity.Property(e => e.Ciudad).IsFixedLength();
            entity.Property(e => e.Dirección).IsFixedLength();
            entity.Property(e => e.Pais).IsFixedLength();
            entity.Property(e => e.Provincia).IsFixedLength();

            entity.HasOne(d => d.Cliente).WithMany(p => p.DireccionEnvio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DireccionEnvio_Cliente");
        });

        modelBuilder.Entity<LogUsuario>(entity =>
        {
            entity.Property(e => e.Contrasena).IsFixedLength();
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.Property(e => e.TipoTarjeta).IsFixedLength();

            entity.HasOne(d => d.Cliente).WithMany(p => p.MetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MetodoPago_Cliente");
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.Property(e => e.Estado).IsFixedLength();

            entity.HasOne(d => d.Cliente).WithMany(p => p.Orden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orden_Cliente1");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK_Productos");

            entity.Property(e => e.Descripcion).IsFixedLength();
            entity.Property(e => e.Estado).IsFixedLength();
            entity.Property(e => e.Marca).IsFixedLength();
            entity.Property(e => e.Nombre).IsFixedLength();
            entity.Property(e => e.Procedencia).IsFixedLength();

            entity.HasOne(d => d.Categoria).WithMany(p => p.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Categoria");

            entity.HasOne(d => d.Descuento).WithMany(p => p.Producto).HasConstraintName("FK_Producto_Descuento");
        });

        modelBuilder.Entity<ReseñaProducto>(entity =>
        {
            entity.Property(e => e.Comentario).IsFixedLength();

            entity.HasOne(d => d.Cliente).WithMany(p => p.ReseñaProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReseñaProducto_Cliente");

            entity.HasOne(d => d.Producto).WithMany(p => p.ReseñaProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReseñaProducto_Producto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
