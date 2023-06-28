using System.Net;
using Microsoft.AspNetCore.Mvc;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.API.Controllers;

[Route("api/unidades")]
public class UnidadesController : MainController
{
    private readonly IUnidadeService _unidadeService;

    public UnidadesController(IUnidadeService unidadeService, INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _unidadeService = unidadeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnidadeDTO>>> ObterTodasUnidades()
    {
        var unidades = await _unidadeService.ObterTodasUnidadesComCondominioAsync();
        return Ok(unidades);
    }

    [HttpGet("por-condominio/{condominioId:int}")]
    public async Task<ActionResult<IEnumerable<UnidadeDTO>>> ObterTodasUnidadesPorCondominio(int condominioId)
    {
        var unidades = await _unidadeService.ObterTodasUnidadesPorCondominioAsync(condominioId);
        return Ok(unidades);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UnidadeDTO>> ObterUnidadePorId(int id)
    {
        var unidade = await _unidadeService.ObterUnidadePorIdAsync(id);

        if (unidade == null) return NotFound(); 

        return Ok(unidade);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] UnidadeDTO unidadeDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _unidadeService.AdicionarAsync(unidadeDTO);

        if (!OperacaoValida()) return CustomResponse();

        return CreatedAtAction(nameof(ObterUnidadePorId), new { unidadeDTO.Id }, unidadeDTO);
    }

    [HttpPost("adicionar-em-massa")]
    public async Task<ActionResult> AdicionarUnidadesEmMassa(UnidadesEmMassaDTO unidadesEmMassaDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _unidadeService.AdicionarUnidadesEmMassaAsync(unidadesEmMassaDTO);

        if (!OperacaoValida()) return CustomResponse();

        return CreatedAtAction(nameof(ObterTodasUnidadesPorCondominio), new { condominioId = unidadesEmMassaDTO.CondominioId }, unidadesEmMassaDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<UnidadeDTO>> Atualizar(int id, UnidadeDTO unidadeDTO)
    {
        var unidade = await _unidadeService.ObterUnidadePorIdAsync(id);

        if (unidade == null) return NotFound();

        if (id != unidadeDTO.Id)
        {
            NotificarErro("Erro ao atualizar a unidade, id informado não é o mesmo que foi passado na query");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _unidadeService.AtualizarAsync(unidadeDTO);

        if (!OperacaoValida()) return CustomResponse();

        return Ok(unidadeDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remover(int id)
    {
        var unidade = await _unidadeService.ObterUnidadePorIdAsync(id);

        if (unidade == null) return NotFound();

        await _unidadeService.RemoverAsync(id);

        if (!OperacaoValida()) return CustomResponse();

        return NoContent();
    }
}