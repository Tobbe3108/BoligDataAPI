using FluentResults;

namespace BoligDataAPI.Features.Lejemaal;

public interface ILejemaalService
{
  public delegate ILejemaalService Factory(string apiKey);
  Result<Lejemaal> GetById(Guid id);
  Result<List<Lejemaal>> GetByEjendomId(Guid id);
}