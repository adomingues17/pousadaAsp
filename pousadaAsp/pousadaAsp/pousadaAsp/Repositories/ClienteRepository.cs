using pousadaAsp.Models;
using pousadaAsp.Data;
using Microsoft.EntityFrameworkCore;

namespace pousadaAsp.Repositories
{
    public interface IClienteRepository
    {
        Task AddPFAsync(PF pf);
        Task AddPJAsync(PJ pj);
        Task<List<Cliente>> GetAllAsync();
    }

    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPFAsync(PF pf)
        {
            _context.PFs.Add(pf);
            await _context.SaveChangesAsync();
        }

        public async Task AddPJAsync(PJ pj)
        {
            _context.PJs.Add(pj);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            var clientes = new List<Cliente>();
            clientes.AddRange(await _context.PFs.ToListAsync());
            clientes.AddRange(await _context.PJs.ToListAsync());
            return clientes;
        }
    }
}
