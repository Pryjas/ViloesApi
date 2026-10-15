using System.Globalization;
using Microsoft.Extensions.DependencyInjection;

namespace ViloesApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);

        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

        // Para na primeira falha de cada regra: ex. não consulta o banco (MustExists) se o id já é inválido.
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

        return services;
    }
}
