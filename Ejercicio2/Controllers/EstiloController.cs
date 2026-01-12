using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ejercicio2.Data;
using Ejercicio2.Models;

namespace Ejercicio2.Controllers
{
    public class EstiloController : Controller
    {
        private readonly Ejercicio2DbContext _context;

        public EstiloController(Ejercicio2DbContext context)
        {
            _context = context;
        }

        // GET: Estilo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estilos.ToListAsync());
        }

        // GET: Estilo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estilo = await _context.Estilos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estilo == null)
            {
                return NotFound();
            }

            return View(estilo);
        }

        // GET: Estilo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estilo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] Estilo estilo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estilo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estilo);
        }

        // GET: Estilo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estilo = await _context.Estilos.FindAsync(id);
            if (estilo == null)
            {
                return NotFound();
            }
            return View(estilo);
        }

        // POST: Estilo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] Estilo estilo)
        {
            if (id != estilo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estilo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstiloExists(estilo.Id))
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
            return View(estilo);
        }

        // GET: Estilo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estilo = await _context.Estilos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estilo == null)
            {
                return NotFound();
            }

            return View(estilo);
        }

        // POST: Estilo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estilo = await _context.Estilos.FindAsync(id);
            if (estilo != null)
            {
                _context.Estilos.Remove(estilo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstiloExists(int id)
        {
            return _context.Estilos.Any(e => e.Id == id);
        }
    }
}
