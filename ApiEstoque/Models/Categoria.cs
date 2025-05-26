namespace ApiEstoque.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiEstoque.Contexto;

public class Categoria
{
    [JsonIgnore]
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório", AllowEmptyStrings = false)]
    [CustomValidation(typeof(Categoria), "ValidarNome")]
    public string? Nome { get; set; }

    //Relaciona pro banco N para 1 com categoria e produto
    public virtual List<Produto>? ListaProdutos { get; set; }

    public static ValidationResult ValidarNome(string nome, ValidationContext context)
    {
        BancoDados banco = context.GetServices<BancoDados>().First();
        IHttpContextAccessor acessor = context.GetRequiredService<IHttpContextAccessor>();
        RouteValueDictionary dadosRota = acessor.HttpContext!.GetRouteData().Values;
        int id = (dadosRota != null && dadosRota.TryGetValue("id", out var numero) && numero != null) ? Convert.ToInt32(numero) : 0;
        if ((id == 0 && banco.TabelaCategorias.Any(u => u.Nome == nome)) || (id > 0 && banco.TabelaCategorias.Any(u => u.Nome == nome && u.Id != id))) return new ValidationResult("Já existe uma categoria com o nome informado");
        return ValidationResult.Success!;
    }
}