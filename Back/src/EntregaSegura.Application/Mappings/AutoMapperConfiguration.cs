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

        CreateMap<Pessoa, PessoaDTO>().ReverseMap();

        CreateMap<UnidadeDTO, Unidade>();
        CreateMap<Unidade, UnidadeDTO>()
            .ForMember(dest => dest.NomeCondominio, opt => opt.MapFrom(src => src.Condominio.Nome))
            .ForMember(dest => dest.DescricaoUnidade, opt => opt.MapFrom(src => $"Unidade {src.Numero}, Bloco {src.Bloco}, {src.Andar}º andar"));

        CreateMap<MoradorDTO, Morador>()
            .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));
        CreateMap<Morador, MoradorDTO>()
            .ForMember(dest => dest.DescricaoUnidade, opt => opt.MapFrom(src => $"Unidade {src.Unidade.Numero}, Bloco {src.Unidade.Bloco}, {src.Unidade.Andar}º andar"))
            .ForMember(dest => dest.NomeCondominio, opt => opt.MapFrom(src => src.Unidade.Condominio.Nome))
            .ForMember(dest => dest.CondominioId, opt => opt.MapFrom(src => src.Unidade.Condominio.Id))
            .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));

        CreateMap<FuncionarioDTO, Funcionario>()
            .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));
        CreateMap<Funcionario, FuncionarioDTO>()
            .ForMember(dest => dest.NomeCondominio, opt => opt.MapFrom(src => src.Condominio.Nome))
            .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));

        CreateMap<EntregaDTO, Entrega>();
        CreateMap<Entrega, EntregaDTO>()
            .ForMember(dest => dest.NomeTransportadora, opt => opt.MapFrom(src => src.Transportadora.Nome))
            .ForMember(dest => dest.NomeMorador, opt => opt.MapFrom(src => src.Morador.Pessoa.Nome))
            .ForMember(dest => dest.EmailMorador, opt => opt.MapFrom(src => src.Morador.Pessoa.Email))
            .ForMember(dest => dest.NomeFuncionario, opt => opt.MapFrom(src => src.Funcionario.Pessoa.Nome))
            .ForMember(dest => dest.DescricaoUnidade, opt => opt.MapFrom(src => $"Unidade {src.Morador.Unidade.Numero}, Bloco {src.Morador.Unidade.Bloco}, {src.Morador.Unidade.Andar}º andar"));

        CreateMap<UsuarioDTO, Usuario>()
            .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));
        CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));
    }
}