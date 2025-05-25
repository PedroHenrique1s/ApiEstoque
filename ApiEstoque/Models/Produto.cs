namespace ApiEstoque.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Produto
{
    [JsonIgnore]
    public int id { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório", AllowEmptyStrings = false)]
    public string? Nome { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")]
    public int Quantidade { get; set; }
    public int CategoriaId { get; set; }

    public virtual Categoria? Categoria { get; set; }
}