namespace ApiEstoque.Controllers;

using Microsoft.AspNetCore.Mvc;
using ApiEstoque.Models;
using ApiEstoque.Contexto;

[ApiController]
[Route("api/[controller]")]

public class UsuarioController(BancoDados Banco) : ControllerBase
{
    [HttpGet]
    public ActionResult ListarUsuarios()
    {
        List<Usuario> lista = [.. Banco.TabelaUsuarios.OrderBy(u => u.Nome)];
        if (lista.Count > 0) return Ok(lista);
        return NotFound();
    }

    [HttpPost]
    public ActionResult CriarUsuario(Usuario model)
    {
        model.Senha = $"estoque@{DateTime.Now:ddMMyyyy}";
        Banco.TabelaUsuarios.Add(model);
        Banco.SaveChanges();
        return Ok(new { Mensagem = "Usuario criado com sucesso" });
    }
}