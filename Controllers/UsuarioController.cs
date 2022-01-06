using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;

namespace Biblioteca.Controllers

{
    public class UsuarioController : Controller
    {
        //inserção
        //lista
        //edição
        //Exclusão
        //funções extras


        public IActionResult listaDeUsuarios()
        {

            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            
            return View(new UsuarioService().Listar());

        }


        public IActionResult RegistrarUsuario()
        {

            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();

        }

        [HttpPost]

        public IActionResult RegistrarUsuario(Usuario novoUsuario)
        {

            novoUsuario.Senha = Criptografo.TextoCriptografado(novoUsuario.Senha);
            new UsuarioService().incluirUsuario(novoUsuario);
            return RedirectToAction("CadastroRealizado");

        }


        public IActionResult CadastroRealizado()
        {

            return View();
        }


        
        public IActionResult editarUsuario(int id)
        {

            Usuario u = new UsuarioService().Listar(id);
            return View(u);

        }


        [HttpPost]
        public IActionResult editarUsuario(Usuario userEditado)
        {

            new UsuarioService().editarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");

        }

        //EXCLUSÃO DE USUÁRIOS

        public IActionResult ExcluirUsuario(int id){

            return View(new UsuarioService().Listar(id));
        }


        [HttpPost]

        public IActionResult ExcluirUsuario(string decisao, int id){

            if (decisao == "EXCLUIR")
            {
                ViewData["Mensagem"] = "Exclusão de usuário" + new UsuarioService().Listar(id).Nome + " realizado exclusão!";
                new UsuarioService().excluirUsuario(id);
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
            else {
                ViewData["Mensagem"] = "Exclusão Cancelada";
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }

        }


        public IActionResult Sair()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");


        }



        public IActionResult NeedAdmin()
        {

            Autenticacao.CheckLogin(this);
            return View();

        }

    }
}