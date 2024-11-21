using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using reserva3.Models;

namespace reserva3.Controllers
{
    public class ReservasController : Controller
    {
        private readonly Glamping3Context _context;

        public ReservasController(Glamping3Context context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var glamping3Context = _context.Reservas.Include(r => r.DocumentoClienteNavigation).Include(r => r.IdestadoReservaNavigation).Include(r => r.MetodoPagoNavigation);
            return View(await glamping3Context.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.DocumentoClienteNavigation)
                .Include(r => r.IdestadoReservaNavigation)
                .Include(r => r.MetodoPagoNavigation)
                .FirstOrDefaultAsync(m => m.Idreserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewBag.Paquetes = _context.Paquetes.ToList();
            ViewBag.Servicios = _context.Servicios.ToList();
            ViewData["DocumentoCliente"] = new SelectList(_context.Clientes, "Documentocliente", "Documentocliente");
            ViewData["IdestadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "IdEstadoReserva");
            ViewData["MetodoPago"] = new SelectList(_context.MetodoPagos, "IdMetodoPago", "IdMetodoPago");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idreserva,DocumentoCliente,FechaReserva,FechaInicio,FechaFinalizacion,SubTotal,Descuento,Iva,MontoTotal,MetodoPago,NroPersonas,IdestadoReserva")] Reserva reserva, List<int> paqueteIds, List<int> servicioids)
        {
            if (ModelState.IsValid)
            {
                // Inicializa los valores predeterminados si es necesario
                const decimal montoBase = 300000m;
                decimal subtotal = montoBase;

                if (paqueteIds != null && paqueteIds.Any())
                {
                    var paquetesSeleccionados = _context.Paquetes.Where(p => paqueteIds.Contains(p.Idpaquete)).ToList();
                    subtotal += paquetesSeleccionados.Sum(p => (decimal)p.Precio); // Conversión explícita a decimal
                }

                // Calcula el subtotal a partir de los servicios seleccionados
                if (servicioids != null && servicioids.Any())
                {
                    var serviciosSeleccionados = _context.Servicios.Where(s => servicioids.Contains(s.Idservicio)).ToList();
                    subtotal += serviciosSeleccionados.Sum(s => (decimal)s.Costo); // Conversión explícita a decimal
                }

                // Aplica el descuento del 10%
                decimal descuento = subtotal * 0.10m;

                // Calcula el IVA (12% del subtotal)
                decimal iva = (subtotal - descuento) * 0.12m;

                // Calcula el monto total
                reserva.SubTotal = subtotal;
                reserva.Descuento = descuento;
                reserva.Iva = iva;
                reserva.MontoTotal = subtotal - descuento + iva;

                // Guarda la reserva
                _context.Add(reserva);
                await _context.SaveChangesAsync(); // Esto asegura que el Idreserva esté disponible

                // Agregar los paquetes seleccionados a la tabla intermedia DetalleReservaPaquete
                if (paqueteIds != null && paqueteIds.Any())
                {
                    foreach (var paqueteId in paqueteIds)
                    {
                        var detallePaquete = new DetalleReservaPaquete
                        {
                            Idreserva = reserva.Idreserva, // Usa el Idreserva generado
                            Idpaquete = paqueteId
                        };
                        _context.DetalleReservaPaquetes.Add(detallePaquete);
                    }
                }

                // Agregar los servicios seleccionados a la tabla intermedia DetalleReservaServicio
                if (servicioids != null && servicioids.Any())
                {
                    foreach (var servicioId in servicioids)
                    {
                        var detalleServicio = new DetalleReservaServicio
                        {
                            Idreserva = reserva.Idreserva, // Usa el Idreserva generado
                            Idservicio = servicioId
                        };
                        _context.DetalleReservaServicios.Add(detalleServicio);
                    }
                }

                // Guarda los cambios nuevamente para incluir los detalles
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // En caso de error, vuelve a cargar los datos necesarios para la vista
            ViewData["DocumentoCliente"] = new SelectList(_context.Clientes, "Documentocliente", "Documentocliente", reserva.DocumentoCliente);
            ViewData["IdestadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "IdEstadoReserva", reserva.IdestadoReserva);
            ViewData["MetodoPago"] = new SelectList(_context.MetodoPagos, "IdMetodoPago", "IdMetodoPago", reserva.MetodoPago);
            return View(reserva);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerClientePorDocumento(string documento)
        {
            var cliente = await _context.Clientes
                                        .Where(c => c.Documentocliente == documento)
                                        .Select(c => new
                                        {
                                            c.Nombre,
                                            c.Apellido,
                                            c.Email, // Verifica que el campo aquí corresponda con el nombre en tu modelo
                                            c.Telefono
                                        })
                                        .FirstOrDefaultAsync();

            if (cliente != null)
            {
                return Json(new { success = true, data = cliente });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.DetalleReservaPaquetes) // Incluir los detalles de paquetes
                .Include(r => r.DetalleReservaServicios) // Incluir los detalles de servicios
                .FirstOrDefaultAsync(m => m.Idreserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            // Cargar los paquetes y servicios seleccionados para la vista
            var paqueteIds = reserva.DetalleReservaPaquetes.Select(dp => dp.Idpaquete).ToList();
            var servicioIds = reserva.DetalleReservaServicios.Select(ds => ds.Idservicio).ToList();

            ViewBag.Paquetes = _context.Paquetes.ToList();
            ViewBag.Servicios = _context.Servicios.ToList();
            ViewBag.PaquetesSeleccionados = paqueteIds;
            ViewBag.ServiciosSeleccionados = servicioIds;

            ViewData["DocumentoCliente"] = new SelectList(_context.Clientes, "Documentocliente", "Documentocliente", reserva.DocumentoCliente);
            ViewData["IdestadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "IdEstadoReserva", reserva.IdestadoReserva);
            ViewData["MetodoPago"] = new SelectList(_context.MetodoPagos, "IdMetodoPago", "IdMetodoPago", reserva.MetodoPago);

            return View(reserva);
        }



        // POST: Reservas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idreserva,DocumentoCliente,FechaReserva,FechaInicio,FechaFinalizacion,SubTotal,Descuento,Iva,MontoTotal,MetodoPago,NroPersonas,IdestadoReserva")] Reserva reserva, List<int> paqueteIds, List<int> servicioids)
        {
            if (id != reserva.Idreserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Eliminar los paquetes y servicios antiguos
                    var detallesPaqueteAntiguos = _context.DetalleReservaPaquetes.Where(dp => dp.Idreserva == id).ToList();
                    _context.DetalleReservaPaquetes.RemoveRange(detallesPaqueteAntiguos);

                    var detallesServicioAntiguos = _context.DetalleReservaServicios.Where(ds => ds.Idreserva == id).ToList();
                    _context.DetalleReservaServicios.RemoveRange(detallesServicioAntiguos);

                    // Agregar los nuevos paquetes seleccionados
                    if (paqueteIds != null && paqueteIds.Any())
                    {
                        foreach (var paqueteId in paqueteIds)
                        {
                            var detallePaquete = new DetalleReservaPaquete
                            {
                                Idreserva = reserva.Idreserva, // Usa el Idreserva actualizado
                                Idpaquete = paqueteId
                            };
                            _context.DetalleReservaPaquetes.Add(detallePaquete);
                        }
                    }

                    // Agregar los nuevos servicios seleccionados
                    if (servicioids != null && servicioids.Any())
                    {
                        foreach (var servicioId in servicioids)
                        {
                            var detalleServicio = new DetalleReservaServicio
                            {
                                Idreserva = reserva.Idreserva, // Usa el Idreserva actualizado
                                Idservicio = servicioId
                            };
                            _context.DetalleReservaServicios.Add(detalleServicio);
                        }
                    }

                    // Actualizar la reserva
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Idreserva))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            // En caso de error, vuelve a cargar los datos necesarios para la vista
            ViewData["DocumentoCliente"] = new SelectList(_context.Clientes, "Documentocliente", "Documentocliente", reserva.DocumentoCliente);
            ViewData["IdestadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "IdEstadoReserva", reserva.IdestadoReserva);
            ViewData["MetodoPago"] = new SelectList(_context.MetodoPagos, "IdMetodoPago", "IdMetodoPago", reserva.MetodoPago);
            return View(reserva);
        }
        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Idreserva == id);
        }
    }
}
