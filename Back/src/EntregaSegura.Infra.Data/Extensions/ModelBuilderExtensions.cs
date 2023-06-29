using Microsoft.EntityFrameworkCore;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace EntregaSegura.Infra.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        PopularCondominios(modelBuilder);
        PopularUnidades(modelBuilder);
        PopularMoradores(modelBuilder);
        PopularFuncionarios(modelBuilder);
        PopularTransportadoras(modelBuilder);
        PopularEntregas(modelBuilder);
    }

    private static void PopularCondominios(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Condominio>().HasData(new
        {
            Id = 1,
            Nome = "Condomínio Boa Vista",
            Cnpj = "55787865000131",
            Telefone = "1140028922",
            Email = "contato@boavista.com.br",
            QuantidadeBlocos = 2,
            QuantidadeAndares = 4,
            QuantidadeUnidades = 7,
            Logradouro = "Rua das Acácias",
            Numero = 55,
            Cep = "04567010",
            Bairro = "Jardim Paulistano",
            Cidade = "São Paulo",
            Estado = "SP"
        });
    }

    private static void PopularUnidades(ModelBuilder modelBuilder)
    {
        var quantidadeBlocos = 2;
        var quantidadeAndares = 4;
        var quantidadeUnidades = 7;

        var unidadeId = 1;

        for (int bloco = 1; bloco <= quantidadeBlocos; bloco++)
        {
            for (int andar = 1; andar <= quantidadeAndares; andar++)
            {
                for (int unidade = 1; unidade <= quantidadeUnidades; unidade++)
                {
                    modelBuilder.Entity<Unidade>().HasData(new
                    {
                        Id = unidadeId++,
                        CondominioId = 1,
                        Numero = unidade,
                        Andar = andar,
                        Bloco = bloco
                    });
                }
            }
        }
    }

    private static void PopularMoradores(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Morador>().HasData(new
        {
            Id = 1,
            UnidadeId = 1,
            Nome = "Morador Teste 1",
            Cpf = "12345678901",
            Email = "morador1@teste.com",
            Telefone = "1234567890",
            Ramal = "123",
            Foto = "foto1.jpg"
        });
    }

    private static void PopularFuncionarios(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funcionario>().HasData(new
        {
            Id = 1,
            CondominioId = 1,
            Nome = "Funcionário Teste 1",
            Cpf = "12345678903",
            Email = "funcionario1@teste.com",
            Telefone = "1234567892",
            Cargo = CargoFuncionario.Porteiro,
            DataAdmissao = DateTime.Now
        });
    }

    private static void PopularTransportadoras(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transportadora>().HasData(new
        {
            Id = 1,
            Nome = "Transportadora Teste 1",
            Cnpj = "12345678912347",
            Telefone = "1234567894",
            Email = "transportadora1@teste.com"
        });
    }

    private static void PopularEntregas(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entrega>().HasData(new
        {
            Id = 1,
            TransportadoraId = 1,
            MoradorId = 1,
            FuncionarioId = 1,
            DataRecebimento = DateTime.Now,
            Descricao = "Entrega Teste 1",
            Observacao = "Observação Teste 1",
            Status = StatusEntrega.AguardandoRetirada
        });
    }
}
