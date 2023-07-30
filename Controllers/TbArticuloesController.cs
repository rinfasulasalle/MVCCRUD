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
    public class TbArticuloesController : Controller
    {
        private readonly Bdventas2023WebapiContext _context;

        public TbArticuloesController(Bdventas2023WebapiContext context)
        {
            _context = context;
        }

        // GET: TbArticuloes
        public async Task<IActionResult> Index()
        {
              return _context.TbArticulos != null ? 
                          View(await _context.TbArticulos.ToListAsync()) :
                          Problem("Entity set 'Bdventas2023WebapiContext.TbArticulos'  is null.");
        }

        // GET: TbArticuloes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TbArticulos == null)
            {
                return NotFound();
            }

            var tbArticulo = await _context.TbArticulos
                .FirstOrDefaultAsync(m => m.CodArt == id);
            if (tbArticulo == null)
            {
                return NotFound();
            }

            return View(tbArticulo);
        }

        // GET: TbArticuloes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbArticuloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodArt,NomArt,UniMed,PreArt,StkArt,DeBaja")] TbArticulo tbArticulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbArticulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbArticulo);
        }

        // GET: TbArticuloes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TbArticulos == null)
            {
                return NotFound();
            }

            var tbArticulo = await _context.TbArticulos.FindAsync(id);
            if (tbArticulo == null)
            {
                return NotFound();
            }
            return View(tbArticulo);
        }

        // POST: TbArticuloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodArt,NomArt,UniMed,PreArt,StkArt,DeBaja")] TbArticulo tbArticulo)
        {
            if (id != tbArticulo.CodArt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbArticulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbArticuloExists(tbArticulo.CodArt))
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
            return View(tbArticulo);
        }

        // GET: TbArticuloes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TbArticulos == null)
            {
                return NotFound();
            }

            var tbArticulo = await _context.TbArticulos
                .FirstOrDefaultAsync(m => m.CodArt == id);
            if (tbArticulo == null)
            {
                return NotFound();
            }

            return View(tbArticulo);
        }

        // POST: TbArticuloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TbArticulos == null)
            {
                return Problem("Entity set 'Bdventas2023WebapiContext.TbArticulos'  is null.");
            }
            var tbArticulo = await _context.TbArticulos.FindAsync(id);
            if (tbArticulo != null)
            {
                _context.TbArticulos.Remove(tbArticulo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbArticuloExists(string id)
        {
          return (_context.TbArticulos?.Any(e => e.CodArt == id)).GetValueOrDefault();
        }
    }
}
