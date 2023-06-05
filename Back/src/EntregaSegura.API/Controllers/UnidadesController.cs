// using AutoMapper;
// using EntregaSegura.Application.DTOs.Unidades;
// using EntregaSegura.Application.Interfaces;
// using EntregaSegura.Domain.Entities;
// using Microsoft.AspNetCore.Mvc;

// namespace EntregaSegura.API.Controllers;

// [Route("api/unidades")]
// public class UnidadesController : MainController
// {
//     private readonly IUnidadeService _unidadeService;
//     private readonly IMapper _mapper;

//     public UnidadesController(IUnidadeService unidadeService,
//                               IMapper mapper,
//                               INotificadorErros notificadorErros) : base(notificadorErros)
//     {
//         _unidadeService = unidadeService;
//         _mapper = mapper;
//     }

//     [HttpGet]
//     public async Task<ActionResult<IEnumerable<UnidadeDTO>>> ObterTodos()
//     {
//         var unidadesDTO = await ObterUnidadesComCondominio();

//         return Ok(unidadesDTO);
//     }

//     [HttpGet("{id:int}")]
//     public async Task<ActionResult<UnidadeDTO>> ObterPorId(int id)
//     {
//         var unidadeDTO = await ObterUnidadePorIdComMoradores(id);

//         if (unidadeDTO == null) return NotFound();

//         return Ok(unidadeDTO);
//     }

//     [HttpPost]
//     public async Task<ActionResult<UnidadeDTO>> Adicionar(UnidadeDTO unidadeDTO)
//     {
//         if (!ModelState.IsValid) return CustomResponse(ModelState);

//         var unidade = _mapper.Map<Unidade>(unidadeDTO);
//         var novaUnidade = await _unidadeService.Adicionar(unidade);

//         if (novaUnidade == null) return CustomResponse(ModelState);

//         unidadeDTO = _mapper.Map<UnidadeDTO>(novaUnidade);

//         return CustomResponse(unidadeDTO);
//     }

//     [HttpPost("em-massa")]
//     public async Task<ActionResult> AdicionarUnidadesEmMassa(UnidadesEmMassaDTO unidadesDTO)
//     {
//         if (!ModelState.IsValid) return CustomResponse(ModelState);

//         var resultado = await _unidadeService.AdicionarUnidadesEmMassa(unidadesDTO);

//         if (!resultado) return CustomResponse(ModelState);

//         return CustomResponse();
//     }

//     [HttpPut("{id:int}")]
//     public async Task<ActionResult<UnidadeDTO>> Atualizar(int id, UnidadeDTO unidadeDTO)
//     {
//         if (id != unidadeDTO.Id)
//         {
//             NotificarErro("O id informado não é o mesmo que foi passado na query");
//             return CustomResponse(unidadeDTO);
//         }

//         if (!ModelState.IsValid) return CustomResponse(ModelState);

//         var unidade = _mapper.Map<Unidade>(unidadeDTO);
//         var unidadeAtualizada = await _unidadeService.Atualizar(unidade);

//         if (unidadeAtualizada == null) return CustomResponse(ModelState);

//         unidadeDTO = _mapper.Map<UnidadeDTO>(unidadeAtualizada);

//         return CustomResponse(unidadeDTO);
//     }

//     [HttpDelete("{id:int}")]
//     public async Task<ActionResult<UnidadeDTO>> Excluir(int id)
//     {
//         var unidadeDTO = await ObterUnidade(id);

//         if (unidadeDTO == null) return NotFound();

//         await _unidadeService.Remover(id);

//         return CustomResponse(unidadeDTO);
//     }

//     private async Task<IEnumerable<UnidadeDTO>> ObterUnidadesComCondominio()
//     {
//         return _mapper.Map<IEnumerable<UnidadeDTO>>(await _unidadeService.ObterUnidadesComCondominioAsync());
//     }

//     private async Task<UnidadeDTO> ObterUnidade(int id)
//     {
//         return _mapper.Map<UnidadeDTO>(await _unidadeService.ObterPorIdAsync(id));
//     }

//     private async Task<UnidadeDTO> ObterUnidadePorIdComMoradores(int id)
//     {
//         return _mapper.Map<UnidadeDTO>(await _unidadeService.ObterUnidadePorIdComCondominioEMoradoresAsync(id));
//     }
// }