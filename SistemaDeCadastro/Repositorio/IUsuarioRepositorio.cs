using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Repositorio
{
    public interface IUsuarioRepositorio
    {

        public bool Adicionar(UserModel usuario);
        UserModel InfoUsuario(int id);
        UserModel EditarUsuario(UserModel NovoUsuario, int id);

        UserModel BuscarPorLogin(string login);
        List<UserModel> GetUserList();
        bool DeletarUsuario(int Id);
        bool verificarUsuarioExistente(string login);
        UserModel BuscarPorEmail(string email);
        bool verificarEmailExistente(string email);





    }
}
