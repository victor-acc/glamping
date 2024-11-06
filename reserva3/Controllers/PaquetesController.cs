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
    public class PaquetesController : Controller
    {
        private readonly Glamping3Context _context;

        public PaquetesController(Glamping3Context context)
        {
            _context = context;
        }

        // GET: Paquetes
        public async Task<IActionResult> Index()
        {
            var glamping3Context = _context.Paquetes.Include(p => p.IdhabitacionNavigation).Include(p => p.IdservicioNavigation);
            return View(await glamping3Context.ToListAsync());
        }

        // GET: Paquetes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes
                .Include(p => p.IdhabitacionNavigation)
                .Include(p => p.IdservicioNavigation)
                .FirstOrDefaultAsync(m => m.Idpaquete == id);
            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // GET: Paquetes/Create
        public IActionResult Create()
        {
            ViewData["Idhabitacion"] = new SelectList(_context.Habitacions, "Idhabitacion", "Idhabitacion");
            ViewData["Idservicio"] = new SelectList(_context.Servicios, "Idservicio", "Idservicio");
            return View();
        }

        // POST: Paquetes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idpaquete,NombrePaquete,ImagenServicio,Descripcion,Idhabitacion,Idservicio,Precio,Estado")] Paquete paquete)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paquete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idhabitacion"] = new SelectList(_context.Habitacions, "Idhabitacion", "Idhabitacion", paquete.Idhabitacion);
            ViewData["Idservicio"] = new SelectList(_context.Servicios, "Idservicio", "Idservicio", paquete.Idservicio);
            return View(paquete);
        }

        // GET: Paquetes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes.FindAsync(id);
            if (paquete == null)
            {
                return NotFound();
            }
            ViewData["Idhabitacion"] = new SelectList(_context.Habitacions, "Idhabitacion", "Idhabitacion", paquete.Idhabitacion);
            ViewData["Idservicio"] = new SelectList(_context.Servicios, "Idservicio", "Idservicio", paquete.Idservicio);
            return View(paquete);
        }

        // POST: Paquetes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idpaquete,NombrePaquete,ImagenServicio,Descripcion,Idhabitacion,Idservicio,Precio,Estado")] Paquete paquete)
        {
            if (id != paquete.Idpaquete)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paquete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaqueteExists(paquete.Idpaquete))
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
            ViewData["Idhabitacion"] = new SelectList(_context.Habitacions, "Idhabitacion", "Idhabitacion", paquete.Idhabitacion);
            ViewData["Idservicio"] = new SelectList(_context.Servicios, "Idservicio", "Idservicio", paquete.Idservicio);
            return View(paquete);
        }

        // GET: Paquetes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes
                .Include(p => p.IdhabitacionNavigation)
                .Include(p => p.IdservicioNavigation)
                .FirstOrDefaultAsync(m => m.Idpaquete == id);
            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // POST: Paquetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paquete = await _context.Paquetes.FindAsync(id);
            if (paquete != null)
            {
                _context.Paquetes.Remove(paquete);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaqueteExists(int id)
        {
            return _context.Paquetes.Any(e => e.Idpaquete == id);
        }
    }
}
