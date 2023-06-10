// using EntregaSegura.Domain.Interfaces.Account;
// using Microsoft.AspNetCore.Identity;
// using System.Text;

// namespace EntregaSegura.Infra.Data.Identity;

// public class AutenticacaoService : IAutenticacaoService
// {
//     private readonly UserManager<ApplicationUser> _userManager;
//     private readonly SignInManager<ApplicationUser> _signInManager;
//     private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

//     public AutenticacaoService(UserManager<ApplicationUser> userManager,
//                                SignInManager<ApplicationUser> signInManager,
//                                IPasswordHasher<ApplicationUser> passwordHasher)
//     {
//         _userManager = userManager;
//         _signInManager = signInManager;
//         _passwordHasher = passwordHasher;
//     }

//     public async Task<bool> AutenticarAsync(string email, string senha)
//     {
//         var resultadoAutenticacao = await _signInManager.PasswordSignInAsync(email, senha, false, lockoutOnFailure: false);

//         return resultadoAutenticacao.Succeeded;
//     }

//     public async Task<bool> RegistrarAsync(string email, string senha, int moradorId)
//     {
//         var applicationUser = new ApplicationUser { UserName = email, Email = email, MoradorId = moradorId };

//         var resultadoRegistro = await _userManager.CreateAsync(applicationUser, senha);

//         if (resultadoRegistro.Succeeded)
//         {
//             await _userManager.AddToRoleAsync(applicationUser, "MORADOR");
//             await _signInManager.SignInAsync(applicationUser, isPersistent: false);
//         }

//         return resultadoRegistro.Succeeded;
//     }

//     public async Task LogoutAsync()
//     {
//         await _signInManager.SignOutAsync();
//     }

//     public string GerarSenhaAleatoria()
//     {
//         const string caracteresValidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";

//         Random random = new Random();
//         StringBuilder senha = new StringBuilder();

//         // Adicione pelo menos uma letra maiúscula
//         senha.Append(caracteresValidos[random.Next(26, 51)]);

//         // Adicione pelo menos um dígito
//         senha.Append(caracteresValidos[random.Next(52, 62)]);

//         // Adicione pelo menos um caractere especial
//         senha.Append(caracteresValidos[random.Next(62, caracteresValidos.Length)]);

//         // Adicione outros caracteres aleatórios até atingir o comprimento mínimo de senha
//         while (senha.Length < 8)
//         {
//             senha.Append(caracteresValidos[random.Next(caracteresValidos.Length)]);
//         }

//         return senha.ToString();
//     }

//     private bool VerificarCriteriosSenha(string senha)
//     {
//         var passwordOptions = _userManager.Options.Password;

//         var hasDigit = passwordOptions.RequireDigit ? senha.Any(char.IsDigit) : true;
//         var hasLowercase = passwordOptions.RequireLowercase ? senha.Any(char.IsLower) : true;
//         var hasUppercase = passwordOptions.RequireUppercase ? senha.Any(char.IsUpper) : true;
//         var hasNonAlphanumeric = passwordOptions.RequireNonAlphanumeric ? senha.Any(char.IsSymbol) : true;
//         var correctLength = senha.Length >= passwordOptions.RequiredLength;

//         return hasDigit && hasLowercase && hasUppercase && hasNonAlphanumeric && correctLength;
//     }
// }