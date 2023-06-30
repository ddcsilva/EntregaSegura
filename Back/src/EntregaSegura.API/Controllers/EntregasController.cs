using Microsoft.AspNetCore.Mvc;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Application.DTOs;

namespace EntregaSegura.API.Controllers;

[Route("api/entregas")]
public class EntregasController : MainController
{
    private readonly IEntregaService _entregaService;

    public EntregasController(IEntregaService entregaService, INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _entregaService = entregaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EntregaDTO>>> ObterTodasEntregas()
    {
        var entregas = await _entregaService.ObterTodasEntregasAsync();
        return Ok(entregas);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EntregaDTO>> ObterEntregaPorId(int id)
    {
        var entrega = await _entregaService.ObterEntregaPorIdAsync(id);

        if (entrega == null) return NotFound();

        return Ok(entrega);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] EntregaDTO entregaDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _entregaService.AdicionarAsync(entregaDTO);

        if (!OperacaoValida()) return CustomResponse();

        return CreatedAtAction(nameof(ObterEntregaPorId), new { entregaDTO.Id }, entregaDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<EntregaDTO>> Atualizar(int id, EntregaDTO entregaDTO)
    {
        var entrega = await _entregaService.ObterEntregaPorIdAsync(id);

        if (entrega == null) return NotFound();

        if (id != entregaDTO.Id)
        {
            NotificarErro("Erro ao atualizar a entrega: Id da requisição difere do Id do objeto");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _entregaService.AtualizarAsync(entregaDTO);

        if (!OperacaoValida()) return CustomResponse();

        return Ok(entregaDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remover(int id)
    {
        var entregaDTO = await _entregaService.ObterEntregaPorIdAsync(id);

        if (entregaDTO == null) return NotFound();

        await _entregaService.RemoverAsync(id);

        if (!OperacaoValida()) return CustomResponse();

        return NoContent();
    }
}