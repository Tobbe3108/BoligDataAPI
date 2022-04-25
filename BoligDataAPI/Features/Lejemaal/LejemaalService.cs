using FluentResults;

namespace BoligDataAPI.Features.Lejemaal;

public class LejemaalService
{
  private readonly IEnumerable<Lejemaal> _data;

  public LejemaalService(IEnumerable<Lejemaal> data)
  {
    _data = data;
  }

  public Result<Lejemaal> GetById(Guid id)
  {
    var result = _data.FirstOrDefault(x => x.Id == id);
    return result is null
      ? Result.Fail<Lejemaal>($"No Lejemaal found with id: {id}")
      : Result.Ok(result);
  }

  public Result<List<Lejemaal>> GetByEjendomId(Guid id)
  {
    var result = _data.Where(x => x.EjendomId == id).ToList();
    return result.Any() is false
      ? Result.Fail<List<Lejemaal>>($"No Lejemaal found on Ejendom with id: {id}")
      : Result.Ok(result);
  }
}