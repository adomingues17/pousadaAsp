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

    public DbSet<ClientePF> ClientePFs { get; set; } = default!;
    public DbSet<ClientePJ> ClientePJs { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
     

    }
}

/*
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pousadaAsp.Models;

namespace pousadaAsp.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<ClientePF> ClientePFs { get; set; } = default!;
    public DbSet<ClientePJ> ClientePJs { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

      
        builder.Entity<ClientePJ>(entity =>
        {
            entity.ToTable("ClientePJ");
            entity.HasIndex(c => c.CNPJ).IsUnique();
        });


    }
}
*/