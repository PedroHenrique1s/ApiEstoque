namespace ApiEstoque.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


public class Usuario
{
    [JsonIgnore]
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório", AllowEmptyStrings = false)]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório", AllowEmptyStrings = false)]
    [EmailAddress(ErrorMessage = "E-mail Inválido")]
    public string? Email { get; set; }
    [JsonIgnore]
    public string? Senha { get; set; }
}