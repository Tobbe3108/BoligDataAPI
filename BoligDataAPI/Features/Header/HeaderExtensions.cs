namespace BoligDataAPI.Features.Header;

public static class HeaderExtensions
{
  public static string ExtractApiKey(this IHeaderDictionary headers)
  {
    var hasValue = headers.TryGetValue("ApiKey", out var headerValue);
    return hasValue
      ? headerValue.ToString()
      : throw new ArgumentNullException(nameof(headerValue));
  }
}