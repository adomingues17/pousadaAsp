using System.ComponentModel.DataAnnotations;

namespace pousadaAsp.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Endereço do Cliente")]
    public string Endereco { get; set; }

    [Required]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP deve conter 8 números!")]
    public string CEP { get; set; }

}
