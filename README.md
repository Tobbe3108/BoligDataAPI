<!-- PROJECT LOGO -->
<div align="center">

  <h3 align="center">Bolig Data API</h3>

  <p align="center">
    API with fake data developed for a 3. semester datamatiker projekt
    <br />
    <a href="https://boligdataapi.azurewebsites.net/swagger/index.html"><strong>Explore the API</strong></a>
  </p>
</div>
<br />



<!-- Table of Contents -->
## :notebook_with_decorative_cover: Table of Contents
- [Tech Stack](#space_invader-tech-stack)
  * [Global](#earth_africa-global)
  * [Server](#computer-server)
  * [Clients](#iphone-clients)
- [Getting Started](#toolbox-getting-started)
  * [Prerequisites](#bangbang-prerequisites)
  * [Run Locally](#running-run-locally)
  * [Define and register](#building_construction-define-and-register)
    * [Refit](#refit)
    * [Flurl](#flurl)
    * [TypedHttpClient](#typedhttpclient)
  * [Usage](#eyes-usage)
- [Contact](#handshake-contact)



<!-- TechStack -->
## :space_invader: Tech Stack

#### :earth_africa: Global
<ul>
  <li><a href="https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10">C# 10</a></li>
  <li><a href="https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6">.NET 6</a></li>
  <li><a href="https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0">ASP.NET Core</a></li>
</ul>
  
  
#### :computer: Server
<ul>
  <li><a href="https://autofac.org">Autofac</a></li>
  <li><a href="https://github.com/altmann/FluentResults">FluentResults</a></li>
  <li><a href="https://docs.microsoft.com/en-us/ef/core/">Entity Framework Core</a></li>
  <li><a href="https://github.com/RimuTec/Faker">RimuTec.Faker</a></li>
</ul>

#### :iphone: Clients
<ul>
  <li><a href="https://reactiveui.github.io/refit/">Refit</a></li>
  <li><a href="https://flurl.dev">Flurl</a></li>
</ul>



<!-- Getting Started -->
## 	:toolbox: Getting Started

<!-- Prerequisites -->
#### :bangbang: Prerequisites

This project needs <a href="https://dotnet.microsoft.com/en-us/download/dotnet/6.0">.NET 6 SDK</a> to be installed


<!-- Run Locally -->
#### :running: Run Locally

Clone the project
```bash
  git clone https://github.com/Tobbe3108/BoligDataAPI/
```

Go to the project directory
```bash
  cd BoligDataAPI\BoligDataAPI
```

Start the server
```bash
  dotnet run
```



<!-- Usage -->
#### :building_construction: Define and register


#### <a href="https://github.com/Tobbe3108/BoligDataAPI/tree/master/Sample/Refit">Refit</a>

Define service
```csharp
public interface IBoligDataApi
{
  [Get("/Ejendom/{id}")] Task<EjendomResponse?> GetEjendom([AliasAs("id")] Guid ejendomId);
  [Get("/Ejendom")] Task<IEnumerable<EjendomResponse>?> ListEjendomme();
  
  ...
}
```

Register service
```csharp
builder.Services.AddRefitClient<IBoligDataApi>()
  .ConfigureHttpClient(c =>
  {
    c.BaseAddress = new Uri("https://boligdataapi.azurewebsites.net");
    c.DefaultRequestHeaders.Add("ApiKey", "YOUR_API_KEY_HERE");
  });
```


#### <a href="https://github.com/Tobbe3108/BoligDataAPI/tree/master/Sample/Flurl">Flurl</a>

Define service
```csharp
public class BoligDataApi : IBoligDataApi
{
  private readonly IFlurlRequest _authBase = "https://boligdataapi.azurewebsites.net".WithHeader("ApiKey", "YOUR_API_KEY_HERE");
  
  public async Task<EjendomResponse?> GetEjendom(Guid ejendomId) => await _authBase.AppendPathSegment("Ejendom")
    .AppendPathSegment($"{ejendomId}")
    .GetJsonAsync<EjendomResponse>();

  public async Task<IEnumerable<EjendomResponse>?> ListEjendomme() => await _authBase.AppendPathSegment("Ejendom")
    .GetJsonAsync<IEnumerable<EjendomResponse>>();
  
  ...
}
```

Register service
```csharp
  builder.Services.AddTransient<IBoligDataApi, BoligDataApi>();
```


#### <a href="https://github.com/Tobbe3108/BoligDataAPI/tree/master/Sample/TypedHttpClient">TypedHttpClient</a>

Define service
```csharp
public class BoligDataApi : IBoligDataApi
{
  private readonly HttpClient _httpClient;

  public BoligDataApi(HttpClient httpClient)
  {
    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri("https://boligdataapi.azurewebsites.net");
    _httpClient.DefaultRequestHeaders.Add("ApiKey", "YOUR_API_KEY_HERE");
  }

  public async Task<EjendomResponse?> GetEjendom(Guid ejendomId) =>
    await _httpClient.GetFromJsonAsync<EjendomResponse>($"Ejendom/{ejendomId}");

  public async Task<IEnumerable<EjendomResponse>?> ListEjendomme() =>
    await _httpClient.GetFromJsonAsync<IEnumerable<EjendomResponse>>("Ejendom");
  
  ...
}
```

Register service
```csharp
builder.Services.AddHttpClient<IBoligDataApi, BoligDataApi>();
```

<!-- Usage -->
#### :eyes: Usage

Inject service
```csharp
public TestController(IBoligDataApi api)
{
  _api = api;
}
```

Use service to make API calls
```csharp
await _api.ListEjendomme()
```



<!-- Contact -->
## :handshake: Contact
Made with :coffee: by [Tobias Lauritzen](https://about.me/tobiaslauritzen)
