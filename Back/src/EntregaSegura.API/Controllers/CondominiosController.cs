using System.Net;
using Microsoft.AspNetCore.Mvc;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;

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
        return CustomResponse(condominios, HttpStatusCode.OK);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CondominioDTO>> ObterCondominioPorId(int id)
    {
        var condominio = await _condominioService.ObterCondominioPorIdAsync(id);

        if (condominio == null)
        {
            NotificarErro("Condomínio não encontrado");
            return CustomResponse(null, HttpStatusCode.NotFound);
        }

        return CustomResponse(condominio, HttpStatusCode.OK);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] CondominioDTO condominioDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _condominioService.AdicionarAsync(condominioDTO);

        if (!OperacaoValida()) return CustomResponse(null, HttpStatusCode.BadRequest);

        return CustomResponse(condominioDTO, HttpStatusCode.Created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CondominioDTO>> Atualizar(int id, CondominioDTO condominioDTO)
    {
        var condominio = await _condominioService.ObterCondominioPorIdAsync(id);

        if (condominio == null)
        {
            NotificarErro("Condomínio não encontrado");
            return CustomResponse(null, HttpStatusCode.NotFound);
        }

        if (id != condominioDTO.Id)
        {
            NotificarErro("Erro ao atualizar condomínio: Id da requisição difere do Id do objeto");
            return CustomResponse(null, HttpStatusCode.BadRequest);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _condominioService.AtualizarAsync(condominioDTO);

        if (!OperacaoValida()) return CustomResponse(null, HttpStatusCode.BadRequest);

        return CustomResponse(condominioDTO, HttpStatusCode.OK);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remover(int id)
    {
        var condominioDTO = await _condominioService.ObterCondominioPorIdAsync(id);

        if (condominioDTO == null)
        {
            NotificarErro("Condomínio não encontrado");
            return CustomResponse(null, HttpStatusCode.NotFound);
        }

        await _condominioService.RemoverAsync(id);

        if (!OperacaoValida()) return CustomResponse(null, HttpStatusCode.BadRequest);

        return CustomResponse(null, HttpStatusCode.OK);
    }
}