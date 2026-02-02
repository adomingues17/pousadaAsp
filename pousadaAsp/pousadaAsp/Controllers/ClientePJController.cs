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
    public class ClientePJController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientePJController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClientePJs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClientePJs.Include(c => c.PJ);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClientePJs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientePJ = await _context.ClientePJs
                .Include(c => c.PJ)
                .FirstOrDefaultAsync(m => m.IdPessoaJuridica == id);
            if (clientePJ == null)
            {
                return NotFound();
            }

            return View(clientePJ);
        }

        // GET: ClientePJs/Create
        public IActionResult Create()
        {
            ViewData["UsuarioPJ"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ClientePJs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPessoaJuridica,NomeCliente,CNPJ,EndrecoCliente,CEP,UsuarioPJ")] ClientePJ clientePJ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientePJ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioPJ"] = new SelectList(_context.Users, "Id", "Id", clientePJ.UsuarioPJ);
            return View(clientePJ);
        }

        // GET: ClientePJs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientePJ = await _context.ClientePJs.FindAsync(id);
            if (clientePJ == null)
            {
                return NotFound();
            }
            ViewData["UsuarioPJ"] = new SelectList(_context.Users, "Id", "Id", clientePJ.UsuarioPJ);
            return View(clientePJ);
        }

        // POST: ClientePJs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPessoaJuridica,NomeCliente,CNPJ,EndrecoCliente,CEP,UsuarioPJ")] ClientePJ clientePJ)
        {
            if (id != clientePJ.IdPessoaJuridica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientePJ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientePJExists(clientePJ.IdPessoaJuridica))
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
            ViewData["UsuarioPJ"] = new SelectList(_context.Users, "Id", "Id", clientePJ.UsuarioPJ);
            return View(clientePJ);
        }

        // GET: ClientePJs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientePJ = await _context.ClientePJs
                .Include(c => c.PJ)
                .FirstOrDefaultAsync(m => m.IdPessoaJuridica == id);
            if (clientePJ == null)
            {
                return NotFound();
            }

            return View(clientePJ);
        }

        // POST: ClientePJs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientePJ = await _context.ClientePJs.FindAsync(id);
            if (clientePJ != null)
            {
                _context.ClientePJs.Remove(clientePJ);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientePJExists(int id)
        {
            return _context.ClientePJs.Any(e => e.IdPessoaJuridica == id);
        }
    }
}
