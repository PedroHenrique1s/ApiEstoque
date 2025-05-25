namespace ApiEstoque;
using System.Security.Cryptography;
using System.IO;

public static class Gerador
{
    public static void GerarChave()
    {
        if(File.Exists(".env"))
        {
            foreach(string linha in File.ReadAllLines(".env"))
            {
                if(linha.StartsWith("ChaveSecreta=")) return;
            }
        }
        using var writer = File.AppendText(".env");
        writer.WriteLine($"ChaveSecreta={Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))}");
    }
}
