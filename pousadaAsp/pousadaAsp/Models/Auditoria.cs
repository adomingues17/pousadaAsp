namespace pousadaAsp.Models;

public class Auditoria
{
    public int Id { get; set; }
    public string UsuarioId { get; set; }
    public string Entidade { get; set; }

    public string Operacao { get; set; } // Create, Update, Delete
    
    public DateTime DataHora { get; set; } 
    public string ValoresAntigos { get; set; } 
    public string ValoresNovos { get; set; }

}
