using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validations;
using FluentAssertions;

namespace EntregaSegura.Domain.Tests;

public class MoradorTests
{
    private readonly MoradorValidator _validator;

    public MoradorTests()
    {
        _validator = new MoradorValidator();
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Nome_For_Nulo()
    {
        var morador = new Morador() { Nome = null, Cpf = "111.111.111-11", Telefone = "(11) 2345-6789", Email = "morador@morador.com.br", UnidadeId = 1 };
        var resultadoValidacao = _validator.Validate(morador);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(morador.Nome));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Nome_For_Muito_Longo()
    {
        var nomeMuitoLongo = new string('a', 101);
        var morador = new Morador() { Nome = nomeMuitoLongo, Cpf = "111.111.111-11", Telefone = "(11) 2345-6789", Email = "morador@morador.com.br", UnidadeId = 1 };
        var resultadoValidacao = _validator.Validate(morador);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(morador.Nome));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Nome_For_Muito_Curto()
    {
        var morador = new Morador() { Nome = "a", Cpf = "111.111.111-11", Telefone = "(11) 2345-6789", Email = "morador@morador.com.br", UnidadeId = 1 };
        var resultadoValidacao = _validator.Validate(morador);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(morador.Nome));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_CPF_For_Invalido()
    {
        var morador = new Morador() { Nome = "Morador", Cpf = "123", Telefone = "(11) 2345-6789", Email = "morador@morador.com.br", UnidadeId = 1 };
        var resultadoValidacao = _validator.Validate(morador);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(morador.Cpf));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Telefone_For_Invalido()
    {
        var morador = new Morador() { Nome = "Morador", Cpf = "111.111.111-11", Telefone = "123", Email = "morador@morador.com.br", UnidadeId = 1 };
        var resultadoValidacao = _validator.Validate(morador);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(morador.Telefone));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Email_For_Invalido()
    {
        var morador = new Morador() { Nome = "Morador", Cpf = "111.111.111-11", Telefone = "(11) 2345-6789", Email = "invalid", UnidadeId = 1 };
        var resultadoValidacao = _validator.Validate(morador);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(morador.Email));
    }

    [Fact]
    public void Nao_Deve_Gerar_Erros_Quando_Dominio_For_Valido()
    {
        var morador = new Morador() { Nome = "Morador", Cpf = "111.111.111-11", Telefone = "(11) 2345-6789", Email = "morador@morador.com.br", UnidadeId = 1 };
        var resultadoValidacao = _validator.Validate(morador);
        resultadoValidacao.IsValid.Should().BeTrue();
    }
}
