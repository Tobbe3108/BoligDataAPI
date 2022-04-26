using Refit;

namespace Sample.Refit;

public static class RefitExtensions
{
  public static void RegisterRefit(this WebApplicationBuilder builder)
  {
    builder.Services.AddRefitClient<IBoligDataApi>()
      .ConfigureHttpClient(c =>
      {
        c.BaseAddress = new Uri(Constants.BoligDataApiUrl);
        c.DefaultRequestHeaders.Add("ApiKey", Constants.BoligDataApiKey);
      });
  }
}