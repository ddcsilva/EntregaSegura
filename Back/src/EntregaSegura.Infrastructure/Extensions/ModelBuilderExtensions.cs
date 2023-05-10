using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        var condominioId = Guid.Parse("f26355b2-c097-4582-8a4a-4a9ecbfc7f09");
        var enderecoId = Guid.Parse("ebdebdf0-2c0c-4db5-aff9-f6e583c7fb5b");
        var unidadeId = Guid.Parse("68165d63-fa38-4d27-858f-ac006b1ada92");
        var moradorId = Guid.Parse("7b3b9132-0eae-4ba1-8519-347d92873868");
        var funcionarioId = Guid.Parse("f1e28b7e-674f-41dd-868c-c245e35de929");
        var transportadoraId = Guid.Parse("5cc12493-0012-43aa-aac0-76cbc18bedb3");
        var entregaId = Guid.Parse("8a6b4827-15d3-4d9c-a567-a14a6a0c8ce7");

        modelBuilder.Entity<Condominio>().HasData(new Condominio
        {
            Id = condominioId,
            Nome = "Condomínio Exemplo",
            CNPJ = "11111111111111",
            Telefone = "11999999999",
            Email = "contato@condominioexemplo.com.br",
            Logradouro = "Rua Exemplo",
            Numero = "100",
            Complemento = "Bloco A",
            CEP = "11111111",
            Bairro = "Bairro Exemplo",
            Cidade = "Cidade Exemplo",
            Estado = "SP"
        });

        modelBuilder.Entity<Unidade>().HasData(new Unidade
        {
            Id = unidadeId,
            CondominioId = condominioId,
            Numero = "101",
            Bloco = "A"
        });

        modelBuilder.Entity<Morador>().HasData(new Morador
        {
            Id = moradorId,
            Nome = "Morador Exemplo",
            CPF = "12345678901",
            Email = "morador@email.com",
            Telefone = "11999999999",
            UnidadeId = unidadeId
        });

        modelBuilder.Entity<Funcionario>().HasData(new Funcionario
        {
            Id = funcionarioId,
            Nome = "Funcionario Exemplo",
            CPF = "98765432109",
            Email = "funcionario@email.com",
            Telefone = "11999999999",
            Cargo = CargoFuncionario.Porteiro,
            CondominioId = condominioId
        });

        modelBuilder.Entity<Transportadora>().HasData(new Transportadora
        {
            Id = transportadoraId,
            Nome = "Transportadora Exemplo",
            CNPJ = "22222222222222",
            Telefone = "11988888888",
            Email = "contato@transportadoraexemplo.com.br"
        });

        modelBuilder.Entity<Entrega>().HasData(new Entrega
        {
            Id = entregaId,
            DataRecebimento = DateTime.Now,
            Descricao = "Descrição da entrega",
            Observacao = "Observação da entrega",
            Status = StatusEntrega.Recebida,
            TransportadoraId = transportadoraId,
            MoradorId = moradorId,
            FuncionarioId = funcionarioId
        });
    }
}
