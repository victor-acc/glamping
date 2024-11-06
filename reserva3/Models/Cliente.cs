﻿using System;
using System.Collections.Generic;

namespace reserva3.Models;

public partial class Cliente
{
    public string Documentocliente { get; set; } = null!;

    public string? TipoDocumento { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Direccion { get; set; }

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public bool? Estado { get; set; }

    public int? Idrol { get; set; }

    public virtual Role? IdrolNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
