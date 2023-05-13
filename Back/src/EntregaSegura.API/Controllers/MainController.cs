using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EntregaSegura.API.Controllers;

[ApiController]
public class MainController : ControllerBase
{
    private readonly INotificadorErros _notificadorErros;

    protected MainController(INotificadorErros notificadorErros)
    {
        _notificadorErros = notificadorErros;
    }

    protected bool OperacaoValida()
    {
        return !_notificadorErros.TemNotificacoes();
    }

    protected ActionResult CustomResponse(object result = null)
    {
        if (OperacaoValida())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notificadorErros.ObterNotificacoes().Select(n => n.Mensagem)
        });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
        return CustomResponse();
    }

    protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var mensagemErro = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotificarErro(mensagemErro);
        }
    }

    protected void NotificarErro(string mensagem)
    {
        _notificadorErros.Handle(new NotificacaoErros(mensagem));
    }
}