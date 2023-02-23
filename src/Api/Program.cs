using Andreani.ARQ.AMQStreams.Extensions;
using Andreani.ARQ.WebHost.Extension;
using Andreani.Scheme.Onboarding;
using desafio.Application;
using desafio.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAndreaniWebHost(args);
builder.Services.ConfigureAndreaniServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddKafka(builder.Configuration)
    .ToProducer<Pedido>("PedidoCreado")
    .Build();

var app = builder.Build();

app.ConfigureAndreani(app.Environment, app.Services.GetRequiredService<IApiVersionDescriptionProvider>());

app.Run();
