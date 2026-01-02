using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ejercicio1.Data;
using Ejercicio1.Models;

namespace Ejercicio1.Controllers
{
    public class PokemonController : Controller
    {
        private readonly Ejercicio1DbContext _context;

        public PokemonController(Ejercicio1DbContext context)
        {
            _context = context;
        }

        // GET: Pokemon
        public async Task<IActionResult> Index()
        {
            var ejercicio1DbContext = _context.Pokemons.Include(p => p.Debilidad).Include(p => p.Tipo);
            return View(await ejercicio1DbContext.ToListAsync());
        }

        // GET: Pokemon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .Include(p => p.Debilidad)
                .Include(p => p.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // GET: Pokemon/Create
        public IActionResult Create()
        {
            ViewData["DebilidadId"] = new SelectList(_context.Elementos, "Id", "Descripcion");
            ViewData["TipoId"] = new SelectList(_context.Elementos, "Id", "Descripcion");
            return View();
        }

        // POST: Pokemon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Nombre,Descripcion,UrlImagen,TipoId,DebilidadId")] Pokemon pokemon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DebilidadId"] = new SelectList(_context.Elementos, "Id", "Descripcion", pokemon.DebilidadId);
            ViewData["TipoId"] = new SelectList(_context.Elementos, "Id", "Descripcion", pokemon.TipoId);
            return View(pokemon);
        }

        // GET: Pokemon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            ViewData["DebilidadId"] = new SelectList(_context.Elementos, "Id", "Descripcion", pokemon.DebilidadId);
            ViewData["TipoId"] = new SelectList(_context.Elementos, "Id", "Descripcion", pokemon.TipoId);
            return View(pokemon);
        }

        // POST: Pokemon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Nombre,Descripcion,UrlImagen,TipoId,DebilidadId")] Pokemon pokemon)
        {
            if (id != pokemon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.Id))
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
            ViewData["DebilidadId"] = new SelectList(_context.Elementos, "Id", "Descripcion", pokemon.DebilidadId);
            ViewData["TipoId"] = new SelectList(_context.Elementos, "Id", "Descripcion", pokemon.TipoId);
            return View(pokemon);
        }

        // GET: Pokemon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .Include(p => p.Debilidad)
                .Include(p => p.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(int id)
        {
            return _context.Pokemons.Any(e => e.Id == id);
        }
    }
}
