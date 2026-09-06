using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionFinanzasAPI.Models;

public partial class GestionFinanzasContext : DbContext
{
    public GestionFinanzasContext()
    {
    }

    public GestionFinanzasContext(DbContextOptions<GestionFinanzasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Meta> Metas { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.EstadoActivoCategoria).HasColumnName("estado_activo_categoria");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_categoria");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Categorias_Usuarios");
        });

        modelBuilder.Entity<Meta>(entity =>
        {
            entity.HasKey(e => e.IdMeta);

            entity.Property(e => e.IdMeta).HasColumnName("id_meta");
            entity.Property(e => e.EstadoActivoMeta).HasColumnName("estado_activo_meta");
            entity.Property(e => e.FechaLimite).HasColumnName("fecha_limite");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.MontoObjetivo)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("monto_objetivo");
            entity.Property(e => e.NombreMeta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_meta");
            entity.Property(e => e.SaldoActual)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("saldo_actual");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Meta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Metas_Usuarios");
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion);

            entity.Property(e => e.IdTransaccion).HasColumnName("id_transaccion");
            entity.Property(e => e.DescripcionTransaccion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion_transaccion");
            entity.Property(e => e.EstadoActivoTransaccion).HasColumnName("estado_activo_transaccion");
            entity.Property(e => e.FechaTransaccion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_transaccion");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.TipoTransaccion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_transaccion");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacciones_Categorias");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacciones_Usuarios");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.ApellidoUsuario)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("apellido_usuario");
            entity.Property(e => e.ContraseñaUsuario)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contraseña_usuario");
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo_usuario");
            entity.Property(e => e.EstadoActivoUsuario).HasColumnName("estado_activo_usuario");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_identificacion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
