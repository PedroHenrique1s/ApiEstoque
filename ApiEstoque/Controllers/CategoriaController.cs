namespace ApiEstoque.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiEstoque.Models;
using ApiEstoque.Contexto;

[ApiController]
[Route("[controller]")]
[Authorize]

public class CategoriaController(BancoDados Banco):ControllerBase
{
    [HttpGet]
    public ActionResult ListarCategorias()
    {
        List<object> lista = [..Banco.TabelaCategorias.OrderBy(c => c.Nome).ToList().Select(c => new{
            c.Id,
            c.Nome,
        })];

        if (lista.Count > 0 ) return Ok(lista);
        return NotFound();
    }

    [HttpGet("{id}")]
    public ActionResult BuscarCategoria(int id)
    {
        Categoria? cat = Banco.TabelaCategorias.SingleOrDefault( c => c.Id == id);
        if (cat == null) return NotFound();
        return Ok(cat); 
    }


    [HttpPost]
    public ActionResult CriarCategoria(Categoria model) 
    {
        Banco.TabelaCategorias.Add(model);
        Banco.SaveChanges();
        return Ok(new{Mensagem = "Categoria cadastrada com sucesso"});
    }


    [HttpPut("{id}")]
    public ActionResult EditarCategoria(int id, Categoria model)
    {
        Categoria? cat = Banco.TabelaCategorias.SingleOrDefault( c => c.Id == id);
        if( cat == null) return NotFound();
        Banco.Entry(cat).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        model.Id = id;
        Banco.TabelaCategorias.Update(model);
        return Ok(new{ Mensagem ="Categoria atualizada com sucesso"});
    }

    [HttpDelete("{id}")]
    public ActionResult ExcluirCategoria(int id)
    {
        Categoria? cat = Banco.TabelaCategorias.SingleOrDefault( c => c.Id == id);
        if( cat == null) return NotFound();
        Banco.TabelaProdutos.RemoveRange([..Banco.TabelaProdutos.Where(e => e.CategoriaId == cat.Id)]);
        Banco.Remove(cat);
        Banco.SaveChanges();
        return Ok(new{ Mensagem = "Categoria excluída e seus produtos foram excluidos"});
    }
    
}