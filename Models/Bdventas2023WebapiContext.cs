using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCCRUD.Models;

public partial class Bdventas2023WebapiContext : DbContext
{
    public Bdventas2023WebapiContext()
    {
    }

    public Bdventas2023WebapiContext(DbContextOptions<Bdventas2023WebapiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbArticulo> TbArticulos { get; set; }

    public virtual DbSet<TbArticulosBaja> TbArticulosBajas { get; set; }

    public virtual DbSet<TbArticulosLiquidacion> TbArticulosLiquidacions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=localhost;Database=BDVENTAS2023_WEBAPI;User Id=sa;Password=123456aA!;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AI");

        modelBuilder.Entity<TbArticulo>(entity =>
        {
            entity.HasKey(e => e.CodArt).HasName("pk_tb_articulos");

            entity.ToTable("tb_articulos");

            entity.Property(e => e.CodArt)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cod_art");
            entity.Property(e => e.DeBaja)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("('No')")
                .IsFixedLength()
                .HasColumnName("de_baja");
            entity.Property(e => e.NomArt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom_art");
            entity.Property(e => e.PreArt)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("pre_art");
            entity.Property(e => e.StkArt).HasColumnName("stk_art");
            entity.Property(e => e.UniMed)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("uni_med");
        });

        modelBuilder.Entity<TbArticulosBaja>(entity =>
        {
            entity.HasKey(e => new { e.CodArt, e.FechaBaja }).HasName("pk_tb_articulos_baja");

            entity.ToTable("tb_articulos_baja");

            entity.Property(e => e.CodArt)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cod_art");
            entity.Property(e => e.FechaBaja)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("fecha_baja");

            entity.HasOne(d => d.CodArtNavigation).WithMany(p => p.TbArticulosBajas)
                .HasForeignKey(d => d.CodArt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_art_baja_cod_art");
        });

        modelBuilder.Entity<TbArticulosLiquidacion>(entity =>
        {
            entity.HasKey(e => e.NumReg).HasName("pk_tb_art_liquidacion");

            entity.ToTable("tb_articulos_liquidacion");

            entity.Property(e => e.NumReg).HasColumnName("num_reg");
            entity.Property(e => e.CodArt)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cod_art");
            entity.Property(e => e.PrecioLiquidar)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("precio_liquidar");
            entity.Property(e => e.UnidadesLiquidar).HasColumnName("unidades_liquidar");

            entity.HasOne(d => d.CodArtNavigation).WithMany(p => p.TbArticulosLiquidacions)
                .HasForeignKey(d => d.CodArt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_art_liqui_cod_art");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
