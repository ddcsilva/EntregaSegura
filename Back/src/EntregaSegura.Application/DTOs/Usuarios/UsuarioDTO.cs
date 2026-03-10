using System.ComponentModel.DataAnnotations;
using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Application.DTOs;

public class UsuarioDTO
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O login é obrigatório")]
    [StringLength(50, ErrorMessage = "O login deve ter no máximo 100 caracteres")]
    public string? Login { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(100, ErrorMessage = "A senha deve ter no máximo 50 caracteres")]
    public string? Senha { get; set; }
    public string? Token { get; set; }
    public string? Foto { get; set; }
    public PerfilUsuario? Perfil { get; set; }
    public int PessoaId { get; set; }
    public PessoaDTO? Pessoa { get; set; }
}