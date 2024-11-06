using System;
using System.Collections.Generic;

namespace reserva3.Models;

public partial class Abono
{
    public int Idabono { get; set; }

    public int? Idreserva { get; set; }

    public DateOnly? FechaAbono { get; set; }

    public decimal? ValorDeuda { get; set; }

    public decimal? Porcentaje { get; set; }

    public int? Pendiente { get; set; }

    public decimal? Subtotal { get; set; }

    public int? Iva { get; set; }

    public int? CantAbono { get; set; }

    public byte[]? Comprobante { get; set; }

    public bool? Estado { get; set; }

    public virtual Reserva? IdreservaNavigation { get; set; }
}
