using SistemaDeCadastro.Data;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Helper;
using System;

namespace SistemaDeCadastro.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public bool Adicionar(UserModel usuario)
        {
            bool aux = false;
            if (usuario == null) {
                aux = false;
            }
            else {
                if (verificarUsuarioExistente(usuario.Login) == false) {
                    if (verificarEmailExistente(usuario.Email) == false){
                        usuario.DataCriacao = DateTime.Now;
                        usuario.AlterarSenhaHash();
                        _bancoContext.Usuarios.Add(usuario);
                        _bancoContext.SaveChanges();
                        aux = true;
                    }
                }
            }
            return aux;
        }

        public bool verificarUsuarioExistente(string login)
        {   bool aux = false;
            var usuario = _bancoContext.Usuarios.FirstOrDefault(c => c.Login.ToUpper() == login.ToUpper());
            if (usuario != null) {
                if (usuario.Login.ToUpper() == login.ToUpper()) {
                    aux = true;
                }
            }
            return aux;
        }

        public bool verificarEmailExistente(string email)
        {
            bool aux = false;
            var usuario = _bancoContext.Usuarios.FirstOrDefault(c => c.Email.ToUpper() == email.ToUpper());
            if (usuario != null)
            {
                if (usuario.Email.ToUpper() == email.ToUpper())
                {
                    aux = true;
                }
            }
            return aux;
        }

        public UserModel BuscarPorLogin(string login)
        {
            UserModel usuario = _bancoContext.Usuarios.FirstOrDefault(c => c.Login.ToUpper() == login.ToUpper());
            return usuario;

        }

        public UserModel BuscarPorEmail(string email)
        {
            UserModel usuario = _bancoContext.Usuarios.FirstOrDefault(c => c.Email.ToUpper() == email.ToUpper());
            return usuario;
        }

        public List<UserModel> GetUserList()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public UserModel InfoUsuario(int id)
        {
            var usuario = _bancoContext.Usuarios.FirstOrDefault(c => c.id == id);
            return usuario;
        }
        public UserModel EditarUsuario(UserModel NovoUsuario, int id)
        {
            var usuarioDB = InfoUsuario(id);
            if (usuarioDB == null) throw new Exception("Erro na atualização de dados");

            usuarioDB.name = NovoUsuario.name;
            usuarioDB.Email = NovoUsuario.Email;
            usuarioDB.Login = NovoUsuario.Login;
            usuarioDB.Perfil = NovoUsuario.Perfil;
            usuarioDB.Senha = NovoUsuario.Senha;
            usuarioDB.DataAtualizacao = DateTime.Now;
            
            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();
            return usuarioDB;
        }
        public bool DeletarUsuario(int id) {
            try
            {
                var usuario = _bancoContext.Usuarios.FirstOrDefault(c => c.id == id);
                if (usuario == null) throw new Exception("Erro ao deletar usuario");
                _bancoContext.Usuarios.Remove(usuario);
                _bancoContext.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

                public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = BuscarPorID(alterarSenhaModel.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

            if (!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!");

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }

    }
}
