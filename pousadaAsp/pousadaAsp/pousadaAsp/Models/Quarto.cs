using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pousadaAsp.Models;

public class Quarto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal diaria { get; set; }

}
