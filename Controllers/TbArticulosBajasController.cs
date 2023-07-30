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
    public class TbArticulosBajasController : Controller
    {
        private readonly Bdventas2023WebapiContext _context;

        public TbArticulosBajasController(Bdventas2023WebapiContext context)
        {
            _context = context;
        }

        // GET: TbArticulosBajas
        public async Task<IActionResult> Index()
        {
            var bdventas2023WebapiContext = _context.TbArticulosBajas.Include(t => t.CodArtNavigation);
            return View(await bdventas2023WebapiContext.ToListAsync());
        }

        // GET: TbArticulosBajas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TbArticulosBajas == null)
            {
                return NotFound();
            }

            var tbArticulosBaja = await _context.TbArticulosBajas
                .Include(t => t.CodArtNavigation)
                .FirstOrDefaultAsync(m => m.CodArt == id);
            if (tbArticulosBaja == null)
            {
                return NotFound();
            }

            return View(tbArticulosBaja);
        }

        // GET: TbArticulosBajas/Create
        public IActionResult Create()
        {
            ViewData["CodArt"] = new SelectList(_context.TbArticulos, "CodArt", "CodArt");
            return View();
        }

        // POST: TbArticulosBajas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodArt,FechaBaja")] TbArticulosBaja tbArticulosBaja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbArticulosBaja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodArt"] = new SelectList(_context.TbArticulos, "CodArt", "CodArt", tbArticulosBaja.CodArt);
            return View(tbArticulosBaja);
        }

        // GET: TbArticulosBajas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TbArticulosBajas == null)
            {
                return NotFound();
            }

            var tbArticulosBaja = await _context.TbArticulosBajas.FindAsync(id);
            if (tbArticulosBaja == null)
            {
                return NotFound();
            }
            ViewData["CodArt"] = new SelectList(_context.TbArticulos, "CodArt", "CodArt", tbArticulosBaja.CodArt);
            return View(tbArticulosBaja);
        }

        // POST: TbArticulosBajas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodArt,FechaBaja")] TbArticulosBaja tbArticulosBaja)
        {
            if (id != tbArticulosBaja.CodArt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbArticulosBaja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbArticulosBajaExists(tbArticulosBaja.CodArt))
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
            ViewData["CodArt"] = new SelectList(_context.TbArticulos, "CodArt", "CodArt", tbArticulosBaja.CodArt);
            return View(tbArticulosBaja);
        }

        // GET: TbArticulosBajas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TbArticulosBajas == null)
            {
                return NotFound();
            }

            var tbArticulosBaja = await _context.TbArticulosBajas
                .Include(t => t.CodArtNavigation)
                .FirstOrDefaultAsync(m => m.CodArt == id);
            if (tbArticulosBaja == null)
            {
                return NotFound();
            }

            return View(tbArticulosBaja);
        }

        // POST: TbArticulosBajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TbArticulosBajas == null)
            {
                return Problem("Entity set 'Bdventas2023WebapiContext.TbArticulosBajas'  is null.");
            }
            var tbArticulosBaja = await _context.TbArticulosBajas.FindAsync(id);
            if (tbArticulosBaja != null)
            {
                _context.TbArticulosBajas.Remove(tbArticulosBaja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbArticulosBajaExists(string id)
        {
          return (_context.TbArticulosBajas?.Any(e => e.CodArt == id)).GetValueOrDefault();
        }
    }
}
