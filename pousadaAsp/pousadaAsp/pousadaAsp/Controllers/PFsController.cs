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
    public class PFsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PFsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PFs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PFs.ToListAsync());
        }

        // GET: PFs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pF = await _context.PFs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pF == null)
            {
                return NotFound();
            }

            return View(pF);
        }

        // GET: PFs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PFs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomePF,CPF,Endereco,CEP")] PF pF)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pF);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pF);
        }

        // GET: PFs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pF = await _context.PFs.FindAsync(id);
            if (pF == null)
            {
                return NotFound();
            }
            return View(pF);
        }

        // POST: PFs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomePF,CPF,Endereco,CEP")] PF pF)
        {
            if (id != pF.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pF);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PFExists(pF.Id))
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
            return View(pF);
        }

        // GET: PFs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pF = await _context.PFs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pF == null)
            {
                return NotFound();
            }

            return View(pF);
        }

        // POST: PFs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pF = await _context.PFs.FindAsync(id);
            if (pF != null)
            {
                _context.PFs.Remove(pF);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PFExists(int id)
        {
            return _context.PFs.Any(e => e.Id == id);
        }
    }
}
