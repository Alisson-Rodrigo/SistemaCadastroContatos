using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastro.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O campo Login é obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string Senha { get; set; }
    }
}
