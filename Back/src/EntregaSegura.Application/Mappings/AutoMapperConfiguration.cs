using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Mappings;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Condominio, CondominioDTO>().ReverseMap();
        CreateMap<Transportadora, TransportadoraDTO>().ReverseMap();

        CreateMap<EntregaDTO, Entrega>();
        CreateMap<Entrega, EntregaDTO>()
            .ForMember(dest => dest.NomeTransportadora, opt => opt.MapFrom(src => src.Transportadora.Nome))
            .ForMember(dest => dest.NomeMorador, opt => opt.MapFrom(src => src.Morador.Nome))
            .ForMember(dest => dest.NomeFuncionario, opt => opt.MapFrom(src => src.Funcionario.Nome));

        CreateMap<FuncionarioDTO, Funcionario>();
        CreateMap<Funcionario, FuncionarioDTO>()
            .ForMember(dest => dest.NomeCondominio, opt => opt.MapFrom(src => src.Condominio.Nome));

        CreateMap<MoradorDTO, Morador>();
        CreateMap<Morador, MoradorDTO>()
            .ForMember(dest => dest.DescricaoUnidade, opt => opt.MapFrom(src => $"Unidade {src.Unidade.Numero}, Bloco {src.Unidade.Bloco}, {src.Unidade.Andar}º andar"))
            .ForMember(dest => dest.NomeCondominio, opt => opt.MapFrom(src => src.Unidade.Condominio.Nome))
            .ForMember(dest => dest.CondominioId, opt => opt.MapFrom(src => src.Unidade.Condominio.Id));

        CreateMap<UnidadeDTO, Unidade>();
        CreateMap<Unidade, UnidadeDTO>()
            .ForMember(dest => dest.NomeCondominio, opt => opt.MapFrom(src => src.Condominio.Nome))
            .ForMember(dest => dest.DescricaoUnidade, opt => opt.MapFrom(src => $"Unidade {src.Numero}, Bloco {src.Bloco}, {src.Andar}º andar"));
    }
}