using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Repositorio
{
    public interface IContatoRepositorio
    {
        List<ContatoModel> GetContatoList(int userId);
        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel InfoContato(int id);

        ContatoModel EditarContato(ContatoModel NovoContato, int id);

        bool DeletarContato(int Id);

    }
}
