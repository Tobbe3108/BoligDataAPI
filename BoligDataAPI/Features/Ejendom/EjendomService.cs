using FluentResults;

namespace BoligDataAPI.Features.Ejendom;

public class EjendomService
{
  private readonly List<Ejendom> _data;

  public EjendomService(List<Ejendom> data)
  {
    _data = data;
  }

  public Result<Ejendom> GetById(Guid id)
  {
    var result = _data.FirstOrDefault(x => x.Id == id);
    return result is null
      ? Result.Fail<Ejendom>($"No Ejendom found with id: {id}")
      : Result.Ok(result);
  }

  public Result<List<Ejendom>> GetAll() => Result.Ok(_data);
}