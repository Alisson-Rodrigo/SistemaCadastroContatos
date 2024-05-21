using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastro.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do contato")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o email do contato")]
        [EmailAddress(ErrorMessage = "Digite um email valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite o numero do contato")]
        [Phone(ErrorMessage = "Digite um numero valido")]
        public string Telefone { get; set; }
        public int? UsuarioID { get; set; }
        public UserModel Usuario { get; set; }
    }
}
