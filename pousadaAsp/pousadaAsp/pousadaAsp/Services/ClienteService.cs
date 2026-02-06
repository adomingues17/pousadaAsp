using pousadaAsp.Models;
using pousadaAsp.Repositories;
using pousadaAsp.ViewModels;

namespace pousadaAsp.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task CadastrarClienteAsync(ClienteViewModel model)
        {
            if (model.TipoCliente == "PF")
            {
                var pf = new PF
                {
                    Endereco = model.Endereco,
                    CEP = model.CEP,
                    NomePF = model.NomePF,
                    CPF = model.CPF
                };
                await _repository.AddPFAsync(pf);
            }
            else if (model.TipoCliente == "PJ")
            {
                var pj = new PJ
                {
                    Endereco = model.Endereco,
                    CEP = model.CEP,
                    NomePJ = model.NomePJ,
                    CNPJ = model.CNPJ
                };
                await _repository.AddPJAsync(pj);
            }
        }

        public async Task<List<Cliente>> ListarClientesAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
