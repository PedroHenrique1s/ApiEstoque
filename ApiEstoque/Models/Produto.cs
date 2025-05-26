namespace ApiEstoque.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiEstoque.Contexto;

public class Produto
{
    [JsonIgnore]
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório", AllowEmptyStrings = false)]
    [CustomValidation(typeof(Produto), "ValidarNome")]
    public string? Nome { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")]
    public int Quantidade { get; set; }
    [CustomValidation(typeof(Produto), "ValidarCategoria")]
    public int CategoriaId { get; set; }

    public virtual Categoria? Categoria { get; set; }

    public static ValidationResult ValidarNome(string nome, ValidationContext context)
    {
        BancoDados banco = context.GetServices<BancoDados>().First();
        IHttpContextAccessor acessor = context.GetRequiredService<IHttpContextAccessor>();
        RouteValueDictionary dadosRota = acessor.HttpContext!.GetRouteData().Values;
        int id = (dadosRota != null && dadosRota.TryGetValue("id", out var numero) && numero != null) ? Convert.ToInt32(numero) : 0;
        if ((id == 0 && banco.TabelaProdutos.Any(u => u.Nome == nome)) || (id > 0 && banco.TabelaProdutos.Any(u => u.Nome == nome && u.Id != id))) return new ValidationResult("Já existe um produto com o nome informado");
        return ValidationResult.Success!;
    }

    public static ValidationResult ValidarCategoria(int id, ValidationContext context)
    {
        BancoDados bd = context.GetRequiredService<BancoDados>();
        if (bd.TabelaCategorias.Any(c => c.Id == id)) return ValidationResult.Success!;
        return new ValidationResult("Selecione uma categoria válida");
    }
}