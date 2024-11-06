using System;
using System.Collections.Generic;

namespace reserva3.Models;

public partial class Reserva
{
    public int Idreserva { get; set; }

    public string? DocumentoCliente { get; set; }

    public DateOnly? FechaReserva { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFinalizacion { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? Descuento { get; set; }

    public decimal? Iva { get; set; }

    public decimal? MontoTotal { get; set; }

    public int? MetodoPago { get; set; }

    public int? NroPersonas { get; set; }

    public int? IdestadoReserva { get; set; }

    public virtual ICollection<Abono> Abonos { get; set; } = new List<Abono>();

    public virtual ICollection<DetalleReservaPaquete> DetalleReservaPaquetes { get; set; } = new List<DetalleReservaPaquete>();

    public virtual ICollection<DetalleReservaServicio> DetalleReservaServicios { get; set; } = new List<DetalleReservaServicio>();

    public virtual Cliente? DocumentoClienteNavigation { get; set; }

    public virtual EstadosReserva? IdestadoReservaNavigation { get; set; }

    public virtual MetodoPago? MetodoPagoNavigation { get; set; }
}
