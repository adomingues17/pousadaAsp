using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pousadaAsp.Filters;
using pousadaAsp.Models;
using pousadaAsp.Services;
using pousadaAsp.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pousadaAsp.Controllers

{
    [Authorize]
    public class ClientePFController : Controller
    {
        private readonly ClientePFService _service;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientePFController> _logger;

        public ClientePFController(ClientePFService service, UserManager<IdentityUser> userManager, 
            IMapper mapper, ILogger<ClientePFController> logger)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Cliente
        public async Task<IActionResult> Index(string busca, int pagina = 1, int tamanhoPagina = 4)
        {
            var usuario = await _userManager.GetUserAsync(User);

            var clientes = await _service.ListaPorUsuario(usuario.Id, busca, pagina, tamanhoPagina);
            var viewModels = _mapper.Map<List<ClientePFViewModel>>(clientes.Items);
            
            var model = new ClientePFIndexViewModel
            {
                Clientes = viewModels,
                TotalPaginas = clientes.TotalPaginas,
                PaginaAtual = pagina,
                Busca = busca,
                EmailLogado = usuario.Email
            };
            return View(model);
        }

        // GET: Cliente/Details/5
        [ServiceFilter(typeof(ClienteOwnerFilter))]
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _service.BuscarPorId(id);
            if (cliente == null) return NotFound();

            var usuario = await _userManager.GetUserAsync(User);
            if (cliente.IdUsuarioPF != usuario.Id) return Forbid();

            var viewModel = _mapper.Map<ClientePFViewModel>(cliente);
            return View(viewModel);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientePFViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            
            try
            {
                var usuario = await _userManager.GetUserAsync(User);
                var cliente = _mapper.Map<ClientePF>(viewModel);

                cliente.IdUsuarioPF = usuario.Id;
                cliente.DataCriacao = DateTime.UtcNow;
                cliente.UsuarioCriacao = usuario.Email;

                await _service.Criar(cliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar cliente PF");
                ModelState.AddModelError("", "Erro ao criar cliente. Tente novamente.");
                return View(viewModel);
            }
        }

        // GET: Cliente/Edit/5
        [ServiceFilter(typeof(ClienteOwnerFilter))]
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _service.BuscarPorId(id);
            if (cliente == null) return NotFound();

            var usuario = await _userManager.GetUserAsync(User);
            if (cliente.IdUsuarioPF != usuario.Id) return Forbid();

            var viewModel = _mapper.Map<ClientePFViewModel>(cliente);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientePFViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            var cliente = await _service.BuscarPorId(id);
            if (cliente == null) return NotFound();

            var usuario = await _userManager.GetUserAsync(User);
            if (cliente.IdUsuarioPF != usuario.Id) return Forbid();

            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                _mapper.Map(viewModel, cliente);
                cliente.DataAtualizacao = DateTime.UtcNow;
                cliente.UsuarioAtualizacao = usuario.Email;

                await _service.Atualizar(cliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar cliente PF.");
                ModelState.AddModelError("", "Erro ao atualizar cliente. Tente novamente.");
                return View(viewModel);
            }
        }

        // GET: Cliente/Delete/5
        [ServiceFilter(typeof(ClienteOwnerFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _service.BuscarPorId(id);
            if (cliente == null) return NotFound();

            var usuario = await _userManager.GetUserAsync(User);
            if (cliente.IdUsuarioPF != usuario.Id) return Forbid();

            var viewModel = _mapper.Map<ClientePFViewModel>(cliente);
            return View(viewModel);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _service.BuscarPorId(id);
            if (cliente == null) return NotFound();

            var usuario = await _userManager.GetUserAsync(User);
            if (cliente.IdUsuarioPF != usuario.Id) return Forbid();
         
            try
            {
                await _service.Remover(cliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir cliente PF");
                ModelState.AddModelError("", "Erro ao excluir cliente. Tente novamente.");
                var viewModel = _mapper.Map<ClientePFViewModel>(cliente);
                return View(viewModel);
            }
            
                        
        }
    }
}

