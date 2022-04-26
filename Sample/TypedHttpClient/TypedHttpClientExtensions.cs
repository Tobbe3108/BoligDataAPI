using Sample.Refit;

namespace Sample.TypedHttpClient;

public static class TypedHttpClientExtensions
{
  public static void RegisterTypedHttpClient(this WebApplicationBuilder builder)
  {
    builder.Services.AddHttpClient<IBoligDataApi, BoligDataApi>();
  }
}