using FluentResults;

namespace BoligDataAPI.Features.Lejer;

public class LejerService
{
  private readonly List<Lejer> _data;

  public LejerService(List<Lejer> data)
  {
    _data = data;
  }

  public Result<Lejer> GetById(Guid id)
  {
    var result = _data.FirstOrDefault(x => x.Id == id);
    return result is null
      ? Result.Fail<Lejer>($"No Lejer found with id: {id}")
      : Result.Ok(result);
  }

  public Result<List<Lejer>> GetByLejemaalId(Guid id)
  {
    var result = _data.Where(x => x.LejemaalId == id).ToList();
    return result.Any() is false
      ? Result.Fail<List<Lejer>>($"No Lejer found on Lejemaal with id: {id}")
      : Result.Ok(result);
  }

  public Result<Lejer> Create(Lejer data)
  {
    var result = GetById(data.Id);
    return result.IsSuccess
      ? result.ToResult()
      : Result.Try(() =>
      {
        _data.Add(data);
        return data;
      });
  }

  public Result<Lejer> Update(Lejer data)
  {
    var result = GetById(data.Id);
    return result.IsFailed
      ? result.ToResult()
      : Result.Try(() =>
      {
        _data.Remove(result.Value);
        _data.Add(data with { LejemaalId = result.Value.LejemaalId });
        return data;
      });
  }

  public Result Delete(Guid id)
  {
    var result = GetById(id);
    return result.IsFailed
      ? result.ToResult()
      : Result.Try(() =>
      {
        _data.Remove(result.Value);
        ;
      });
  }
}