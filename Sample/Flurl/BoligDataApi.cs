using Flurl.Http;
using Sample.Refit;
using Sample.Shared;

namespace Sample.Flurl;

public class BoligDataApi : IBoligDataApi
{
  private readonly IFlurlRequest _authBase = Constants.BoligDataApiUrl.WithHeader("ApiKey", Constants.BoligDataApiKey);

  //Ejendom
  public async Task<EjendomResponse?> GetEjendom(Guid ejendomId) =>
    await _authBase.AppendPathSegment("Ejendom")
      .AppendPathSegment($"{ejendomId}")
      .GetJsonAsync<EjendomResponse>();

  public async Task<IEnumerable<EjendomResponse>?> ListEjendomme() =>
    await _authBase.AppendPathSegment("Ejendom")
      .GetJsonAsync<IEnumerable<EjendomResponse>>();

  //Lejemaal
  public async Task<LejemaalResponse?> GetLejemaal(Guid lejemaalId) =>
    await _authBase.AppendPathSegment("Lejemaal")
      .AppendPathSegment($"{lejemaalId}")
      .GetJsonAsync<LejemaalResponse>();

  public async Task<IEnumerable<LejemaalResponse>?> ListLejemaalOnEjendom(Guid ejendomId) =>
    await _authBase.AppendPathSegment("Ejendom")
      .AppendPathSegment($"{ejendomId}")
      .AppendPathSegment("Lejemaal")
      .GetJsonAsync<IEnumerable<LejemaalResponse>>();

  //Lejer
  public async Task<LejerResponse?> GetLejer(Guid lejerId) =>
    await _authBase.AppendPathSegment("Lejer")
      .AppendPathSegment($"{lejerId}")
      .GetJsonAsync<LejerResponse>();

  public async Task<LejerResponse?> UpdateLejer(Guid lejerId, LejerUpdateRequest request) =>
    await _authBase.AppendPathSegment("Lejer")
      .AppendPathSegment($"{lejerId}")
      .PutJsonAsync(request)
      .ReceiveJson<LejerResponse>();

  public async Task DeleteLejer(Guid lejerId) =>
    await _authBase.AppendPathSegment("Lejer")
      .AppendPathSegment($"{lejerId}")
      .DeleteAsync();

  public async Task<IEnumerable<LejerResponse>?> ListLejereOnLejemaal(Guid lejemaalId) =>
    await _authBase.AppendPathSegment("Lejemaal")
      .AppendPathSegment($"{lejemaalId}")
      .AppendPathSegment("Lejer")
      .GetJsonAsync<IEnumerable<LejerResponse>>();

  public async Task<LejerResponse?> CreateLejer(LejerCreateRequest request) =>
    await _authBase.AppendPathSegment("Lejer")
      .PostJsonAsync(request)
      .ReceiveJson<LejerResponse>();
}