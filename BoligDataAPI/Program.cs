using BoligDataAPI.Features.Data;
using BoligDataAPI.Features.Ejendom;
using BoligDataAPI.Features.Lejemaal;
using BoligDataAPI.Features.Lejer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dataFakerConfiguration = builder.Configuration.GetSection("Faker").Get<DataFakerConfiguration>();
var faker = new DataFaker(dataFakerConfiguration);
faker.GenerateData();

var ejendomService = new EjendomService(faker.Ejendomme);
var lejemaalService = new LejemaalService(faker.Lejemaal);
var lejerService = new LejerService(faker.Lejere);

builder.Services.AddSingleton(ejendomService);
builder.Services.AddSingleton(lejemaalService);
builder.Services.AddSingleton(lejerService);

builder.Services.AddSwaggerGen(options =>
{
  options.CustomSchemaIds(x => x.FullName);
});

//TODO Add api keys

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();