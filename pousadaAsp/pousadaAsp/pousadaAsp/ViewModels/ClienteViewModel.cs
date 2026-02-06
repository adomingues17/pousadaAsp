namespace pousadaAsp.ViewModels;

public class ClienteViewModel
{
    public string TipoCliente { get; set; } // "PF" ou "PJ" 
    
    // Campos comuns
    public string Endereco { get; set; }    
    public string CEP { get; set; } 
    
    // Campos PF public
    public string? NomePF { get; set; }    
    public string? CPF { get; set; } 
    
    // Campos PJ
    public string? NomePJ { get; set; }    
    public string? CNPJ { get; set; }
}
