namespace ApiEstoque.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


public class Categoria
{
    [JsonIgnore]
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório", AllowEmptyStrings = false)]
    public string? Nome { get; set; }

    //Relaciona pro banco N para 1 com categoria e produto
    public virtual List<Produto>? ListaProdutos { get; set; }
}