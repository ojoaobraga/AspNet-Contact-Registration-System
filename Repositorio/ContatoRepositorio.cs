using SistemaCadastros.Data;
using SistemaCadastros.Models;
using System.Reflection.Metadata.Ecma335;

namespace SistemaCadastros.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext context) => _bancoContext = context;

        public ContatoModel ListarPorId(int id) => _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);

        public List<ContatoModel> BuscarTodos() => _bancoContext.Contatos.ToList();

        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);

            _bancoContext.SaveChanges();

            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            var contatoDb = ListarPorId(contato.Id);

            if (contatoDb == null) {
                throw new Exception("O contato que você está tentando alterar é nulo");
            }

            contatoDb.Nome = contato.Nome;
            contatoDb.Email = contato.Email;
            contatoDb.Celular = contato.Celular;

            _bancoContext.Contatos.Update(contatoDb);

            _bancoContext.SaveChanges();

            return contato;
        }

        public bool Apagar(int id)
        {
            var contatoDb = ListarPorId(id);

            if (contatoDb == null)
            {
                throw new Exception("O contato que você está tentando apagar é nulo");
            }

            _bancoContext.Contatos.Remove(contatoDb);

            _bancoContext.SaveChanges();

            return true;

        }
    }
}
