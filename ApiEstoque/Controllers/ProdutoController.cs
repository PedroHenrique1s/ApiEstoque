namespace ApiEstoque.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiEstoque.Models;
using ApiEstoque.Contexto;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
[Authorize]


public class ProdutoController(BancoDados Banco): ControllerBase
{
    [HttpGet]
    public ActionResult ListarProdutos()
    {
        List<object> lista = [..Banco.TabelaProdutos.OrderBy(p => p.Nome).Include(p => p.Categoria).Select(p => new{
            p.Id,
            p.Nome,
            p.Quantidade,
            Categoria = new {
                p.Categoria!.Id,
                p.Categoria!.Nome
            }
        })];
        if (lista.Count > 0)  return Ok(lista);
        return NotFound();
    }

    [HttpGet("{id}")]
    public ActionResult BuscarProduto(int id)
    {
        Produto? prod = Banco.TabelaProdutos.SingleOrDefault( c => c.Id == id);
        if (prod == null) return NotFound();
        return Ok(prod); 
    }

    [HttpPost]
    public ActionResult CriarProduto(Produto model)
    {
        Banco.TabelaProdutos.Add(model);
        Banco.SaveChanges();
        return Ok( new{ Mensagem = "Produto criado com sucesso"});
    }

    [HttpPut]
    public ActionResult EditarProduto(int id, Produto model)
    {
        Produto? prod = Banco.TabelaProdutos.SingleOrDefault( p => p.Id == id);
        if(prod == null) return NotFound();
        Banco.Entry(prod).State = EntityState.Detached;
        model.Id = id;
        Banco.TabelaProdutos.Update(model);
        Banco.SaveChanges();
        return Ok( new{Mensagem = "Produto alterado com sucesso"});
    }
    

    [HttpDelete("{id}")]
    public ActionResult ExcluirProduto(int id)
    {
        Produto? prod = Banco.TabelaProdutos.SingleOrDefault(p => p.Id == id);
        if (prod == null) return NotFound();
        Banco.TabelaProdutos.Remove(prod);
        Banco.SaveChanges();
        return Ok(new { Mensagem = "Produto excluído com sucesso" });
    }
}
