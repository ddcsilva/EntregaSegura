using EntregaSegura.API.Contexts;
using EntregaSegura.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntregaSegura.API.Controllers;

[Route("api/condominios")]
[ApiController]
public class CondominiosController
{
    private readonly EntregaSeguraContext _context;

    public CondominiosController(EntregaSeguraContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Condominio> Get()
    {
        return _context.Condominios;
    }

    [HttpGet("{id}")]
    public Condominio GetById(Guid id)
    {
        return _context.Condominios.FirstOrDefault(x => x.Id == id);
    }
}
