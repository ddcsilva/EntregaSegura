using System.Net;
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
        return CustomResponse(moradores, HttpStatusCode.OK);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MoradorDTO>> ObterMoradorPorId(int id)
    {
        var morador = await _moradorService.ObterMoradorPorIdAsync(id);

        if (morador == null)
        {
            NotificarErro("Morador não encontrado");
            return CustomResponse(null, HttpStatusCode.NotFound);
        }

        return CustomResponse(morador, HttpStatusCode.OK);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] MoradorDTO moradorDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _moradorService.AdicionarAsync(moradorDTO);

        if (!OperacaoValida()) return CustomResponse(null, HttpStatusCode.BadRequest);

        return CustomResponse(moradorDTO, HttpStatusCode.Created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MoradorDTO>> Atualizar(int id, MoradorDTO moradorDTO)
    {
        var morador = await _moradorService.ObterMoradorPorIdAsync(id);

        if (morador == null)
        {
            NotificarErro("Morador não encontrado");
            return CustomResponse(null, HttpStatusCode.NotFound);
        }

        if (id != moradorDTO.Id)
        {
            NotificarErro("Erro ao atualizar morador: Id da requisição difere do Id do objeto");
            return CustomResponse(null, HttpStatusCode.BadRequest);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _moradorService.AtualizarAsync(moradorDTO);

        if (!OperacaoValida()) return CustomResponse(null, HttpStatusCode.BadRequest);

        return CustomResponse(moradorDTO, HttpStatusCode.OK);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remover(int id)
    {
        var moradorDTO = await _moradorService.ObterMoradorPorIdAsync(id);

        if (moradorDTO == null)
        {
            NotificarErro("Morador não encontrado");
            return CustomResponse(null, HttpStatusCode.NotFound);
        }

        await _moradorService.RemoverAsync(id);

        if (!OperacaoValida()) return CustomResponse(null, HttpStatusCode.BadRequest);

        return CustomResponse(moradorDTO, HttpStatusCode.OK);
    }

    private bool UploadArquivo(string arquivo, string nomeImagem)
    {
        if (string.IsNullOrEmpty(arquivo))
        {
            NotificarErro("Forneça uma imagem para este morador!");
            return false;
        }

        var imageDataByteArray = Convert.FromBase64String(arquivo);

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", nomeImagem);

        if (System.IO.File.Exists(filePath))
        {
            NotificarErro("Já existe um arquivo com este nome!");
            return false;
        }

        System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

        return true;
    }
}