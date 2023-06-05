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

    public CondominiosController(ICondominioService condominioService,
                                 INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _condominioService = condominioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CondominioDTO>>> ObterTodosCondominios()
    {
        var condominios = await _condominioService.ObterTodosCondominiosAsync();

        if (condominios == null)
        {
            return NotFound();
        }

        return Ok(condominios);
    }

    [HttpGet("{id:int}", Name = "ObterCondominio")]
    public async Task<ActionResult<CondominioDTO>> ObterCondominioPorId(int id)
    {
        var condominio = await _condominioService.ObterCondominioPorIdAsync(id);

        if (condominio == null)
        {
            return NotFound();
        }

        return Ok(condominio);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] CondominioDTO condominioDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _condominioService.AdicionarAsync(condominioDTO);

        return new CreatedAtRouteResult("ObterCondominio", new { id = condominioDTO.Id }, condominioDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CondominioDTO>> Atualizar(int id, CondominioDTO condominioDTO)
    {
        if (id != condominioDTO.Id)
        {
            NotificarErro("Erro ao atualizar condomínio: Id da requisição difere do Id do objeto");
            return CustomResponse(condominioDTO);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _condominioService.AtualizarAsync(condominioDTO);

        return CustomResponse(condominioDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remover(int id)
    {
        var condominioDTO = await _condominioService.ObterCondominioPorIdAsync(id);

        if (condominioDTO == null) return NotFound("Condomínio não encontrado.");

        await _condominioService.RemoverAsync(id);

        return CustomResponse(condominioDTO);
    }
}