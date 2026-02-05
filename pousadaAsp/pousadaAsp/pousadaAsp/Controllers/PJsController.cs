using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pousadaAsp.Data;
using pousadaAsp.Models;

namespace pousadaAsp.Controllers
{
    public class PJsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PJsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PJs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PJs.ToListAsync());
        }

        // GET: PJs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pJ = await _context.PJs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pJ == null)
            {
                return NotFound();
            }

            return View(pJ);
        }

        // GET: PJs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PJs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomePJ,CNPJ,Endereco,CEP")] PJ pJ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pJ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pJ);
        }

        // GET: PJs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pJ = await _context.PJs.FindAsync(id);
            if (pJ == null)
            {
                return NotFound();
            }
            return View(pJ);
        }

        // POST: PJs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomePJ,CNPJ,Endereco,CEP")] PJ pJ)
        {
            if (id != pJ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pJ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PJExists(pJ.Id))
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
            return View(pJ);
        }

        // GET: PJs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pJ = await _context.PJs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pJ == null)
            {
                return NotFound();
            }

            return View(pJ);
        }

        // POST: PJs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pJ = await _context.PJs.FindAsync(id);
            if (pJ != null)
            {
                _context.PJs.Remove(pJ);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PJExists(int id)
        {
            return _context.PJs.Any(e => e.Id == id);
        }
    }
}
