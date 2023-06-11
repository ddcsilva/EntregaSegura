namespace EntregaSegura.Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
    }
}