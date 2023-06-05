using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validations;
using FluentAssertions;

namespace EntregaSegura.Domain.Tests;

public class CondominioTests
{
    private readonly CondominioValidator _validator;

    public CondominioTests()
    {
        _validator = new CondominioValidator();
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Nome_For_Nulo()
    {
        var condominio = new Condominio(null, "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Nome));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_CNPJ_For_Nulo()
    {
        var condominio = new Condominio("Condominio Paulista", null, "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Cnpj));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_CNPJ_For_Invalido()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Cnpj));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Telefone_For_Nulo()
    {
        var condominio = new Condominio("Condominio Paulista", "22.264.404/0001-25", null, "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Telefone));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Telefone_For_Invalido()
    {
        var condominio = new Condominio("Condominio Paulista", "22.264.404/0001-25", "(11) 23-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Telefone));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Email_For_Nulo()
    {
        var condominio = new Condominio("Condominio Paulista", "22.264.404/0001-25", "(11) 2345-6789", null,
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Email));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Email_For_Invalido()
    {
        var condominio = new Condominio("Condominio Paulista", "22.264.404/0001-25", "(11) 2345-6789", "condominio@",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Email));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_CEP_For_Invalido()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "123", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Cep));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Logradouro_For_Nulo()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, null, 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Logradouro));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Logradouro_For_Curto_Longo_Demais()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "A", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Logradouro));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Numero_For_Zero_Ou_Negativo()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 0, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Numero));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Numero_For_Maior_Que_9999()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 10000, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Numero));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Bairro_For_Nulo()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", null, "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Bairro));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Bairro_For_Curto_Ou_Longo_Demais()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "A", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Bairro));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Cidade_For_Nulo()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", null, "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Cidade));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Cidade_For_Curta_Ou_Longa_Demais()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "A", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Cidade));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Estado_For_Nulo()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", null);
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Estado));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Estado_For_Invalido()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "XX");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.Estado));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_QuantidadeBlocos_For_Menor_Que_1_Ou_Maior_Que_20()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            0, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.QuantidadeBlocos));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_QuantidadeUnidades_For_Menor_Que_1()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 0, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.QuantidadeUnidades));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_QuantidadeUnidades_For_Maior_Que_8()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 9, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.QuantidadeUnidades));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_QuantidadeAndares_For_Menor_Que_1()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 0, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.QuantidadeAndares));
    }
    
    [Fact]
    public void Deve_Gerar_Erro_Quando_QuantidadeAndares_For_Maior_Que_40()
    {
        var condominio = new Condominio("Condominio Paulista", "04.238.377/0001-80", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 41, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(condominio.QuantidadeAndares));
    }

    [Fact]
    public void Nao_Deve_Gerar_Erros_Quando_Dominio_For_Valido()
    {
        var condominio = new Condominio("Condominio Paulista", "22.264.404/0001-25", "(11) 2345-6789", "condominio@condominio.com.br",
            5, 5, 5, "Avenida Paulista", 1500, "01311000", "Bela Vista", "São Paulo", "SP");
        var resultadoValidacao = _validator.Validate(condominio);
        resultadoValidacao.IsValid.Should().BeTrue();
    }
}