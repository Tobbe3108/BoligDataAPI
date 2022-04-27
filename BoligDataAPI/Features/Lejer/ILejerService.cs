using FluentResults;

namespace BoligDataAPI.Features.Lejer;

public interface ILejerService
{
  public delegate ILejerService Factory(string apiKey);
  Result<Lejer> GetById(Guid id);
  Result<List<Lejer>> GetByLejemaalId(Guid id);
  Result<Lejer> Create(Lejer data);
  Result<Lejer> Update(Lejer data);
  Result Delete(Guid id);
}