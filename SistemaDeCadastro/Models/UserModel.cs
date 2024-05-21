using System;
using System.ComponentModel.DataAnnotations;
using SistemaDeCadastro.Enums;
using SistemaDeCadastro.Helper;

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

        public bool VerificarSenha (string senha) {
            return senha.GerarHash() == Senha;
        }
        public void AlterarSenhaHash() {
            Senha = Senha.GerarHash();
        }

        public string GerarNovaSenha() {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }

        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }
        
    }
}
