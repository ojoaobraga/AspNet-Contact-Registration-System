using SistemaCadastros.Data;
using SistemaCadastros.Models;
using System.Reflection.Metadata.Ecma335;

namespace SistemaCadastros.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext context) => _bancoContext = context;

        public UsuarioModel ListarPorId(int id) => _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);

        public List<UsuarioModel> BuscarTodos() => _bancoContext.Usuarios.ToList();

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;

            _bancoContext.Usuarios.Add(usuario);

            _bancoContext.SaveChanges();

            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            var usuarioDb = ListarPorId(usuario.Id);

            if (usuarioDb == null) {
                throw new Exception("O usuario que você está tentando alterar é nulo");
            }

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Login = usuario.Login;
            usuarioDb.Perfil = usuario.Perfil;
            usuarioDb.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDb);

            _bancoContext.SaveChanges();

            return usuario;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDb = ListarPorId(id);

            if (usuarioDb == null)
            {
                throw new Exception("O usuario que você está tentando apagar é nulo");
            }

            _bancoContext.Usuarios.Remove(usuarioDb);

            _bancoContext.SaveChanges();

            return true;

        }
    }
}
