using System;
using System.Collections.Generic;

namespace reserva3.Models;

public partial class Role
{
    public int Idrol { get; set; }

    public string? NombreRol { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<RolesPermiso> RolesPermisos { get; set; } = new List<RolesPermiso>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
