using Microsoft.AspNetCore.Mvc;
using SistemaCadastros.Models;
using SistemaCadastros.Repositorio;

namespace SistemaCadastros.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Criar() { return View(); }

        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();

            return View(contatos);
        }

        public IActionResult Editar(int id) {

            var contato = _contatoRepositorio.ListarPorId(id);

            return View(contato); 
        }
        public IActionResult ApagarConfirmacao(int id) {

            var contato = _contatoRepositorio.ListarPorId(id);

            return View(contato); 
        }

        
        public IActionResult Apagar(int id) {
            try
            {
                bool isApagado = _contatoRepositorio.Apagar(id);

                if (isApagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso";
                }
                else
                {
                    TempData["MensagemSucesso"] = "Ops, não conseguimos apagar seu contato";
                }

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao apagar contato, detalhe do erro: {ex.Message}";
                //teste
                return RedirectToAction("Index");
            }


        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (Exception ex) {
                TempData["MensagemErro"] = $"Erro ao cadastrar contato, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Editar", contato);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao alterar contato, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }

        }

    }
}
