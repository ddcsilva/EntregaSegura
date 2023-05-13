using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntregaSegura.API.Controllers;

[Route("api/condominios")]
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CondominioDTO>> ObterPorId(Guid id)
    {
        var condominioDTO = await ObterCondominioComUnidadesEFuncionarios(id);

        if(condominioDTO == null) return NotFound();

        return Ok(condominioDTO);
    }

    [HttpPost]
    public async Task<ActionResult<CondominioDTO>> Adicionar(CondominioDTO condominioDTO)
    {
        if(!ModelState.IsValid) return BadRequest();

        var condominio = _mapper.Map<Condominio>(condominioDTO);
        var condominioAdicionado = await _condominioService.Adicionar(condominio);

        if(!condominioAdicionado) return BadRequest();

        return Ok(condominioDTO);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CondominioDTO>> Atualizar(Guid id, CondominioDTO condominioDTO)
    {
        if(id != condominioDTO.Id) return BadRequest();

        if(!ModelState.IsValid) return BadRequest();

        var condominio = _mapper.Map<Condominio>(condominioDTO);
        var condominioAtualizado = await _condominioService.Atualizar(condominio);

        if(!condominioAtualizado) return BadRequest();

        return Ok(condominioDTO);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<CondominioDTO>> Remover(Guid id)
    {
        var condominioDTO = await ObterCondominio(id);

        if(condominioDTO == null) return NotFound();

        await _condominioService.Remover(id);

        return Ok(condominioDTO);
    }

    private async Task<CondominioDTO> ObterCondominioComUnidadesEFuncionarios(Guid condominioId)
    {
        return _mapper.Map<CondominioDTO>(await _condominioService.ObterCondominioComUnidadesEFuncionariosAsync(condominioId));
    }

    private async Task<CondominioDTO> ObterCondominio(Guid condominioId)
    {
        return _mapper.Map<CondominioDTO>(await _condominioService.ObterPorIdAsync(condominioId));
    }
}