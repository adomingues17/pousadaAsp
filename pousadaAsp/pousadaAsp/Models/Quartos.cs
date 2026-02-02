using System.ComponentModel.DataAnnotations;

namespace pousadaAsp.Models;

public class Quartos
{
    [Key]
    public int IdQuarto { get; set; }

    [Required]
    public int CamaCasal { get; set; }

    [Required]
    public int CamaSolteiro { get; set; }

}
