using System;
using System.Collections.Generic;

namespace reserva3.Models;

public partial class Paquete
{
    public int Idpaquete { get; set; }

    public string? NombrePaquete { get; set; }

    public byte[]? ImagenServicio { get; set; }

    public string? Descripcion { get; set; }

    public int? Idhabitacion { get; set; }

    public int? Idservicio { get; set; }

    public decimal? Precio { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<DetalleReservaPaquete> DetalleReservaPaquetes { get; set; } = new List<DetalleReservaPaquete>();

    public virtual Habitacion? IdhabitacionNavigation { get; set; }

    public virtual Servicio? IdservicioNavigation { get; set; }

    public virtual ICollection<PaquetesHabitacion> PaquetesHabitacions { get; set; } = new List<PaquetesHabitacion>();

    public virtual ICollection<PaquetesServicio> PaquetesServicios { get; set; } = new List<PaquetesServicio>();
}
