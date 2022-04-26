using Sample.Refit;

namespace Sample.Flurl;

public static class FlurlExtensions
{
  public static void RegisterFlurl(this WebApplicationBuilder builder)
  {
    builder.Services.AddTransient<IBoligDataApi, BoligDataApi>();
  }
}
