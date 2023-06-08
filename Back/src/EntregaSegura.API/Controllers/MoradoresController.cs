// using EntregaSegura.Application.DTOs;
// using EntregaSegura.Application.Interfaces;
// using EntregaSegura.Domain.Entities;
// using Microsoft.AspNetCore.Mvc;

// namespace EntregaSegura.API.Controllers;

// [Route("api/moradores")]
// public class MoradoresController : MainController
// {
//     private readonly IMoradorService _moradorService;

//     public MoradoresController(IMoradorService moradorService,
//                                INotificadorErros notificadorErros) : base(notificadorErros)
//     {
//         _moradorService = moradorService;
//     }

//     [HttpGet]
//     public async Task<ActionResult<IEnumerable<MoradorDTO>>> ObterTodosMoradores()
//     {
//         var moradores = await _moradorService.ObterTodosMoradoresAsync();

//         return CustomResponse(moradores, HttpStatusCode.OK);
//     }

//     [HttpPost]
//     public async Task<ActionResult<MoradorDTO>> Adicionar(MoradorDTO moradorDTO)
//     {
//         if (!ModelState.IsValid) return CustomResponse(ModelState);

//         var nomeImagem = $"{Guid.NewGuid() + "_" + moradorDTO.Foto}";
//         if(!UploadArquivo(moradorDTO.FotoBase64, nomeImagem))
//         {
//             return CustomResponse(moradorDTO);
//         }

//         var morador = _mapper.Map<Morador>(moradorDTO);
//         morador.Foto = nomeImagem;

//         var novoMorador = await _moradorService.Adicionar(morador);

//         if (novoMorador == null) return CustomResponse(ModelState);

//         moradorDTO = _mapper.Map<MoradorDTO>(novoMorador);

//         return CustomResponse(moradorDTO);
//     }

//     private bool UploadArquivo(string arquivo, string nomeImagem)
//     {
//         if (string.IsNullOrEmpty(arquivo))
//         {
//             NotificarErro("Forneça uma imagem para este morador!");
//             return false;
//         }

//         var imageDataByteArray = Convert.FromBase64String(arquivo);

//         var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", nomeImagem);

//         if (System.IO.File.Exists(filePath))
//         {
//             NotificarErro("Já existe um arquivo com este nome!");
//             return false;
//         }

//         System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

//         return true;
//     }
// }