using Microsoft.EntityFrameworkCore;
using pousadaAsp.Data;
using pousadaAsp.Models;

namespace pousadaAsp.Services;

public class PageResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalPaginas { get; set; }
}

public class ClientePFService
{
    private readonly ApplicationDbContext _context;

    public ClientePFService(ApplicationDbContext context)    
    {
        _context = context;
    }

    public async Task<PageResult<ClientePF>> ListaPorUsuario(string usuarioId, string busca, int pagina, int tamanhoPagina)
    {
        var query = _context.ClientePFs.Where(c => c.IdUsuarioPF == usuarioId);

        if (!string.IsNullOrEmpty(busca))
            query = query.Where(c => c.NomeCliente.Contains(busca));

        int totalRegistros = await query.CountAsync();

        var clientes = await query
            .OrderBy(c => c.NomeCliente)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return new PageResult<ClientePF>
        {
            Items = clientes,
            TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanhoPagina)

        };
    }

    public async Task<ClientePF> BuscarPorId(int id) =>
        await _context.ClientePFs.FindAsync(id);     

    public async Task Criar(ClientePF cliente)
    {
        _context.ClientePFs.Add(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(ClientePF cliente)
    {
        _context.ClientePFs.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task Remover(ClientePF cliente)
    {
        _context.ClientePFs.Remove(cliente);
        await _context.SaveChangesAsync();
    }
}
