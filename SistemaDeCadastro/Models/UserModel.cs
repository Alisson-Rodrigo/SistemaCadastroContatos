using System;
using System.ComponentModel.DataAnnotations;
using SistemaDeCadastro.Enums;

namespace SistemaDeCadastro.Models
{
    public class UserModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string name { get; set; }
        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }
        [Required(ErrorMessage = "Digite a senha do usuário")]
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        
    }
}
