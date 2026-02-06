using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pousadaAsp.Models;

namespace pousadaAsp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PF> PFs { get; set; }

    public DbSet<PJ> PJs { get; set; }        

    public DbSet<Quarto> Quartos { get; set; }

    public DbSet<Cliente> Clientes { get; set; }

    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>().ToTable("Clientes");
        modelBuilder.Entity<PF>().ToTable("PFs");
        modelBuilder.Entity<PJ>().ToTable("PJs");
    }


}
