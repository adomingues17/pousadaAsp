using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pousadaAsp.Models;

namespace pousadaAsp.Data.Configurations;

public class ClientePFConfiguration : IEntityTypeConfiguration<ClientePF>
{
    public void Configure(EntityTypeBuilder<ClientePF> entity)
    {
        entity.ToTable("ClientePF");

        entity.HasIndex(c => c.CPF).IsUnique();
        entity.HasIndex(c => c.NomeCliente);
        entity.HasIndex(c => c.CEP);

        entity.Property(c => c.NomeCliente).HasMaxLength(120)
              .IsRequired();

        entity.Property(c => c.EnderecoCliente).HasMaxLength(140)
              .IsRequired();

        entity.Property(c => c.CEP).HasMaxLength(8)
              .IsRequired();

        entity.HasOne(c => c.PF).WithMany()
              .HasForeignKey(c => c.IdUsuarioPF)
              .OnDelete(DeleteBehavior.Restrict);

    }    
}
