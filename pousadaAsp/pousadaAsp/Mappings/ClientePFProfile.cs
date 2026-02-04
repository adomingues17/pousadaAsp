using AutoMapper;
using pousadaAsp.Models;
using pousadaAsp.ViewModels;

namespace pousadaAsp.Mappings;

public class ClientePFProfile : Profile
{
    public ClientePFProfile()
    {
        CreateMap<ClientePF, ClientePFViewModel>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NomeCliente))
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.EnderecoCliente))
            .ReverseMap()
            .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.EnderecoCliente, opt => opt.MapFrom(src => src.Endereco));

    }
}
