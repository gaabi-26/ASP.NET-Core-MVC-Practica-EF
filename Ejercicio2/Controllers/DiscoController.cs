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
    public class DiscoController : Controller
    {
        private readonly Ejercicio2DbContext _context;

        public DiscoController(Ejercicio2DbContext context)
        {
            _context = context;
        }

        // GET: Disco
        public async Task<IActionResult> Index()
        {
            var ejercicio2DbContext = _context.Discos.Include(d => d.Estilo).Include(d => d.TipoEdicion);
            return View(await ejercicio2DbContext.ToListAsync());
        }

        // GET: Disco/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Discos
                .Include(d => d.Estilo)
                .Include(d => d.TipoEdicion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disco == null)
            {
                return NotFound();
            }

            return View(disco);
        }

        // GET: Disco/Create
        public IActionResult Create()
        {
            ViewData["EstiloId"] = new SelectList(_context.Estilos, "Id", "Descripcion");
            ViewData["TipoEdicionId"] = new SelectList(_context.TiposEdicion, "Id", "Descripcion");
            return View();
        }

        // POST: Disco/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,FechaLanzamiento,CantidadCanciones,TipoEdicionId,EstiloId")] Disco disco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstiloId"] = new SelectList(_context.Estilos, "Id", "Id", disco.EstiloId);
            ViewData["TipoEdicionId"] = new SelectList(_context.TiposEdicion, "Id", "Id", disco.TipoEdicionId);
            return View(disco);
        }

        // GET: Disco/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Discos.FindAsync(id);
            if (disco == null)
            {
                return NotFound();
            }
            ViewData["EstiloId"] = new SelectList(_context.Estilos, "Id", "Descripcion", disco.EstiloId);
            ViewData["TipoEdicionId"] = new SelectList(_context.TiposEdicion, "Id", "Descripcion", disco.TipoEdicionId);
            return View(disco);
        }

        // POST: Disco/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,FechaLanzamiento,CantidadCanciones,TipoEdicionId,EstiloId")] Disco disco)
        {
            if (id != disco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscoExists(disco.Id))
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
            ViewData["EstiloId"] = new SelectList(_context.Estilos, "Id", "Descripcion", disco.EstiloId);
            ViewData["TipoEdicionId"] = new SelectList(_context.TiposEdicion, "Id", "Descripcion", disco.TipoEdicionId);
            return View(disco);
        }

        // GET: Disco/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Discos
                .Include(d => d.Estilo)
                .Include(d => d.TipoEdicion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disco == null)
            {
                return NotFound();
            }

            return View(disco);
        }

        // POST: Disco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disco = await _context.Discos.FindAsync(id);
            if (disco != null)
            {
                _context.Discos.Remove(disco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscoExists(int id)
        {
            return _context.Discos.Any(e => e.Id == id);
        }
    }
}
