namespace ApiEstoque.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiEstoque.Contexto;
using Microsoft.AspNetCore.StaticAssets;

public class Usuario
{
    [JsonIgnore]
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório", AllowEmptyStrings = false)]
    [CustomValidation(typeof(Usuario), "ValidarNomeUsuario")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório", AllowEmptyStrings = false)]
    [EmailAddress(ErrorMessage = "E-mail Inválido")]
    [CustomValidation(typeof(Usuario), "ValidarEmail")]
    public string? Email { get; set; }
    [JsonIgnore]
    public string? Senha { get; set; }
    public static ValidationResult ValidarNomeUsuario(string nome, ValidationContext context)
    {
        BancoDados banco = context.GetServices<BancoDados>().First();
        IHttpContextAccessor acessor = context.GetRequiredService<IHttpContextAccessor>();
        RouteValueDictionary dadosRota = acessor.HttpContext!.GetRouteData().Values;
        int id = (dadosRota != null && dadosRota.TryGetValue("id", out var numero) && numero != null) ? Convert.ToInt32(numero): 0;
        if((id == 0 && banco.TabelaUsuarios.Any(u => u.Nome == nome)) || (id > 0 && banco.TabelaUsuarios.Any(u => u.Nome == nome && u.Id != id))) return new ValidationResult("Já existe um usuário com o nome informado");
        return ValidationResult.Success!;
    }

public static ValidationResult ValidarEmail(string email, ValidationContext context)
    {
        BancoDados banco = context.GetServices<BancoDados>().First();
        IHttpContextAccessor acessor = context.GetRequiredService<IHttpContextAccessor>();
        RouteValueDictionary dadosRota = acessor.HttpContext!.GetRouteData().Values;
        int id = (dadosRota != null && dadosRota.TryGetValue("id", out var numero) && numero != null) ? Convert.ToInt32(numero): 0;
        if((id == 0 && banco.TabelaUsuarios.Any(u => u.Email == email)) || (id > 0 && banco.TabelaUsuarios.Any(u => u.Email == email && u.Id != id))) return new ValidationResult("Já existe um usuário com o e-mail informado");
        return ValidationResult.Success!;
    }
    
}