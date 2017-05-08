using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BancaTec
{
    public partial class BancaTecContext : DbContext
    {
        public virtual DbSet<Asesor> Asesor { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<Prestamo> Prestamo { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Tarjeta> Tarjeta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=BancaTec;Username=postgres;Password=bases2017");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asesor>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK_ASESOR");

                entity.ToTable("ASESOR");

                entity.Property(e => e.Cedula)
                    .HasColumnType("bpchar")
                    .HasMaxLength(9);

                entity.Property(e => e.Estado).HasColumnType("char");

                entity.Property(e => e.FechaNac).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(15);

                entity.Property(e => e.PriApellido)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(15);

                entity.Property(e => e.SegApellido)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(15);

                entity.Property(e => e.SegNombre)
                    .HasColumnType("varchar")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK_CLIENTE");

                entity.ToTable("CLIENTE");

                entity.Property(e => e.Cedula)
                    .HasColumnType("bpchar")
                    .HasMaxLength(9);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.Estado).HasColumnType("char");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.PriApellido)
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.SegApellido)
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.SegundoNombre)
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasColumnName("Telefono ")
                    .HasColumnType("bpchar")
                    .HasMaxLength(8);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.NumCuenta)
                    .HasName("PK_CUENTA");

                entity.ToTable("CUENTA");

                entity.Property(e => e.CedCliente)
                    .HasColumnType("varchar")
                    .HasMaxLength(9);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.Estado).HasColumnType("char");

                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(7);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(9);

                entity.HasOne(d => d.CedClienteNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.CedCliente)
                    .HasConstraintName("Cliente");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.NumPrestamo)
                    .HasName("PK_PAGO");

                entity.ToTable("PAGO");

                entity.Property(e => e.NumPrestamo).ValueGeneratedNever();

                entity.Property(e => e.CedCliente)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(9);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(14);

                entity.HasOne(d => d.CedClienteNavigation)
                    .WithMany(p => p.Pago)
                    .HasForeignKey(d => d.CedCliente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("ClienteReference");

                entity.HasOne(d => d.NumPrestamoNavigation)
                    .WithOne(p => p.Pago)
                    .HasForeignKey<Pago>(d => d.NumPrestamo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("PrestamoReference");
            });

            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.HasKey(e => e.Numero)
                    .HasName("PK_PRESTAMO");

                entity.ToTable("PRESTAMO");

                entity.Property(e => e.CedAsesor)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(9);

                entity.Property(e => e.CedCliente)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(9);

                entity.Property(e => e.Estado).HasColumnType("char");

                entity.HasOne(d => d.CedAsesorNavigation)
                    .WithMany(p => p.Prestamo)
                    .HasForeignKey(d => d.CedAsesor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("AsesorReference");

                entity.HasOne(d => d.CedClienteNavigation)
                    .WithMany(p => p.Prestamo)
                    .HasForeignKey(d => d.CedCliente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("ClienteReference");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.Nombre)
                    .HasName("PK_ROL");

                entity.ToTable("ROL");

                entity.Property(e => e.Nombre)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Tarjeta>(entity =>
            {
                entity.HasKey(e => e.Numero)
                    .HasName("PK_TARJETA");

                entity.ToTable("TARJETA");

                entity.Property(e => e.CodigoSeg)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(4);

                entity.Property(e => e.Estado).HasColumnType("char");

                entity.Property(e => e.FechaExp).HasColumnType("date");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(7);

                entity.HasOne(d => d.NumCuentaNavigation)
                    .WithMany(p => p.Tarjeta)
                    .HasForeignKey(d => d.NumCuenta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("CuentaReference");
            });
        }
    }
}