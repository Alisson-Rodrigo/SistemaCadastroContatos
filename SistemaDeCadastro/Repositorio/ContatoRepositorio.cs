using SistemaDeCadastro.Data;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {

        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }

        public List<ContatoModel> GetContatoList(int userId)
        {
            return _bancoContext.Contatos.Where(c => c.Id == userId).ToList();
        }

        public ContatoModel InfoContato(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(c => c.Id == id);
        }
        public ContatoModel EditarContato(ContatoModel NovoContato, int id)
        {
            var contatoDB = InfoContato(id);
            if (contatoDB == null) throw new Exception("Erro na atualização de dados");

            contatoDB.Nome = NovoContato.Nome;
            contatoDB.Email = NovoContato.Email;
            contatoDB.Telefone = NovoContato.Telefone;
            _bancoContext.Contatos.Update(contatoDB);
            _bancoContext.SaveChanges();
            return contatoDB;
        }

        public bool DeletarContato(int id) {
            try
            {
                var contato = _bancoContext.Contatos.FirstOrDefault(c => c.Id == id);
                if (contato == null) throw new Exception("Erro ao deletar contato");
                _bancoContext.Contatos.Remove(contato);
                _bancoContext.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

    }
}
