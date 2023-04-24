using Andreani.ARQ.AMQStreams.Extensions;
using Andreani.ARQ.WebHost.Extension;
using Andreani.Scheme.Onboarding;
using desafio.Application;
using desafio.Infrastructure;
using desafio.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins(builder.Configuration["localhostAPI"]).AllowAnyMethod().AllowAnyHeader();
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

app.UseCors(MyAllowSpecificOrigins);

app.ConfigureAndreani(app.Environment, app.Services.GetRequiredService<IApiVersionDescriptionProvider>());

app.Run();
