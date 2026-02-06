using System.ComponentModel.DataAnnotations;

namespace pousadaAsp.Models;

public class PJ : Cliente
{
    /*
    [Key]
    public int Id { get; set; }
    */
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [Display(Name = "Nome do Cliente")]
    public string NomePJ { get; set; }

    [Required(ErrorMessage = "CNPJ é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CNPJ deve conter 14 números!")]
    [Display(Name = "CNPJ")]
    public string CNPJ { get; set; }

}
