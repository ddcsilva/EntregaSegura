using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validations;
using FluentAssertions;

namespace EntregaSegura.Domain.Tests;

public class TransportadoraTests
{
    private readonly TransportadoraValidator _validator;

    public TransportadoraTests()
    {
        _validator = new TransportadoraValidator();
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Nome_For_Nulo()
    {
        var transportadora = new Transportadora(null, "04.238.377/0001-80", "(11) 2345-6789", "empresa@empresa.com.br");
        var resultadoValidacao = _validator.Validate(transportadora);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(transportadora.Nome));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Nome_For_Curto()
    {
        var transportadora = new Transportadora("a", "04.238.377/0001-80", "(11) 2345-6789", "empresa@empresa.com.br");
        var resultadoValidacao = _validator.Validate(transportadora);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(transportadora.Nome));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_CNPJ_For_Invalido()
    {
        var transportadora = new Transportadora("Transportadora", "123", "(11) 2345-6789", "empresa@empresa.com.br");
        var resultadoValidacao = _validator.Validate(transportadora);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(transportadora.Cnpj));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Telefone_For_Invalido()
    {
        var transportadora = new Transportadora("Transportadora", "04.238.377/0001-80", "123", "empresa@empresa.com.br");
        var resultadoValidacao = _validator.Validate(transportadora);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(transportadora.Telefone));
    }

    [Fact]
    public void Deve_Gerar_Erro_Quando_Email_For_Invalido()
    {
        var transportadora = new Transportadora("Transportadora", "04.238.377/0001-80", "(11) 2345-6789", "invalid");
        var resultadoValidacao = _validator.Validate(transportadora);
        resultadoValidacao.Errors.Should().Contain(failure => failure.PropertyName == nameof(transportadora.Email));
    }

    [Fact]
    public void Nao_Deve_Gerar_Erros_Quando_Dominio_For_Valido()
    {
        var transportadora = new Transportadora("Transportadora", "22.264.404/0001-25", "(11) 2345-6789", "empresa@empresa.com.br");
        var resultadoValidacao = _validator.Validate(transportadora);
        resultadoValidacao.IsValid.Should().BeTrue();
    }
}