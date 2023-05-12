using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.API.Configurations;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Condominio, CondominioDTO>().ReverseMap();
        CreateMap<Entrega, EntregaDTO>().ReverseMap();
        CreateMap<Funcionario, FuncionarioDTO>().ReverseMap();
        CreateMap<Morador, MoradorDTO>().ReverseMap();
        CreateMap<Transportadora, TransportadoraDTO>().ReverseMap();
        CreateMap<Unidade, UnidadeDTO>().ReverseMap();
    }
}