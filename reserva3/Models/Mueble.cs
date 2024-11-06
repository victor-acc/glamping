using System;
using System.Collections.Generic;

namespace reserva3.Models;

public partial class Mueble
{
    public int Idmueble { get; set; }

    public string? NombreMueble { get; set; }

    public byte[]? ImagenMueble { get; set; }

    public int? CantidadMueble { get; set; }

    public int? Valor { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<HabitacionMueble> HabitacionMuebles { get; set; } = new List<HabitacionMueble>();
}
