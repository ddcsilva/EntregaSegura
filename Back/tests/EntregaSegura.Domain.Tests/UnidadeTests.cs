using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validations;
using FluentAssertions;

namespace EntregaSegura.Domain.Tests;

public class UnidadeTests
{
    private readonly UnidadeValidator _validator;

    public UnidadeTests()
    {
        _validator = new UnidadeValidator();
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Bloco_For_Nulo()
    {
        var unidade = new Unidade(1, 1, null, 1);
        var resultadoValidacao = _validator.Validate(unidade);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(unidade.Bloco));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Bloco_For_Muito_Longo()
    {
        var blocoMuitoLongo = new string('a', 21);
        var unidade = new Unidade(1, 1, blocoMuitoLongo, 1);
        var resultadoValidacao = _validator.Validate(unidade);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(unidade.Bloco));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Numero_For_Maior_Que_10()
    {
        var unidade = new Unidade(11, 1, "A", 1);
        var resultadoValidacao = _validator.Validate(unidade);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(unidade.Numero));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Andar_For_Maior_Que_40()
    {
        var unidade = new Unidade(1, 41, "A", 1);
        var resultadoValidacao = _validator.Validate(unidade);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(unidade.Andar));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_CondominioId_For_Nulo()
    {
        var unidade = new Unidade(1, 1, "A", 0);
        var resultadoValidacao = _validator.Validate(unidade);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(unidade.CondominioId));
    }

    [Fact]
    public void Nao_Deve_Gerar_Erros_Quando_Dominio_For_Valido()
    {
        var unidade = new Unidade(1, 1, "A", 1);
        var resultadoValidacao = _validator.Validate(unidade);
        resultadoValidacao.IsValid.Should().BeTrue();
    }
}
