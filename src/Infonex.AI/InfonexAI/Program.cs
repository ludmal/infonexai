using System.Reflection;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.HttpResults;
using NodaTime;
using WebApplication2;
using WebApplication2.Endpoints.v1;
using WebApplication2.Jobs;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
// builder.Services.AddMediator();

builder.Services.AddHostedService<ContentParserJob>();
builder.Services.AddSingleton<IClock>(SystemClock.Instance);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// app.MapPost("/products", ProductsEndpoints.GetData);
// app.MapPost("/html", ProductsEndpoints.GetUrlData);

app.MapPost("{brandId}/content/parse", ContentParser.Endpoint);

app.Run();