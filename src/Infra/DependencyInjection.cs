using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViloesApi.Domain.Interfaces.Infrastructure;
using ViloesApi.Infra.Data;
using ViloesApi.Infra.Hosting.ExceptionHandlers;
using ViloesApi.Infra.MediatR.Behaviours;

namespace ViloesApi.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ViloesDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("Viloes")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        // A ordem importa: o primeiro handler que reconhecer a exceção monta a resposta.
        services.AddProblemDetails();
        services.AddExceptionHandler<RequisicaoInvalidaExceptionHandler>();
        services.AddExceptionHandler<ValidacaoExceptionHandler>();
        services.AddExceptionHandler<NaoEncontradoExceptionHandler>();
        services.AddExceptionHandler<ErroInesperadoExceptionHandler>();

        return services;
    }

    /// <summary>
    /// Cria o arquivo SQLite e as tabelas na primeira execução.
    /// </summary>
    public static void CriarBancoDeDados(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        scope.ServiceProvider.GetRequiredService<ViloesDbContext>().Database.EnsureCreated();
    }
}
