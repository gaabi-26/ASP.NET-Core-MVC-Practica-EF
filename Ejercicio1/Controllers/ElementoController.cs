using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ejercicio1.Data;
using Ejercicio1.Models;
using Microsoft.Data.SqlClient;

namespace Ejercicio1.Controllers
{
    public class ElementoController : Controller
    {
        private readonly Ejercicio1DbContext _context;

        public ElementoController(Ejercicio1DbContext context)
        {
            _context = context;
        }

        // GET: Elementoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Elementos.ToListAsync());
        }

        // GET: Elementoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elemento = await _context.Elementos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (elemento == null)
            {
                return NotFound();
            }

            return View(elemento);
        }

        // GET: Elementoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Elementoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] Elemento elemento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(elemento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(elemento);
        }

        // GET: Elementoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elemento = await _context.Elementos.FindAsync(id);
            if (elemento == null)
            {
                return NotFound();
            }
            return View(elemento);
        }

        // POST: Elementoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] Elemento elemento)
        {
            if (id != elemento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(elemento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElementoExists(elemento.Id))
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
            return View(elemento);
        }

        // GET: Elementoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elemento = await _context.Elementos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (elemento == null)
            {
                return NotFound();
            }

            return View(elemento);
        }

        // POST: Elementoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var elemento = await _context.Elementos.FindAsync(id);
            if (elemento != null)
            {
                _context.Elementos.Remove(elemento);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // 547 = FOREIGN KEY constraint violation in SQL Server
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    // Redirige a una vista que explica que no se puede eliminar por integridad referencial
                    return RedirectToAction(nameof(DeleteError));
                }
                // Si no es el caso esperado, relanzar para no ocultar otros errores
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // Vista que informa por qué no se puede eliminar el elemento
        public IActionResult DeleteError()
        {
            return View();
        }

        private bool ElementoExists(int id)
        {
            return _context.Elementos.Any(e => e.Id == id);
        }
    }
}
