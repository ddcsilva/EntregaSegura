using Microsoft.AspNetCore.Mvc;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.API.Controllers;

[Route("api/condominios")]
public class CondominiosController : MainController
{
    private readonly ICondominioService _condominioService;

    public CondominiosController(ICondominioService condominioService, INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _condominioService = condominioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CondominioDTO>>> ObterTodosCondominios()
    {
        var condominios = await _condominioService.ObterTodosCondominiosAsync();
        return Ok(condominios);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CondominioDTO>> ObterCondominioPorId(int id)
    {
        var condominio = await _condominioService.ObterCondominioPorIdAsync(id);

        if (condominio == null) return NotFound();

        return Ok(condominio);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] CondominioDTO condominioDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _condominioService.AdicionarAsync(condominioDTO);

        if (!OperacaoValida()) return CustomResponse();

        return CreatedAtAction(nameof(ObterCondominioPorId), new { condominioDTO.Id }, condominioDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CondominioDTO>> Atualizar(int id, CondominioDTO condominioDTO)
    {
        var condominio = await _condominioService.ObterCondominioPorIdAsync(id);

        if (condominio == null) return NotFound();

        if (id != condominioDTO.Id)
        {
            NotificarErro("Erro ao atualizar condomínio: Id da requisição difere do Id do objeto");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _condominioService.AtualizarAsync(condominioDTO);

        if (!OperacaoValida()) return CustomResponse();

        return Ok(condominioDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remover(int id)
    {
        var condominioDTO = await _condominioService.ObterCondominioPorIdAsync(id);

        if (condominioDTO == null) return NotFound();

        await _condominioService.RemoverAsync(id);

        if (!OperacaoValida()) return CustomResponse();

        return NoContent();
    }
}