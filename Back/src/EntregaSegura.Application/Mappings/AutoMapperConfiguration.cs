using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Identity;

namespace EntregaSegura.Application.Mappings;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Condominio, CondominioDTO>().ReverseMap();
        CreateMap<Transportadora, TransportadoraDTO>().ReverseMap();

        CreateMap<EntregaDTO, Entrega>();
        CreateMap<Entrega, EntregaDTO>();

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

        CreateMap<UsuarioDTO, User>();
        CreateMap<User, UsuarioDTO>();

        CreateMap<LoginUsuarioDTO, User>();
        CreateMap<User, LoginUsuarioDTO>();
    }
}