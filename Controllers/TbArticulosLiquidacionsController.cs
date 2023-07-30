using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCRUD.Models;

namespace MVCCRUD.Controllers
{
    public class TbArticulosLiquidacionsController : Controller
    {
        private readonly Bdventas2023WebapiContext _context;

        public TbArticulosLiquidacionsController(Bdventas2023WebapiContext context)
        {
            _context = context;
        }

        // GET: TbArticulosLiquidacions
        public async Task<IActionResult> Index()
        {
            var bdventas2023WebapiContext = _context.TbArticulosLiquidacions.Include(t => t.CodArtNavigation);
            return View(await bdventas2023WebapiContext.ToListAsync());
        }

        // GET: TbArticulosLiquidacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbArticulosLiquidacions == null)
            {
                return NotFound();
            }

            var tbArticulosLiquidacion = await _context.TbArticulosLiquidacions
                .Include(t => t.CodArtNavigation)
                .FirstOrDefaultAsync(m => m.NumReg == id);
            if (tbArticulosLiquidacion == null)
            {
                return NotFound();
            }

            return View(tbArticulosLiquidacion);
        }

        // GET: TbArticulosLiquidacions/Create
        public IActionResult Create()
        {
            ViewData["CodArt"] = new SelectList(_context.TbArticulos, "CodArt", "CodArt");
            return View();
        }

        // POST: TbArticulosLiquidacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumReg,CodArt,UnidadesLiquidar,PrecioLiquidar")] TbArticulosLiquidacion tbArticulosLiquidacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbArticulosLiquidacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodArt"] = new SelectList(_context.TbArticulos, "CodArt", "CodArt", tbArticulosLiquidacion.CodArt);
            return View(tbArticulosLiquidacion);
        }

        // GET: TbArticulosLiquidacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbArticulosLiquidacions == null)
            {
                return NotFound();
            }

            var tbArticulosLiquidacion = await _context.TbArticulosLiquidacions.FindAsync(id);
            if (tbArticulosLiquidacion == null)
            {
                return NotFound();
            }
            ViewData["CodArt"] = new SelectList(_context.TbArticulos, "CodArt", "CodArt", tbArticulosLiquidacion.CodArt);
            return View(tbArticulosLiquidacion);
        }

        // POST: TbArticulosLiquidacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumReg,CodArt,UnidadesLiquidar,PrecioLiquidar")] TbArticulosLiquidacion tbArticulosLiquidacion)
        {
            if (id != tbArticulosLiquidacion.NumReg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbArticulosLiquidacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbArticulosLiquidacionExists(tbArticulosLiquidacion.NumReg))
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
            ViewData["CodArt"] = new SelectList(_context.TbArticulos, "CodArt", "CodArt", tbArticulosLiquidacion.CodArt);
            return View(tbArticulosLiquidacion);
        }

        // GET: TbArticulosLiquidacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbArticulosLiquidacions == null)
            {
                return NotFound();
            }

            var tbArticulosLiquidacion = await _context.TbArticulosLiquidacions
                .Include(t => t.CodArtNavigation)
                .FirstOrDefaultAsync(m => m.NumReg == id);
            if (tbArticulosLiquidacion == null)
            {
                return NotFound();
            }

            return View(tbArticulosLiquidacion);
        }

        // POST: TbArticulosLiquidacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbArticulosLiquidacions == null)
            {
                return Problem("Entity set 'Bdventas2023WebapiContext.TbArticulosLiquidacions'  is null.");
            }
            var tbArticulosLiquidacion = await _context.TbArticulosLiquidacions.FindAsync(id);
            if (tbArticulosLiquidacion != null)
            {
                _context.TbArticulosLiquidacions.Remove(tbArticulosLiquidacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbArticulosLiquidacionExists(int id)
        {
          return (_context.TbArticulosLiquidacions?.Any(e => e.NumReg == id)).GetValueOrDefault();
        }
    }
}
