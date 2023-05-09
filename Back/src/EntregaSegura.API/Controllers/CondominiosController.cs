using EntregaSegura.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntregaSegura.API.Controllers;

[Route("api/condominios")]
[ApiController]
public class CondominiosController
{
    public IEnumerable<Condominio> condominios = new Condominio[] {
        new Condominio() {
            Nome = "Condomínio 1",
            CNPJ = "12345678901234",
            Telefone = "1234567890",
            Email = "contato@condominio.com.br",
            Logradouro = "Rua 1",
            Numero = "123",
            Complemento = "Apto 1",
            CEP = "12345678",
            Bairro = "Bairro 1",
            Cidade = "Cidade 1",
            Estado = "Estado 1"
        },
        new Condominio() {
            Nome = "Condomínio 2",
            CNPJ = "12345678901234",
            Telefone = "1234567890",
            Email = "contato@condominio2.com.br",
            Logradouro = "Rua 2",
            Numero = "123",
            Complemento = "Apto 2",
            CEP = "12345678",
            Bairro = "Bairro 2",
            Cidade = "Cidade 2",
            Estado = "Estado 2"
        }
    };

    [HttpGet]
    public IEnumerable<Condominio> Get()
    {
        return condominios;
    }

    [HttpGet("{id}")]
    public IEnumerable<Condominio> GetById(Guid id)
    {
        return condominios.Where(c => c.Id == id);
    }

    [HttpPost]
    public void Post([FromBody] Condominio condominio)
    {
        condominios.Append(condominio);
    }
}
