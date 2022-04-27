using FluentResults;

namespace BoligDataAPI.Features.Ejendom;

public interface IEjendomService
{
  public delegate IEjendomService Factory(string apiKey);
  Result<Ejendom> GetById(Guid id);
  Result<IEnumerable<Ejendom>> GetAll();
}