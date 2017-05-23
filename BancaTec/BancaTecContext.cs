using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BancaTec
{
    public partial class BancaTecContext : DbContext
    {
        public virtual DbSet<Asesor> Asesor { get; set; }
        public virtual DbSet<CancelarTarjeta> CancelarTarjeta { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EmpleadoRol> EmpleadoRol { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<Prestamo> Prestamo { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Tarjeta> Tarjeta { get; set; }
        public virtual DbSet<Transferencia> Transferencia { get; set; }

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

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");

                entity.Property(e => e.FechaNac).HasColumnType("date");

                entity.Property(e => e.MetaColones).HasColumnType("money");

                entity.Property(e => e.MetaDolares).HasColumnType("money");

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

            modelBuilder.Entity<CancelarTarjeta>(entity =>
            {
                entity.ToTable("CANCELAR_TARJETA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Monto).HasColumnType("money");

                entity.HasOne(d => d.NumTarjetaNavigation)
                    .WithMany(p => p.CancelarTarjeta)
                    .HasForeignKey(d => d.NumTarjeta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("TarjetaConstraint");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK_CLIENTE");

                entity.ToTable("CLIENTE");

                entity.Property(e => e.Cedula)
                    .HasColumnType("bpchar")
                    .HasMaxLength(9);

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");

                entity.Property(e => e.Ingreso).HasColumnType("money");

                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(7)
                    .HasDefaultValueSql("'Colones'::character varying");

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

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.ToTable("COMPRA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comercio)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(7)
                    .HasDefaultValueSql("'Colones'::character varying");

                entity.Property(e => e.Monto).HasColumnType("money");

                entity.HasOne(d => d.NumTarjetaNavigation)
                    .WithMany(p => p.Compra)
                    .HasForeignKey(d => d.NumTarjeta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("TarjetaConstraint");
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

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");

                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(7)
                    .HasDefaultValueSql("'Colones'::character varying");

                entity.Property(e => e.Saldo)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(9);

                entity.HasOne(d => d.CedClienteNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.CedCliente)
                    .HasConstraintName("Cliente");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK_EMPLEADO");

                entity.ToTable("EMPLEADO");

                entity.Property(e => e.Cedula)
                    .HasColumnType("bpchar")
                    .HasMaxLength(9);

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.PriApellido)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.SegApellido)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.SegNombre)
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.Sucursal)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'Cartago'::character varying");
            });

            modelBuilder.Entity<EmpleadoRol>(entity =>
            {
                entity.ToTable("EMPLEADO_ROL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CedulaEmpledo)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(9);

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");

                entity.Property(e => e.NombreRol)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.HasOne(d => d.CedulaEmpledoNavigation)
                    .WithMany(p => p.EmpleadoRol)
                    .HasForeignKey(d => d.CedulaEmpledo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("EmpleadoReference");

                entity.HasOne(d => d.NombreRolNavigation)
                    .WithMany(p => p.EmpleadoRol)
                    .HasForeignKey(d => d.NombreRol)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RolReference");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.ToTable("MOVIMIENTO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(7)
                    .HasDefaultValueSql("'Colones'::character varying");

                entity.Property(e => e.Monto).HasColumnType("money");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(8);

                entity.HasOne(d => d.NumCuentaNavigation)
                    .WithMany(p => p.Movimiento)
                    .HasForeignKey(d => d.NumCuenta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("CuentaReference");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => new { e.NumPrestamo, e.Fecha })
                    .HasName("PK_PAGO");

                entity.ToTable("PAGO");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.CedCliente)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(9);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(9);

                entity.Property(e => e.Extraordinario)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Monto).HasColumnType("money");

                entity.Property(e => e.MontoInteres)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PagosRestantes).HasDefaultValueSql("12");

                entity.HasOne(d => d.CedClienteNavigation)
                    .WithMany(p => p.Pago)
                    .HasForeignKey(d => d.CedCliente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("ClienteReference");

                entity.HasOne(d => d.NumPrestamoNavigation)
                    .WithMany(p => p.Pago)
                    .HasForeignKey(d => d.NumPrestamo)
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

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");

                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(7)
                    .HasDefaultValueSql("'Colones'::character varying");

                entity.Property(e => e.SaldoActual).HasColumnType("money");

                entity.Property(e => e.SaldoOrig).HasColumnType("money");

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

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");
            });

            modelBuilder.Entity<Tarjeta>(entity =>
            {
                entity.HasKey(e => e.Numero)
                    .HasName("PK_TARJETA");

                entity.ToTable("TARJETA");

                entity.Property(e => e.CodigoSeg)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(3);

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");

                entity.Property(e => e.FechaExp).HasColumnType("date");

                entity.Property(e => e.SaldoActual)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SaldoOrig)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

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

            modelBuilder.Entity<Transferencia>(entity =>
            {
                entity.ToTable("TRANSFERENCIA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Estado)
                    .HasColumnType("char")
                    .HasDefaultValueSql("'A'::\"char\"");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(7)
                    .HasDefaultValueSql("'Colones'::character varying");

                entity.Property(e => e.Monto).HasColumnType("money");

                entity.HasOne(d => d.CuentaEmisoraNavigation)
                    .WithMany(p => p.TransferenciaCuentaEmisoraNavigation)
                    .HasForeignKey(d => d.CuentaEmisora)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("EmisorReference");

                entity.HasOne(d => d.CuentaReceptoraNavigation)
                    .WithMany(p => p.TransferenciaCuentaReceptoraNavigation)
                    .HasForeignKey(d => d.CuentaReceptora)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("ReceptorReference");
            });
        }
    }
}