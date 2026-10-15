using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using ViloesApi.Application;
using ViloesApi.Infra;
using ViloesApi.WebAPI.Attributes;
using ViloesApi.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfra(builder.Configuration);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services
    .AddControllers(options =>
    {
        options.SuppressAsyncSuffixInActionNames = false;
        options.Filters.Add<NotFoundFilterAttribute>();
    })
    .AddJsonOptions(options =>
    {
        // Campo que não existe no recurso (ex.: "maldad") é rejeitado com 400 em vez de ser ignorado.
        options.JsonSerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;

        // Enums trafegam pelo nome ("Filme", "Serie"); número no lugar do nome é rejeitado com 400.
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
    })
    .ConfigureApiBehaviorOptions(options =>
        options.InvalidModelStateResponseFactory = ModeloInvalidoResponseFactory.Criar);

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapControllers();

app.CriarBancoDeDados();

app.Run();
