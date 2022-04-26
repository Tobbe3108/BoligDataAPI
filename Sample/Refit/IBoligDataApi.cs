using Refit;
using Sample.Shared;

namespace Sample.Refit;

public interface IBoligDataApi
{
  //Ejendom
  [Get("/Ejendom/{id}")] Task<EjendomResponse?> GetEjendom([AliasAs("id")] Guid ejendomId);
  [Get("/Ejendom")] Task<IEnumerable<EjendomResponse>?> ListEjendomme();

  //Lejemaal
  [Get("/Lejemaal/{id}")] Task<LejemaalResponse?> GetLejemaal([AliasAs("id")] Guid lejemaalId);
  [Get("/Ejendom/{id}/Lejemaal")] Task<IEnumerable<LejemaalResponse>?> ListLejemaalOnEjendom([AliasAs("id")] Guid ejendomId);

  //Lejer
  [Get("/Lejer/{id}")] Task<LejerResponse?> GetLejer([AliasAs("id")] Guid lejerId);
  [Put("/Lejer/{id}")] Task<LejerResponse?> UpdateLejer([AliasAs("id")] Guid lejerId, LejerUpdateRequest request);
  [Delete("/Lejer/{id}")] Task DeleteLejer([AliasAs("id")] Guid lejerId);
  [Get("/Lejemaal/{id}/Lejer")] Task<IEnumerable<LejerResponse>?> ListLejereOnLejemaal([AliasAs("id")] Guid lejemaalId);
  [Post("/Lejer")] Task<LejerResponse?> CreateLejer(LejerCreateRequest request);
}