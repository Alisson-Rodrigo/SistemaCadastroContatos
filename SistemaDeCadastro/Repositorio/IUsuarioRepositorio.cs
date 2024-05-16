using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Repositorio
{
    public interface IUsuarioRepositorio
    {

        UserModel Adicionar(UserModel usuario);
        UserModel InfoUsuario(int id);
        UserModel EditarUsuario(UserModel NovoUsuario, int id);
        List<UserModel> GetUserList();
        bool DeletarUsuario(int Id);



    }
}
