using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntregaSegura.API.Controllers;

[Route("api/controminios")]
public class CondominiosController : MainController
{
    private readonly ICondominioService _condominioService;
    private readonly IMapper _mapper;

    public CondominiosController(ICondominioService condominioService,
                                 IMapper mapper)
    {
        _condominioService = condominioService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CondominioDTO>>> ObterTodos()
    {
        var condominiosDTO = _mapper.Map<IEnumerable<CondominioDTO>>(await _condominioService.ObterTodosAsync());
        return Ok(condominiosDTO);
    }
}