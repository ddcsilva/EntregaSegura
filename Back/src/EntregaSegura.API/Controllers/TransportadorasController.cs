using AutoMapper;
using EntregaSegura.Application.DTOs.Transportadoras;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntregaSegura.API.Controllers;

[Route("api/transportadoras")]
public class TransportadorasController : MainController
{
    private readonly ITransportadoraService _transportadoraService;
    private readonly IMapper _mapper;

    public TransportadorasController(
        ITransportadoraService transportadoraService,
        IMapper mapper,
        INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _transportadoraService = transportadoraService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<TransportadoraDTO>> Adicionar(TransportadoraDTO transportadoraDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var transportadora = _mapper.Map<Transportadora>(transportadoraDTO);
        var novaTransportadora = await _transportadoraService.Adicionar(transportadora);

        if (novaTransportadora == null) return CustomResponse(ModelState);

        transportadoraDTO = _mapper.Map<TransportadoraDTO>(novaTransportadora);

        return CustomResponse(transportadoraDTO);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransportadoraDTO>>> ObterTodos()
    {
        var transportadorasDTO = await ObterTransportadoras();

        return Ok(transportadorasDTO);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TransportadoraDTO>> ObterPorId(int id)
    {
        var transportadoraDTO = await ObterTransportadora(id);

        return transportadoraDTO == null ? NotFound() : Ok(transportadoraDTO);
    }

    [HttpGet("por-nome/{nome}")]
    public async Task<ActionResult<IEnumerable<TransportadoraDTO>>> ObterPorNome(string nome)
    {
        var transportadorasDTO = await ObterTransportadorasPeloNome(nome);

        return Ok(transportadorasDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TransportadoraDTO>> Atualizar(int id, TransportadoraDTO transportadoraDTO)
    {
        if (id != transportadoraDTO.Id)
        {
            NotificarErro("Erro ao atualizar a transportadora: Id da requisição difere do Id do objeto");
            return CustomResponse(transportadoraDTO);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var transportadora = _mapper.Map<Transportadora>(transportadoraDTO);
        var transportadoraAtualizada = await _transportadoraService.Atualizar(transportadora);

        if (transportadoraAtualizada == null) return CustomResponse(ModelState);

        transportadoraDTO = _mapper.Map<TransportadoraDTO>(transportadoraAtualizada);

        return CustomResponse(transportadoraDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remover(int id)
    {
        var transportadoraDTO = await ObterTransportadora(id);

        if (transportadoraDTO == null) return NotFound();

        var remocaoBemSucedida = await _transportadoraService.Remover(id);

        if (!remocaoBemSucedida)
        {
            return CustomResponse();
        }

        return CustomResponse(transportadoraDTO);
    }

    private async Task<IEnumerable<TransportadoraDTO>> ObterTransportadoras()
    {
        return _mapper.Map<IEnumerable<TransportadoraDTO>>(await _transportadoraService.ObterTodosAsync());
    }

    private async Task<TransportadoraDTO> ObterTransportadora(int transportadoraId)
    {
        return _mapper.Map<TransportadoraDTO>(await _transportadoraService.ObterPorIdAsync(transportadoraId));
    }

    private async Task<IEnumerable<TransportadoraDTO>> ObterTransportadorasPeloNome(string nome)
    {
        return _mapper.Map<IEnumerable<TransportadoraDTO>>(await _transportadoraService.ObterTodasTransportadorasPeloNomeAsync(nome));
    }
}