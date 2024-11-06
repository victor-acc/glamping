using System;
using System.Collections.Generic;

namespace reserva3.Models;

public partial class Servicio
{
    public int Idservicio { get; set; }

    public string? NombreServicio { get; set; }

    public string? Descripcion { get; set; }

    public string? Duracion { get; set; }

    public int? CantidadMaximaPersonas { get; set; }

    public int? Costo { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<DetalleReservaServicio> DetalleReservaServicios { get; set; } = new List<DetalleReservaServicio>();

    public virtual ICollection<Paquete> Paquetes { get; set; } = new List<Paquete>();

    public virtual ICollection<PaquetesServicio> PaquetesServicios { get; set; } = new List<PaquetesServicio>();
}
