using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pousadaAsp.Models;

public class Reserva
{
    [Key]
    public int Id { get; set; }

    public int? ClienteId { get; set; }

    [ForeignKey(nameof(ClienteId))]
    public virtual Cliente? IdCliente { get; set; }

    public string IdentityId { get; set; }

    [ForeignKey(nameof(IdentityId))]
    public virtual IdentityUser? IdIdentity { get; set; }

    public int QuartoId { get; set; }

    [ForeignKey(nameof(QuartoId))]
    public virtual Quarto? IdQuarto { get; set; }

    public DateTime DataEntrada { get; set; }
    public DateTime DataSaida { get; set; }

    public int QuantidadeDias { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Adicionais { get; set; }

    public decimal CalcularValor()
    {
        var dias = (DataSaida - DataEntrada).Days + 1;
        QuantidadeDias = dias > 0 ? dias : 1;

        decimal valorBase = QuantidadeDias * IdQuarto.diaria;

        ValorTotal = valorBase + Adicionais;
        return ValorTotal;
    }

}
