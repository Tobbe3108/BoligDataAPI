using Sample.Refit;
using Sample.Shared;

namespace Sample.TypedHttpClient;

public class BoligDataApi : IBoligDataApi
{
  private readonly HttpClient _httpClient;

  public BoligDataApi(HttpClient httpClient)
  {
    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri(Constants.BoligDataApiUrl);
    _httpClient.DefaultRequestHeaders.Add("ApiKey", Constants.BoligDataApiKey);
  }

  //Ejendom
  public async Task<EjendomResponse?> GetEjendom(Guid ejendomId) =>
    await _httpClient.GetFromJsonAsync<EjendomResponse>($"Ejendom/{ejendomId}");

  public async Task<IEnumerable<EjendomResponse>?> ListEjendomme() =>
    await _httpClient.GetFromJsonAsync<IEnumerable<EjendomResponse>>("Ejendom");

  //Lejemaal
  public async Task<LejemaalResponse?> GetLejemaal(Guid lejemaalId) =>
    await _httpClient.GetFromJsonAsync<LejemaalResponse>($"Lejemaal/{lejemaalId}");

  public async Task<IEnumerable<LejemaalResponse>?> ListLejemaalOnEjendom(Guid ejendomId) =>
    await _httpClient.GetFromJsonAsync<IEnumerable<LejemaalResponse>>($"Ejendom/{ejendomId}/Lejemaal");

  //Lejer
  public async Task<LejerResponse?> GetLejer(Guid lejerId) =>
    await _httpClient.GetFromJsonAsync<LejerResponse>($"Lejer/{lejerId}");

  public async Task<LejerResponse?> UpdateLejer(Guid lejerId, LejerUpdateRequest request) =>
    await _httpClient.PutAsJsonAsync($"/Lejer/{lejerId}", request).Result.Content.ReadFromJsonAsync<LejerResponse>();

  public async Task DeleteLejer(Guid lejerId) => await _httpClient.DeleteAsync($"Lejer/{lejerId}");

  public async Task<IEnumerable<LejerResponse>?> ListLejereOnLejemaal(Guid lejemaalId) =>
    await _httpClient.GetFromJsonAsync<IEnumerable<LejerResponse>>($"Lejemaal/{lejemaalId}/Lejer");

  public async Task<LejerResponse?> CreateLejer(LejerCreateRequest request) =>
    await _httpClient.PostAsJsonAsync("/Lejer", request).Result.Content.ReadFromJsonAsync<LejerResponse>();
}