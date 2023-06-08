using Microsoft.EntityFrameworkCore;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Infra.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        var condominio1 = new Condominio
        (
            "Condomínio Boa Vista",
            "17540623000150",
            "1140028922",
            "contato@boavista.com.br",
            2,
            4,
            7,
            "Rua das Acácias",
            55,
            "04567010",
            "Jardim Paulistano",
            "São Paulo",
            "SP"
        );

        var condominio2 = new Condominio
        (
            "Condomínio Raio de Sol",
            "27004428000169",
            "2130033211",
            "contato@raiodesol.com.br",
            3,
            8,
            10,
            "Avenida Atlântica",
            700,
            "22021001",
            "Copacabana",
            "Rio de Janeiro",
            "RJ"
        );

        condominio1.DefinirId(1);
        condominio2.DefinirId(2);

        modelBuilder.Entity<Condominio>().HasData(condominio1, condominio2);

        var unidades = new List<Unidade>();

        for (int bloco = 1; bloco <= condominio1.QuantidadeBlocos; bloco++)
        {
            for (int andar = 1; andar <= condominio1.QuantidadeAndares; andar++)
            {
                for (int unidade = 1; unidade <= condominio1.QuantidadeUnidades; unidade++)
                {
                    unidades.Add(new Unidade
                    {
                        CondominioId = 1,
                        Numero = unidade,
                        Andar = andar,
                        Bloco = bloco.ToString()
                    });
                }
            }
        }

        for (int bloco = 1; bloco <= condominio2.QuantidadeBlocos; bloco++)
        {
            for (int andar = 1; andar <= condominio2.QuantidadeAndares; andar++)
            {
                for (int unidade = 1; unidade <= condominio2.QuantidadeUnidades; unidade++)
                {
                    unidades.Add(new Unidade
                    {
                        CondominioId = 2,
                        Numero = unidade,
                        Andar = andar,
                        Bloco = bloco.ToString()
                    });
                }
            }
        }

        // Define os IDs para as Unidades criadas
        for (int i = 0; i < unidades.Count; i++)
        {
            unidades[i].DefinirId(i + 1);
        }

        modelBuilder.Entity<Unidade>().HasData(unidades.ToArray());

        var morador1 = new Morador
        {
            UnidadeId = 1,
            Nome = "Morador Teste 1",
            Cpf = "12345678901",
            Email = "morador1@teste.com",
            Telefone = "1234567890",
            Ramal = "123",
            Foto = "foto1.jpg"
        };

        var morador2 = new Morador
        {
            UnidadeId = 2,
            Nome = "Morador Teste 2",
            Cpf = "12345678902",
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
        (
            "Transportadora Teste 1",
            "12345678912347",
            "1234567894",
            "transportadora1@teste.com"
        );

        var transportadora2 = new Transportadora
        (
            "Transportadora Teste 2",
            "12345678912348",
            "1234567895",
            "transportadora2@teste.com"
        );

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
