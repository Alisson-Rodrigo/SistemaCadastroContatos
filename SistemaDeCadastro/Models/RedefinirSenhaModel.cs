using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastro.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        public string Email { get; set; }
    }
}