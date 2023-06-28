using Microsoft.AspNetCore.Mvc;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.API.Controllers;

[Route("api/moradores")]
public class MoradoresController : MainController
{
    private readonly IMoradorService _moradorService;

    public MoradoresController(IMoradorService moradorService,
                               INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _moradorService = moradorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MoradorDTO>>> ObterTodosMoradores()
    {
        var moradores = await _moradorService.ObterTodosMoradoresComUnidadeECondominioAsync();
        return Ok(moradores);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MoradorDTO>> ObterMoradorPorId(int id)
    {
        var morador = await _moradorService.ObterMoradorPorIdAsync(id);

        if (morador == null) return NotFound();

        return Ok(morador);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] MoradorDTO moradorDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _moradorService.AdicionarAsync(moradorDTO);

        if (!OperacaoValida()) return CustomResponse();

        return CreatedAtAction(nameof(ObterMoradorPorId), new { moradorDTO.Id }, moradorDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MoradorDTO>> Atualizar(int id, MoradorDTO moradorDTO)
    {
        var morador = await _moradorService.ObterMoradorPorIdAsync(id);

        if (morador == null) return NotFound();

        if (id != moradorDTO.Id)
        {
            NotificarErro("Erro ao atualizar morador: Id da requisição difere do Id do objeto");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _moradorService.AtualizarAsync(moradorDTO);

        if (!OperacaoValida()) return CustomResponse();

        return Ok(moradorDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remover(int id)
    {
        var moradorDTO = await _moradorService.ObterMoradorPorIdAsync(id);

        if (moradorDTO == null) return NotFound();

        await _moradorService.RemoverAsync(id);

        if (!OperacaoValida()) return CustomResponse();

        return NoContent();
    }
}