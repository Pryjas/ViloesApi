using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ViloesApi.Domain.Common.Validacoes;
using ViloesApi.Infra.Hosting.Wrappers;

namespace ViloesApi.WebAPI.Configurations;

/// <summary>
/// Resposta 400 para corpo de requisição que não pôde ser lido (JSON malformado, tipo errado ou campo desconhecido).
/// Substitui as mensagens padrão do ASP.NET, que vêm em inglês e expõem nomes de tipos internos.
/// </summary>
public static class ModeloInvalidoResponseFactory
{
    public static IActionResult Criar(ActionContext context)
    {
        // Corpo vazio gera erro em "$" e em "command"; ambos viram uma única falha em "corpo".
        var falhas = context.ModelState
            .SelectMany(e => e.Value.Errors.Select(erro =>
                new ValidacaoFalha(NomeDoCampo(e.Key), Mensagem(e.Key, erro.ErrorMessage))))
            .Distinct()
            .ToList();

        var result = new Response
        {
            Succeeded = false,
            Message = "Falha ao validar os campos.",
            Errors = falhas,
        };

        return new BadRequestObjectResult(result);
    }

    // O ModelState identifica o campo pelo caminho JSON ("$.ano"); "$", vazio ou o nome do parâmetro
    // ("command") indicam que o problema é o corpo inteiro.
    private static bool EhCorpoInteiro(string chave)
        => !chave.StartsWith('$') || chave.TrimStart('$', '.').Length is 0;

    private static string NomeDoCampo(string chave)
        => EhCorpoInteiro(chave) ? "corpo" : JsonNamingPolicy.CamelCase.ConvertName(chave.TrimStart('$', '.'));

    private static string Mensagem(string chave, string mensagemOriginal)
    {
        if (mensagemOriginal.Contains("could not be mapped"))
        {
            return "Campo não esperado para este recurso.";
        }

        if (EhCorpoInteiro(chave))
        {
            return "O corpo da requisição está vazio ou não é um JSON válido.";
        }

        return "Valor em formato inválido para o campo.";
    }
}
