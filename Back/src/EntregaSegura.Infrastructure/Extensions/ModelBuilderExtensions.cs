using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        var condominio1 = new Condominio
        {
            Nome = "Condomínio Boa Vista",
            CNPJ = "17540623000150",
            Telefone = "1140028922",
            Email = "contato@boavista.com.br",
            QuantidadeBlocos = 5,
            QuantidadeUnidades = 4,
            QuantidadeAndares = 10,
            Logradouro = "Rua das Acácias",
            Numero = "55",
            CEP = "04567010",
            Bairro = "Jardim Paulistano",
            Cidade = "São Paulo",
            Estado = "SP"
        };

        var condominio2 = new Condominio
        {
            Nome = "Condomínio Raio de Sol",
            CNPJ = "27004428000169",
            Telefone = "2130033211",
            Email = "contato@raiodesol.com.br",
            QuantidadeBlocos = 5,
            QuantidadeUnidades = 8,
            QuantidadeAndares = 10,
            Logradouro = "Avenida Atlântica",
            Numero = "700",
            CEP = "22021001",
            Bairro = "Copacabana",
            Cidade = "Rio de Janeiro",
            Estado = "RJ"
        };
        
        condominio1.DefinirId(1);
        condominio2.DefinirId(2);

        modelBuilder.Entity<Condominio>().HasData(condominio1, condominio2);

        var unidade1 = new Unidade
        {
            CondominioId = 1,
            Numero = "101",
            Bloco = "A"
        };

        var unidade2 = new Unidade
        {
            CondominioId = 1,
            Numero = "102",
            Bloco = "A"
        };

        unidade1.DefinirId(1);
        unidade2.DefinirId(2);

        modelBuilder.Entity<Unidade>().HasData(unidade1, unidade2);

        var morador1 = new Morador
        {
            UnidadeId = 1,
            Nome = "Morador Teste 1",
            CPF = "12345678901",
            Email = "morador1@teste.com",
            Telefone = "1234567890",
            Ramal = "123",
            Foto = "foto1.jpg"
        };

        var morador2 = new Morador
        {
            UnidadeId = 2,
            Nome = "Morador Teste 2",
            CPF = "12345678902",
            Email = "morador2@teste.com",
            Telefone = "1234567891",
            Ramal = "456",
            Foto = "foto2.jpg"
        };

        morador1.DefinirId(1);
        morador2.DefinirId(2);

        modelBuilder.Entity<Morador>().HasData(morador1, morador2);

        var funcionario1 = new Funcionario
        {
            CondominioId = 1,
            Nome = "Funcionario Teste 1",
            CPF = "12345678903",
            Email = "funcionario1@teste.com",
            Telefone = "1234567892",
            Cargo = CargoFuncionario.Zelador
        };

        var funcionario2 = new Funcionario
        {
            CondominioId = 2,
            Nome = "Funcionario Teste 2",
            CPF = "12345678904",
            Email = "funcionario2@teste.com",
            Telefone = "1234567893",
            Cargo = CargoFuncionario.Porteiro
        };

        funcionario1.DefinirId(1);
        funcionario2.DefinirId(2);

        modelBuilder.Entity<Funcionario>().HasData(funcionario1, funcionario2);

        var transportadora1 = new Transportadora
        {
            Nome = "Transportadora Teste 1",
            CNPJ = "12345678912347",
            Telefone = "1234567894",
            Email = "transportadora1@teste.com"
        };

        var transportadora2 = new Transportadora
        {
            Nome = "Transportadora Teste 2",
            CNPJ = "12345678912348",
            Telefone = "1234567895",
            Email = "transportadora2@teste.com"
        };

        transportadora1.DefinirId(1);
        transportadora2.DefinirId(2);

        modelBuilder.Entity<Transportadora>().HasData(transportadora1, transportadora2);

        var entrega1 = new Entrega
        {
            TransportadoraId = 1,
            MoradorId = 1,
            FuncionarioId = 1,
            Descricao = "Entrega Teste 1",
            Observacao = "Observação Teste 1",
            Status = StatusEntrega.Recebida,
        };

        var entrega2 = new Entrega
        {
            TransportadoraId = 2,
            MoradorId = 2,
            FuncionarioId = 2,
            Descricao = "Entrega Teste 2",
            Observacao = "Observação Teste 2",
            Status = StatusEntrega.Recebida,
        };

        entrega1.DefinirId(1);
        entrega2.DefinirId(2);

        modelBuilder.Entity<Entrega>().HasData(entrega1, entrega2);
    }
}
