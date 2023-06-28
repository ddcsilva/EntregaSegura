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
        return Ok(funcionarios);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FuncionarioDTO>> ObterFuncionarioPorId(int id)
    {
        var funcionario = await _funcionarioService.ObterFuncionarioPorIdAsync(id);

        if (funcionario == null) return NotFound();

        return Ok(funcionario);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] FuncionarioDTO funcionarioDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _funcionarioService.AdicionarAsync(funcionarioDTO);

        if (!OperacaoValida()) return CustomResponse();

        return CreatedAtAction(nameof(ObterFuncionarioPorId), new { funcionarioDTO.Id }, funcionarioDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<FuncionarioDTO>> Atualizar(int id, FuncionarioDTO funcionarioDTO)
    {
        var funcionario = await _funcionarioService.ObterFuncionarioPorIdAsync(id);

        if (funcionario == null) return NotFound();

        if (id != funcionarioDTO.Id)
        {
            NotificarErro("Erro ao atualizar funcionário: Id da requisição difere do Id do objeto");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _funcionarioService.AtualizarAsync(funcionarioDTO);

        if (!OperacaoValida()) return CustomResponse();

        return Ok(funcionarioDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remover(int id)
    {
        var funcionarioDTO = await _funcionarioService.ObterFuncionarioPorIdAsync(id);

        if (funcionarioDTO == null) return NotFound();

        await _funcionarioService.RemoverAsync(id);

        if (!OperacaoValida()) return CustomResponse();

        return NoContent();
    }
}