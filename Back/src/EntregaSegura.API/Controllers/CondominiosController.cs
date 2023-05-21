using AutoMapper;
using EntregaSegura.Application.DTOs.Condominios;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntregaSegura.API.Controllers;

[Route("api/condominios")]
public class CondominiosController : MainController
{
    private readonly ICondominioService _condominioService;
    private readonly IMapper _mapper;

    public CondominiosController(ICondominioService condominioService,
                                 IMapper mapper,
                                 INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _condominioService = condominioService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<CondominioDTO>> Adicionar(CondominioDTO condominioDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var condominio = _mapper.Map<Condominio>(condominioDTO);
        var novoCondominio = await _condominioService.Adicionar(condominio);

        if (novoCondominio == null) return CustomResponse(ModelState);

        condominioDTO = _mapper.Map<CondominioDTO>(novoCondominio);

        return CustomResponse(condominioDTO);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CondominioDTO>>> ObterTodos()
    {
        var condominiosDTO = await ObterCondominios();

        return Ok(condominiosDTO);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CondominioDTO>> ObterPorId(Guid id)
    {
        var condominioDTO = await ObterCondominioComUnidadesEFuncionarios(id);

        return condominioDTO == null ? NotFound() : Ok(condominioDTO);
    }

    [HttpGet("por-nome/{nome}")]
    public async Task<ActionResult<IEnumerable<CondominioDTO>>> ObterPorNome(string nome)
    {
        var condominiosDTO = await ObterCondominiosPorNome(nome);

        return Ok(condominiosDTO);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CondominioDTO>> Atualizar(Guid id, CondominioDTO condominioDTO)
    {
        if (id != condominioDTO.Id)
        {
            NotificarErro("Erro ao atualizar condomínio: Id da requisição difere do Id do objeto");
            return CustomResponse(condominioDTO);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var condominio = _mapper.Map<Condominio>(condominioDTO);
        var condominioAtualizado = await _condominioService.Atualizar(condominio);

        if (condominioAtualizado == null) return CustomResponse(ModelState);

        condominioDTO = _mapper.Map<CondominioDTO>(condominioAtualizado);

        return CustomResponse(condominioDTO);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Remover(Guid id)
    {
        var condominioDTO = await ObterCondominio(id);

        if (condominioDTO == null) return NotFound();

        var remocaoBemSucedida = await _condominioService.Remover(id);

        if (!remocaoBemSucedida)
        {
            return CustomResponse();
        }

        return CustomResponse(condominioDTO);
    }

    private async Task<IEnumerable<CondominioDTO>> ObterCondominios()
    {
        return _mapper.Map<IEnumerable<CondominioDTO>>(await _condominioService.ObterTodosAsync());
    }

    private async Task<CondominioDTO> ObterCondominio(Guid condominioId)
    {
        return _mapper.Map<CondominioDTO>(await _condominioService.ObterPorIdAsync(condominioId));
    }

    private async Task<CondominioDTO> ObterCondominioComUnidadesEFuncionarios(Guid condominioId)
    {
        return _mapper.Map<CondominioDTO>(await _condominioService.ObterCondominioComUnidadesEFuncionariosAsync(condominioId));
    }

    private async Task<IEnumerable<CondominioDTO>> ObterCondominiosPorNome(string nome)
    {
        return _mapper.Map<IEnumerable<CondominioDTO>>(await _condominioService.ObterTodosCondominiosPorNomeAsync(nome));
    }
}