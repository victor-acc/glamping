using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace reserva3.Models;

public partial class Glamping3Context : DbContext
{
    public Glamping3Context()
    {
    }

    public Glamping3Context(DbContextOptions<Glamping3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Abono> Abonos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleReservaPaquete> DetalleReservaPaquetes { get; set; }

    public virtual DbSet<DetalleReservaServicio> DetalleReservaServicios { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EstadosReserva> EstadosReservas { get; set; }

    public virtual DbSet<Habitacion> Habitacions { get; set; }

    public virtual DbSet<HabitacionMueble> HabitacionMuebles { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Mueble> Muebles { get; set; }

    public virtual DbSet<Paquete> Paquetes { get; set; }

    public virtual DbSet<PaquetesHabitacion> PaquetesHabitacions { get; set; }

    public virtual DbSet<PaquetesServicio> PaquetesServicios { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPermiso> RolesPermisos { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<TipodeHabitacion> TipodeHabitacions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= VICTORPC\\SQLEXPRESS;Initial Catalog=glamping3;integrated security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abono>(entity =>
        {
            entity.HasKey(e => e.Idabono).HasName("PK__Abono__8647F8A95BEF818C");

            entity.ToTable("Abono");

            entity.Property(e => e.Idabono).HasColumnName("IDAbono");
            entity.Property(e => e.Idreserva).HasColumnName("IDReserva");
            entity.Property(e => e.Iva).HasColumnName("IVA");
            entity.Property(e => e.Porcentaje).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ValorDeuda).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdreservaNavigation).WithMany(p => p.Abonos)
                .HasForeignKey(d => d.Idreserva)
                .HasConstraintName("FK_Abono_Reserva");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Documentocliente).HasName("PK__Clientes__B8AC153F7E6CFB82");

            entity.Property(e => e.Documentocliente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idrol).HasColumnName("IDRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Idrol)
                .HasConstraintName("FK_Clientes_Roles");
        });

        modelBuilder.Entity<DetalleReservaPaquete>(entity =>
        {
            entity.HasKey(e => e.IddetalleReservaPaquetes).HasName("PK__DetalleR__64F9FDAEC8264FCC");

            entity.Property(e => e.IddetalleReservaPaquetes).HasColumnName("IDDetalleReservaPaquetes");
            entity.Property(e => e.Idpaquete).HasColumnName("IDPaquete");
            entity.Property(e => e.Idreserva).HasColumnName("IDReserva");

            entity.HasOne(d => d.IdpaqueteNavigation).WithMany(p => p.DetalleReservaPaquetes)
                .HasForeignKey(d => d.Idpaquete)
                .HasConstraintName("FK_DetalleReservaPaquetes_Paquete");

            entity.HasOne(d => d.IdreservaNavigation).WithMany(p => p.DetalleReservaPaquetes)
                .HasForeignKey(d => d.Idreserva)
                .HasConstraintName("FK_DetalleReservaPaquetes_Reserva");
        });

        modelBuilder.Entity<DetalleReservaServicio>(entity =>
        {
            entity.HasKey(e => e.IddetalleReservaServicio).HasName("PK__DetalleR__B52B22F89715EE41");

            entity.ToTable("DetalleReservaServicio");

            entity.Property(e => e.IddetalleReservaServicio).HasColumnName("IDDetalleReservaServicio");
            entity.Property(e => e.Idreserva).HasColumnName("IDReserva");
            entity.Property(e => e.Idservicio).HasColumnName("IDServicio");

            entity.HasOne(d => d.IdreservaNavigation).WithMany(p => p.DetalleReservaServicios)
                .HasForeignKey(d => d.Idreserva)
                .HasConstraintName("FK_DetalleReservaServicio_Reserva");

            entity.HasOne(d => d.IdservicioNavigation).WithMany(p => p.DetalleReservaServicios)
                .HasForeignKey(d => d.Idservicio)
                .HasConstraintName("FK_DetalleReservaServicio_Servicio");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Idempresa).HasName("PK__Empresa__ED09F0D54CF762BD");

            entity.ToTable("Empresa");

            entity.Property(e => e.Idempresa).HasColumnName("IDEmpresa");
            entity.Property(e => e.PronombrePrimero)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadosReserva>(entity =>
        {
            entity.HasKey(e => e.IdEstadoReserva).HasName("PK__EstadosR__3E654CA8F6510A9E");

            entity.ToTable("EstadosReserva");

            entity.Property(e => e.NombreEstadoReserva)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.Idhabitacion).HasName("PK__Habitaci__6B4757DACC0F0A67");

            entity.ToTable("Habitacion");

            entity.Property(e => e.Idhabitacion).HasColumnName("IDHabitacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdtipodeHabitacion).HasColumnName("IDTipodeHabitacion");
            entity.Property(e => e.Muebles)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NombreHabitacion)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdtipodeHabitacionNavigation).WithMany(p => p.Habitacions)
                .HasForeignKey(d => d.IdtipodeHabitacion)
                .HasConstraintName("FK_Habitacion_TipodeHabitacion");
        });

        modelBuilder.Entity<HabitacionMueble>(entity =>
        {
            entity.HasKey(e => e.IdhabitacionMuebles).HasName("PK__Habitaci__65CFCA9A5806C8B0");

            entity.Property(e => e.IdhabitacionMuebles).HasColumnName("IDHabitacionMuebles");
            entity.Property(e => e.Idhabitacion).HasColumnName("IDHabitacion");
            entity.Property(e => e.Idmueble).HasColumnName("IDMueble");

            entity.HasOne(d => d.IdhabitacionNavigation).WithMany(p => p.HabitacionMuebles)
                .HasForeignKey(d => d.Idhabitacion)
                .HasConstraintName("FK_HabitacionMuebles_Habitacion");

            entity.HasOne(d => d.IdmuebleNavigation).WithMany(p => p.HabitacionMuebles)
                .HasForeignKey(d => d.Idmueble)
                .HasConstraintName("FK_HabitacionMuebles_Inmuebles");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__MetodoPa__6F49A9BE0A06EB9C");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.NomMetodoPago)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Mueble>(entity =>
        {
            entity.HasKey(e => e.Idmueble).HasName("PK__Muebles__EAC9F7C88D5AB9AA");

            entity.Property(e => e.Idmueble).HasColumnName("IDMueble");
            entity.Property(e => e.NombreMueble)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.HasKey(e => e.Idpaquete).HasName("PK__Paquetes__4C29513BD822A62A");

            entity.Property(e => e.Idpaquete).HasColumnName("IDPaquete");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idhabitacion).HasColumnName("IDHabitacion");
            entity.Property(e => e.Idservicio).HasColumnName("IDServicio");
            entity.Property(e => e.NombrePaquete)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdhabitacionNavigation).WithMany(p => p.Paquetes)
                .HasForeignKey(d => d.Idhabitacion)
                .HasConstraintName("FK_Habitacione_Paquetes");

            entity.HasOne(d => d.IdservicioNavigation).WithMany(p => p.Paquetes)
                .HasForeignKey(d => d.Idservicio)
                .HasConstraintName("FK_Servicio_Paquetes");
        });

        modelBuilder.Entity<PaquetesHabitacion>(entity =>
        {
            entity.HasKey(e => e.IdpaquetesHabitacion).HasName("PK__Paquetes__2EBD778119DDD9F1");

            entity.ToTable("PaquetesHabitacion");

            entity.Property(e => e.IdpaquetesHabitacion).HasColumnName("IDPaquetesHabitacion");
            entity.Property(e => e.Idhabitacion).HasColumnName("IDHabitacion");
            entity.Property(e => e.Idpaquete).HasColumnName("IDPaquete");

            entity.HasOne(d => d.IdhabitacionNavigation).WithMany(p => p.PaquetesHabitacions)
                .HasForeignKey(d => d.Idhabitacion)
                .HasConstraintName("FK_PaquetesHabitacion_Habitacion");

            entity.HasOne(d => d.IdpaqueteNavigation).WithMany(p => p.PaquetesHabitacions)
                .HasForeignKey(d => d.Idpaquete)
                .HasConstraintName("FK_PaquetesHabitacion_Paquetes");
        });

        modelBuilder.Entity<PaquetesServicio>(entity =>
        {
            entity.HasKey(e => e.IdpaquetesServicio).HasName("PK__Paquetes__C5795B25E9C8776E");

            entity.ToTable("PaquetesServicio");

            entity.Property(e => e.IdpaquetesServicio).HasColumnName("IDPaquetesServicio");
            entity.Property(e => e.Idpaquete).HasColumnName("IDPaquete");
            entity.Property(e => e.Idservicio).HasColumnName("IDServicio");

            entity.HasOne(d => d.IdpaqueteNavigation).WithMany(p => p.PaquetesServicios)
                .HasForeignKey(d => d.Idpaquete)
                .HasConstraintName("FK_PaquetesServicio_Paquetes");

            entity.HasOne(d => d.IdservicioNavigation).WithMany(p => p.PaquetesServicios)
                .HasForeignKey(d => d.Idservicio)
                .HasConstraintName("FK_PaquetesServicio_Servicio");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Idpermiso).HasName("PK__Permisos__F11D75F32A7700D4");

            entity.Property(e => e.Idpermiso).HasColumnName("IDPermiso");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoPermisos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombrePermisos)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Idreserva).HasName("PK__Reserva__D9F2FA675C7CE5DB");

