using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pousadaAsp.Models;

public class ClientePF
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório.")]
    [Display(Name = "Nome do Cliente")]
    public string NomeCliente { get; set; }

    [Required(ErrorMessage = "CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 números!")]
    [Display(Name = "CPF")]
    public string CPF { get; set; }

    [Required]
    [Display(Name = "Endereço do Cliente")]
    public string EnderecoCliente { get; set; }

    [Required]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP deve conter 8 números!")]
    public string CEP { get; set; }

    [Required]
    public string IdUsuarioPF { get; set; }

    [ForeignKey(nameof(IdUsuarioPF))]
    public virtual IdentityUser? PF { get; set; }

    //Auditoria
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }     
    public string UsuarioCriacao { get; set; }
    public string? UsuarioAtualizacao { get; set; }

}
