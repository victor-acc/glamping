using System;
using System.Collections.Generic;

namespace reserva3.Models;

public partial class HabitacionMueble
{
    public int IdhabitacionMuebles { get; set; }

    public int? Idhabitacion { get; set; }

    public int? Idmueble { get; set; }

    public virtual Habitacion? IdhabitacionNavigation { get; set; }

    public virtual Mueble? IdmuebleNavigation { get; set; }
}
