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
    private readonly IImagemService _imagemService;

    public MoradoresController(IMoradorService moradorService,
                               IImagemService imagemService,
                               INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _moradorService = moradorService;
        _imagemService = imagemService;
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

        string extensao = Path.GetExtension(moradorDTO.Foto);
        var rand = new Random();
        var nomeFoto = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + "_" + rand.Next(1000, 9999).ToString() + extensao;

        if (await _imagemService.SalvarImagemAsync(moradorDTO.FotoUpload, nomeFoto))
        {
            moradorDTO.Foto = nomeFoto;
        }

        await _moradorService.AdicionarAsync(moradorDTO);

        if (!OperacaoValida()) return CustomResponse(null, HttpStatusCode.BadRequest);

        return CustomResponse(moradorDTO, HttpStatusCode.Created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MoradorDTO>> Atualizar(int id, MoradorDTO moradorDTO)
    {
        var morador = await _moradorService.ObterMoradorPorIdAsync(id);
        string extensao = Path.GetExtension(moradorDTO.Foto);
        var rand = new Random();
        var nomeFoto = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + "_" + rand.Next(1000, 9999).ToString() + extensao;

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

        if (await _imagemService.SalvarImagemAsync(moradorDTO.FotoUpload, nomeFoto))
        {
            moradorDTO.Foto = nomeFoto;
        }

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
        if (string.IsNullOrEmpty(arquivo)) return false;

        var imageDataByteArray = Convert.FromBase64String(arquivo);

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", nomeImagem);

        System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

        return true;
    }
}