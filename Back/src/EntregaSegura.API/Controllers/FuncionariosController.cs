using System.Net;
using Microsoft.AspNetCore.Mvc;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.API.Controllers;

[Route("api/funcionarios")]
public class FuncionariosController : MainController
{
    private readonly IFuncionarioService _funcionarioService;

    public FuncionariosController(IFuncionarioService funcionarioService,
                                  INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _funcionarioService = funcionarioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FuncionarioDTO>>> ObterTodosFuncionarios()
    {
        var funcionarios = await _funcionarioService.ObterTodosFuncionariosAsync();
        return CustomResponse(funcionarios, HttpStatusCode.OK);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FuncionarioDTO>> ObterFuncionarioPorId(int id)
    {
        var funcionario = await _funcionarioService.ObterFuncionarioPorIdAsync(id);

        if (funcionario == null)
        {
            NotificarErro("Funcionário não encontrado");
            return CustomResponse(null, HttpStatusCode.NotFound);
        }

        return CustomResponse(funcionario, HttpStatusCode.OK);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] FuncionarioDTO funcionarioDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _funcionarioService.AdicionarAsync(funcionarioDTO);

        if (!OperacaoValida()) return CustomResponse(null, HttpStatusCode.BadRequest);

        return CustomResponse(funcionarioDTO, HttpStatusCode.Created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<FuncionarioDTO>> Atualizar(int id, FuncionarioDTO funcionarioDTO)
    {
        var funcionario = await _funcionarioService.ObterFuncionarioPorIdAsync(id);

        if (funcionario == null)
        {
            NotificarErro("Funcionário não encontrado");
            return CustomResponse(null, HttpStatusCode.NotFound);
        }

        if (id != funcionarioDTO.Id)
        {
            NotificarErro("Erro ao atualizar funcionário: Id da requisição difere do Id do objeto");
            return CustomResponse(null, HttpStatusCode.BadRequest);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _funcionarioService.AtualizarAsync(funcionarioDTO);

        if (!OperacaoValida()) return CustomResponse(null, HttpStatusCode.BadRequest);

        return CustomResponse(funcionarioDTO, HttpStatusCode.OK);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remover(int id)
    {
        var funcionarioDTO = await _funcionarioService.ObterFuncionarioPorIdAsync(id);

        if (funcionarioDTO == null)
        {
            NotificarErro("Funcionário não encontrado");
            return CustomResponse(null, HttpStatusCode.NotFound);
        }

        await _funcionarioService.RemoverAsync(id);

        if (!OperacaoValida()) return CustomResponse(null, HttpStatusCode.BadRequest);

        return CustomResponse(funcionarioDTO, HttpStatusCode.OK);
    }

    private bool UploadArquivo(string arquivo, string nomeImagem)
    {
        if (string.IsNullOrEmpty(arquivo))
        {
            NotificarErro("Forneça uma imagem para este funcionário!");
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