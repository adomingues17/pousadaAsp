using Microsoft.AspNetCore.Mvc;
using pousadaAsp.Services;
using pousadaAsp.ViewModels;

namespace pousadaAsp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteService _service;

        public ClientesController(ClienteService service)
        {
            _service = service;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var clientes = await _service.ListarClientesAsync();
            return View(clientes);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Log dos erros
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var e in errors)
                {
                    Console.WriteLine($"Erro: {e.ErrorMessage}");
                }
                return View(model);
            }

            Console.WriteLine($"TipoCliente recebido: {model.TipoCliente}");
            await _service.CadastrarClienteAsync(model);
            return RedirectToAction("Index");
        }

    }
}
