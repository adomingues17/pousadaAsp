using System.ComponentModel.DataAnnotations;

namespace pousadaAsp.Models;

public class PF : Cliente
{
    /*
    [Key]
    public int Id { get; set; }
    */
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [Display(Name = "Nome do Cliente")]
    public string NomePF { get; set; }

    [Required(ErrorMessage = "CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 números!")]
    [Display(Name = "CPF")]
    public string CPF { get; set; }

}