            entity.ToTable("Reserva");

            entity.Property(e => e.Idreserva).HasColumnName("IDReserva");
            entity.Property(e => e.Descuento).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DocumentoCliente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdestadoReserva).HasColumnName("IDEstadoReserva");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("IVA");
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.DocumentoClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.DocumentoCliente)
                .HasConstraintName("FK_Reserva_Clientes");

            entity.HasOne(d => d.IdestadoReservaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdestadoReserva)
                .HasConstraintName("FK_estado_reserva");

            entity.HasOne(d => d.MetodoPagoNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.MetodoPago)
                .HasConstraintName("FK_metodo_reserva");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Idrol).HasName("PK__Roles__A681ACB609ADB76B");

            entity.Property(e => e.Idrol).HasColumnName("IDRol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolesPermiso>(entity =>
        {
            entity.HasKey(e => e.IdrolPermiso).HasName("PK__RolesPer__3F09FABF7ED94F21");

            entity.Property(e => e.IdrolPermiso).HasColumnName("IDRolPermiso");
            entity.Property(e => e.Idpermiso).HasColumnName("IDPermiso");
            entity.Property(e => e.Idrol).HasColumnName("IDRol");

            entity.HasOne(d => d.IdpermisoNavigation).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.Idpermiso)
                .HasConstraintName("FK_RolesPermisos_Permisos");

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.Idrol)
                .HasConstraintName("FK_RolesPermisos_Roles");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Idservicio).HasName("PK__Servicio__3CCE7416DF06601C");

            entity.Property(e => e.Idservicio).HasColumnName("IDServicio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Duracion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipodeHabitacion>(entity =>
        {
            entity.HasKey(e => e.IdtipodeHabitacion).HasName("PK__TipodeHa__9BD4086C5B6B865D");

            entity.ToTable("TipodeHabitacion");

            entity.Property(e => e.IdtipodeHabitacion).HasColumnName("IDTipodeHabitacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreTipodeHabitacion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__Usuarios__5231116965FC8D93");

            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.ApellidoUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ciudad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idrol).HasColumnName("IDRol");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idrol)
                .HasConstraintName("FK_Usuarios_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
