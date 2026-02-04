using System.ComponentModel.DataAnnotations;

namespace pousadaAsp.ViewModels;

public class ClientePFViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome obrigatório.")]
    [StringLength(120)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "CPF obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 números.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "Endereço obrigatório.")]
    [StringLength(140)]
    public string Endereco { get; set; }

    [Required(ErrorMessage = "CEP é obrigatório.")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP deve conter 8 números.")]
    public string CEP { get; set; }

}
