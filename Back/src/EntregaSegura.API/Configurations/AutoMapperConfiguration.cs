using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.DTOs.Condominios;
using EntregaSegura.Application.DTOs.Transportadoras;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.API.Configurations;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Condominio, CondominioDTO>().ReverseMap();
        CreateMap<Transportadora, TransportadoraDTO>().ReverseMap();
        
        CreateMap<EntregaDTO, Entrega>();
        CreateMap<Entrega, EntregaDTO>();

        CreateMap<FuncionarioDTO, Funcionario>();
        CreateMap<Funcionario, FuncionarioDTO>();

        CreateMap<MoradorDTO, Morador>();
        CreateMap<Morador, MoradorDTO>();
        
        CreateMap<UnidadeDTO, Unidade>();
        CreateMap<Unidade, UnidadeDTO>()
            .ForMember(dest => dest.NomeCondominio, opt => opt.MapFrom(src => src.Condominio.Nome));
    }
}