using System;
using System.Collections.Generic;

namespace reserva3.Models;

public partial class PaquetesHabitacion
{
    public int IdpaquetesHabitacion { get; set; }

    public int? Idpaquete { get; set; }

    public int? Idhabitacion { get; set; }

    public virtual Habitacion? IdhabitacionNavigation { get; set; }

    public virtual Paquete? IdpaqueteNavigation { get; set; }
}
