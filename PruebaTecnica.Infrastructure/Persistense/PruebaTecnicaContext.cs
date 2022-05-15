using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PruebaTecnica.Core;

#nullable disable

namespace PruebaTecnica.Infrastructure
{
    public partial class PruebaTecnicaContext : DbContext
    {
        public PruebaTecnicaContext()
        {
        }

        public PruebaTecnicaContext(DbContextOptions<PruebaTecnicaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Cuentum> Cuenta { get; set; }
        public virtual DbSet<Movimiento> Movimientos { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=localhost,1433; Database=PruebaTecnica; User=sa; Password=abc123..;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.HasIndex(e => e.PersonaId, "Cliente_UN")
                    .IsUnique();

                entity.Property(e => e.Contrasenia)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Persona)
                    .WithOne(p => p.Cliente)
                    .HasForeignKey<Cliente>(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Cliente_FK");
            });

            modelBuilder.Entity<Cuentum>(entity =>
            {
                entity.HasKey(e => e.CuentaId)
                    .HasName("Cuenta_PK");

                entity.Property(e => e.NumeroCuenta)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.SaldoInicial).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.TipoCuenta)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Cuenta_FK");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.MovimientosId)
                    .HasName("Movimientos_PK");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Saldo).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.TipoMovimiento)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Valor).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Cuenta)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.CuentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Movimientos_FK");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.HasIndex(e => e.Identificacion, "Persona_UN")
                    .IsUnique();

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Edad).HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
