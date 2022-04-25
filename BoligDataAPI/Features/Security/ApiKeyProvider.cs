using AspNetCore.Authentication.ApiKey;

namespace BoligDataAPI.Features.Security;

public class ApiKeyProvider : IApiKeyProvider
{
  private readonly IConfiguration _configuration;

  public ApiKeyProvider(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public Task<IApiKey?> ProvideAsync(string key)
  {
    var keys = _configuration.GetSection("ApiKeys").Get<IEnumerable<string>>();
    return Task.FromResult<IApiKey?>(keys.Contains(key)
      ? new ApiKey(key, "DsLoggerService")
      : null);
  }
}