using System.Net;
using Microsoft.AspNetCore.Mvc;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.API.Controllers;

[Route("api/transportadoras")]
public class TransportadorasController : MainController
{
    private readonly ITransportadoraService _transportadoraService;

    public TransportadorasController(ITransportadoraService transportadoraService, INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _transportadoraService = transportadoraService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransportadoraDTO>>> ObterTodasTransportadoras()
    {
        var transportadoras = await _transportadoraService.ObterTodasTransportadorasAsync();
        return Ok(transportadoras);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TransportadoraDTO>> ObterTransportadoraPorId(int id)
    {
        var transportadora = await _transportadoraService.ObterTransportadoraPorIdAsync(id);

        if (transportadora == null) return NotFound();

        return Ok(transportadora);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] TransportadoraDTO transportadoraDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _transportadoraService.AdicionarAsync(transportadoraDTO);

        if (!OperacaoValida()) return CustomResponse();

        return Ok(transportadoraDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TransportadoraDTO>> Atualizar(int id, TransportadoraDTO transportadoraDTO)
    {
        var transportadora = await _transportadoraService.ObterTransportadoraPorIdAsync(id);

        if (transportadora == null) return NotFound();

        if (id != transportadoraDTO.Id)
        {
            NotificarErro("Erro ao atualizar a transportadora: Id da requisição difere do Id do objeto");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _transportadoraService.AtualizarAsync(transportadoraDTO);

        if (!OperacaoValida()) return CustomResponse();

        return Ok(transportadoraDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remover(int id)
    {
        var transportadoraDTO = await _transportadoraService.ObterTransportadoraPorIdAsync(id);

        if (transportadoraDTO == null) return NotFound();

        await _transportadoraService.RemoverAsync(id);

        if (!OperacaoValida()) return CustomResponse();

        return NoContent();
    }
}