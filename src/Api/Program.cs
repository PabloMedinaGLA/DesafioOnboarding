using Andreani.ARQ.AMQStreams.Extensions;
using Andreani.ARQ.WebHost.Extension;
using Andreani.Scheme.Onboarding;
using desafio.Application;
using desafio.Infrastructure;
using desafio.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(b =>
{
    b.AddDefaultPolicy(options =>
    {
        options.WithOrigins("*").WithHeaders("*").WithMethods("*");
    });
});


builder.Host.ConfigureAndreaniWebHost(args);
builder.Services.ConfigureAndreaniServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddKafka(builder.Configuration)
    .CreateOrUpdateTopic(6,"PedidoCreadoMP")
    .ToProducer<Pedido>("PedidoCreadoMP")
    .ToConsumer<Subscriber, Pedido>("PedidoAsignadoMP")
    .Build();

var app = builder.Build();
app.UseCors();
app.ConfigureAndreani(app.Environment, app.Services.GetRequiredService<IApiVersionDescriptionProvider>());

app.Run();
