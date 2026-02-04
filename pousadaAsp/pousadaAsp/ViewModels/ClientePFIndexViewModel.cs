using System.Collections.Generic;

namespace pousadaAsp.ViewModels;

public class ClientePFIndexViewModel
{
    public List<ClientePFViewModel> Clientes { get; set; } = new();
    public int TotalPaginas { get; set; }
    public int PaginaAtual { get; set; }
    public string Busca {  get; set; }
    public string EmailLogado { get; set; }
}
