using AspNetCore.Authentication.ApiKey;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BoligDataAPI.Features.Data;
using BoligDataAPI.Features.Database;
using BoligDataAPI.Features.Ejendom;
using BoligDataAPI.Features.Lejemaal;
using BoligDataAPI.Features.Lejer;
using BoligDataAPI.Features.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

//Use Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("BoligDataAPI"));

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterType<EjendomService>().As<IEjendomService>();
  containerBuilder.RegisterType<LejemaalService>().As<ILejemaalService>();
  containerBuilder.RegisterType<LejerService>().As<ILejerService>();
  containerBuilder.RegisterType<DataFaker>().As<IDataFaker>();
});

builder.Services.AddSwaggerGen(options =>
{
  options.CustomSchemaIds(x => x.FullName);
  options.AddSecurityDefinition("BoligDataAPI",
    new OpenApiSecurityScheme
    {
      Type = SecuritySchemeType.ApiKey,
      In = ParameterLocation.Header,
      Name = "ApiKey",
      Description = "DsLoggerService API Key"
    });
  options.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BoligDataAPI" }
      },
      new List<string>()
    }
  });
});

builder.Services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme)
  .AddApiKeyInHeaderOrQueryParams<ApiKeyProvider>(options =>
  {
    options.Realm = "BoligDataAPI";
    options.KeyName = "ApiKey";
  });

builder.Services.AddAuthorization(options =>
{
  options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});

var app = builder.Build();

var dataFakerConfiguration = builder.Configuration.GetSection("Faker").Get<DataFakerConfiguration>();
var apiKeys = builder.Configuration.GetSection("ApiKeys").Get<IEnumerable<string>>();
var faker = app.Services.GetRequiredService<IDataFaker>();
faker.GenerateData(dataFakerConfiguration, apiKeys);

app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();