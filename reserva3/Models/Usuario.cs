﻿using System;
using System.Collections.Generic;

namespace reserva3.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? ApellidoUsuario { get; set; }

    public string? Contrasena { get; set; }

    public string? Email { get; set; }

    public string? TipoDocumento { get; set; }

    public int? NumeroDocumento { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Pais { get; set; }

    public string? Ciudad { get; set; }

    public int? Idrol { get; set; }

    public bool IsActive { get; set; }

    public virtual Role? IdrolNavigation { get; set; }
}
