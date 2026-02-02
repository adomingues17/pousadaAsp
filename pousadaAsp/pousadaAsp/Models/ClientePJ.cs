using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pousadaAsp.Models;

public class ClientePJ
{

    [Key]
    public int IdPessoaJuridica { get; set; }

    [Required]
    [Display(Name = "Razão Social")]
    public string? NomeCliente { get; set; }

    [Required]
    [Display(Name = "CNPJ"), MaxLength(14)]
    public string? CNPJ { get; set; }

    [Required]
    [Display(Name = "Endereço do Cliente")]
    public string? EndrecoCliente { get; set; }

    [Required]
    public string? CEP { get; set; }

    [Required]
    public string? UsuarioPJ { get; set; }

    [ForeignKey("UsuarioPJ")]
    public virtual IdentityUser? PJ { get; set; }

}



/*
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pousadaAsp.Models;

public class ClientePF
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(120)]
    [Display(Name = "Nome do Cliente")]
    public string NomeCliente { get; set; }

    [Required, StringLength(11, MinimumLength = 11)]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 números!")]
    [Display(Name = "CPF")]
    public string CPF { get; set; }

    [Required, MaxLength(200)]
    [Display(Name = "Endereço do Cliente")]
    public string EnderecoCliente { get; set; }

    [Required, StringLength(8, MinimumLength = 8)]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP deve conter 8 números!")]
    public string CEP { get; set; }

    [Required]
    public string IdUsuarioPF { get; set; }
    //public int? UsuarioPF { get; set; }

    [ForeignKey(nameof(IdUsuarioPF))]
    public virtual IdentityUser? PF { get; set; }

    /*
    [ForeignKey("UsuarioPF")]
    public virtual IdentityUser? PF { get; set; }
    */