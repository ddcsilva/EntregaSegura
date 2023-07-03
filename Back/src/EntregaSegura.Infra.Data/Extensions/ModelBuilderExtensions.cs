using Microsoft.EntityFrameworkCore;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Infra.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        PopularCondominios(modelBuilder);
        PopularUnidades(modelBuilder);
        PopularPessoas(modelBuilder);
        PopularMoradores(modelBuilder);
        PopularFuncionarios(modelBuilder);
        PopularUsuarios(modelBuilder);
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

    private static void PopularPessoas(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>().HasData(new
        {
            Id = 1,
            Nome = "João da Silva",
            Cpf = "05245246023",
            Email = "joao.silva@email.com",
            Telefone = "1234567890"
        });

        modelBuilder.Entity<Pessoa>().HasData(new
        {
            Id = 2,
            Nome = "Maria da Silva",
            Cpf = "16522195011",
            Email = "maria.silva@email.com",
            Telefone = "1234567891"
        });

        modelBuilder.Entity<Pessoa>().HasData(new
        {
            Id = 3,
            Nome = "José da Silva",
            Cpf = "38813954077",
            Email = "jose.silva@email.com",
            Telefone = "1234567892"
        });
    }

    private static void PopularMoradores(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Morador>().HasData(new
        {
            Id = 1,
            Ramal = 123,
            UnidadeId = 1,
            PessoaId = 1
        });
    }

    private static void PopularFuncionarios(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funcionario>().HasData(new
        {
            Id = 1,
            DataAdmissao = DateTime.Now,
            CondominioId = 1,
            PessoaId = 2
        });

        modelBuilder.Entity<Funcionario>().HasData(new
        {
            Id = 2,
            DataAdmissao = DateTime.Now,
            CondominioId = 1,
            PessoaId = 3
        });
    }

    private static void PopularUsuarios(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().HasData(new
        {
            Id = 1,
            Login = "joao.silva@email.com",
            Senha = "b8gf2lXaUxY/ZK9dbzVkUb5Lgg5T4jMdow+QosWpBI4kX8Lj",
            Token = "",
            Foto = "foto1.jpg",
            Perfil = PerfilUsuario.Morador,
            PessoaId = 1
        });

        modelBuilder.Entity<Usuario>().HasData(new
        {
            Id = 2,
            Login = "maria.silva@email.com",
            Senha = "b8gf2lXaUxY/ZK9dbzVkUb5Lgg5T4jMdow+QosWpBI4kX8Lj",
            Token = "",
            Foto = "foto2.jpg",
            Perfil = PerfilUsuario.Funcionario,
            PessoaId = 2
        });

        modelBuilder.Entity<Usuario>().HasData(new
        {
            Id = 3,
            Login = "jose.silva@email.com",
            Senha = "b8gf2lXaUxY/ZK9dbzVkUb5Lgg5T4jMdow+QosWpBI4kX8Lj",
            Token = "",
            Foto = "foto3.jpg",
            Perfil = PerfilUsuario.Sindico,
            PessoaId = 3
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
            Status = StatusEntrega.Recebida
        });
    }
}
