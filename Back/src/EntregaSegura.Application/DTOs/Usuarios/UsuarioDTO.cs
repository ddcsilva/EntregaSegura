using System.ComponentModel.DataAnnotations;
using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Application.DTOs;

public class UsuarioDTO
{
    [Key]
    public int Id { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public string Token { get; set; }
    public string Foto { get; set; }
    public PerfilUsuario Perfil { get; set; }
    public int PessoaId { get; set; }
    public PessoaDTO Pessoa { get; set; }
}