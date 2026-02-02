using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pousadaAsp.Data;
using pousadaAsp.Models;

namespace pousadaAsp.Controllers

{
    [Authorize]
    public class ClientePFController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public ClientePFController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cliente
        public async Task<IActionResult> Index(string busca, int pagina = 1, int tamanhoPagina = 4)
        {
            var usuario = await _userManager.GetUserAsync(User);

            //var query = _context.ClientePFs.AsQueryable();
            var query = _context.ClientePFs
                .Where(c => c.IdUsuarioPF == usuario.Id);

            if (!string.IsNullOrEmpty(busca))
                query = query.Where(c => c.NomeCliente.Contains(busca));


            int totalRegistros = await query.CountAsync();

            var clientes = await query
                .OrderBy(c => c.NomeCliente)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            ViewBag.TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanhoPagina);
            ViewBag.PaginaAtual = pagina;
            ViewBag.Busca = busca;
            ViewBag.EmailLogado = usuario.Email;
            return View(clientes);
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.ClientePFs
                .Include(c => c.PF) // se houver relacionamentos --Include(c => c.RelacionamentoExemplo) 
                .FirstOrDefaultAsync(m => m.Id == id); //FirstOrDefaultAsync(m => m.Id

            if (cliente == null) return NotFound();

            var usuario = await _userManager.GetUserAsync(User);
            if (cliente.IdUsuarioPF != usuario.Id) return Forbid();

            return View(cliente);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientePF model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.GetUserAsync(User);

                var cliente = new ClientePF
                {
                    NomeCliente = model.NomeCliente,
                    CPF = model.CPF,
                    EnderecoCliente = model.EnderecoCliente,
                    CEP = model.CEP,
                    IdUsuarioPF = usuario.Id
                };

                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.ClientePFs.FindAsync(id);
            if (cliente == null) return NotFound();

            var usuario = await _userManager.GetUserAsync(User);
            if (cliente.IdUsuarioPF != usuario.Id)
                return Forbid();

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientePF model)
        {
            if (id != model.Id) return NotFound();

            var usuario = await _userManager.GetUserAsync(User);
            var cliente = await _context.ClientePFs.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null) return NotFound();
            if (cliente.IdUsuarioPF != usuario.Id) return Forbid();

            if (ModelState.IsValid)
            {
                cliente.NomeCliente = model.NomeCliente;
                cliente.CPF = model.CPF;
                cliente.EnderecoCliente = model.EnderecoCliente;
                cliente.CEP = model.CEP;

                _context.Update(cliente);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        /*
        // POST: Cliente/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPessoaFisica,NomeCliente,CPF,EnderecoCliente")] ClientePF cliente)
        {
            if (id != cliente.IdPessoaFisica) return NotFound();

            var usuario = await _userManager.GetUserAsync(User);

            // valida se o registro pertence ao usuário logado
            var clienteBanco = await _context.ClientePFs.AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdPessoaFisica == id); 
            
            if (clienteBanco == null) return NotFound(); 
            if (clienteBanco.UsuarioPF != usuario.Id) 
                return Forbid(); // impede alteração de dados de outro usuário

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdPessoaFisica))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index"); //return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }
        */


        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.ClientePFs
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cliente == null) return NotFound();

            var usuario = await _userManager.GetUserAsync(User);
            if (cliente.IdUsuarioPF != usuario.Id) return Forbid();

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _userManager.GetUserAsync(User);
            var cliente = await _context.ClientePFs.FindAsync(id);

            if (cliente == null) return NotFound();
            if (cliente.IdUsuarioPF != usuario.Id)
                return Forbid();
         
            _context.ClientePFs.Remove(cliente);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");   //return RedirectToAction(nameof(Index));
                        
        }

        private bool ClienteExists(int id)
        {
            return _context.ClientePFs.Any(e => e.Id == id);
        }
    }
}


/*
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
    public class ClientePFsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientePFsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClientePFs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClientePF.Include(c => c.PF);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClientePFs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientePF = await _context.ClientePF
                .Include(c => c.PF)
                .FirstOrDefaultAsync(m => m.IdPessoaFisica == id);
            if (clientePF == null)
            {
                return NotFound();
            }

            return View(clientePF);
        }

        // GET: ClientePFs/Create
        public IActionResult Create()
        {
            ViewData["UsuarioPF"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ClientePFs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPessoaFisica,NomeCliente,CPF,EndrecoCliente,CEP,UsuarioPF")] ClientePF clientePF)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientePF);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioPF"] = new SelectList(_context.Users, "Id", "Id", clientePF.UsuarioPF);
            return View(clientePF);
        }

        // GET: ClientePFs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientePF = await _context.ClientePF.FindAsync(id);
            if (clientePF == null)
            {
                return NotFound();
            }
            ViewData["UsuarioPF"] = new SelectList(_context.Users, "Id", "Id", clientePF.UsuarioPF);
            return View(clientePF);
        }

        // POST: ClientePFs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPessoaFisica,NomeCliente,CPF,EndrecoCliente,CEP,UsuarioPF")] ClientePF clientePF)
        {
            if (id != clientePF.IdPessoaFisica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientePF);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientePFExists(clientePF.IdPessoaFisica))
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
            ViewData["UsuarioPF"] = new SelectList(_context.Users, "Id", "Id", clientePF.UsuarioPF);
            return View(clientePF);
        }

        // GET: ClientePFs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientePF = await _context.ClientePF
                .Include(c => c.PF)
                .FirstOrDefaultAsync(m => m.IdPessoaFisica == id);
            if (clientePF == null)
            {
                return NotFound();
            }

            return View(clientePF);
        }

        // POST: ClientePFs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientePF = await _context.ClientePF.FindAsync(id);
            if (clientePF != null)
            {
                _context.ClientePF.Remove(clientePF);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientePFExists(int id)
        {
            return _context.ClientePF.Any(e => e.IdPessoaFisica == id);
        }
    }
}
*/