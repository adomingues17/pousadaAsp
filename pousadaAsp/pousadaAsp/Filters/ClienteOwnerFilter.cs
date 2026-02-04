using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using pousadaAsp.Services;
using System.Threading.Tasks;

namespace pousadaAsp.Filters;

public class ClienteOwnerFilter : IAsyncActionFilter
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ClientePFService _service;

    public ClienteOwnerFilter(UserManager<IdentityUser> userManager, ClientePFService service)
    {
        _userManager = userManager;
        _service = service;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("id", out var idObj) && idObj is int id)
        {
            var cliente = await _service.BuscarPorId(id);
            var usuario = await _userManager.GetUserAsync(context.HttpContext.User);

            if (cliente == null)
            {
                context.Result = new NotFoundResult();
                return;
            }

            if (cliente.IdUsuarioPF != usuario.Id)
            {
                context.Result = new ForbidResult();
                return;
            }

        }
        await next();
    }
}
