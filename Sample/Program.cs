using Sample.Flurl;
using Sample.Refit;
using Sample.TypedHttpClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => c.CustomSchemaIds(x => x.FullName));

var clientType = Environment.GetEnvironmentVariable("CLIENT_TYPE");

switch (clientType)
{
  case "Refit":
    builder.RegisterRefit();
    break;
  case "Flurl":
    builder.RegisterFlurl();
    break;
  case "TypedHttpClient":
    builder.RegisterTypedHttpClient();
    break;
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();