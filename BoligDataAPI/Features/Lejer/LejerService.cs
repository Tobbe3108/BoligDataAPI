using BoligDataAPI.Features.Database;
using FluentResults;

namespace BoligDataAPI.Features.Lejer;

public class LejerService
{
  private readonly DataContext _context;
  private readonly string _apiKey;

  public delegate LejerService Factory(string apiKey);
  public LejerService(DataContext context, string apiKey)
  {
    _context = context;
    _apiKey = apiKey;
  }

  public Result<Database.Lejer> GetById(Guid id)
  {
    var result = _context.Lejere.Where(x => x.ApiKey == _apiKey).FirstOrDefault(x => x.Id == id);
    return result is null
      ? Result.Fail<Database.Lejer>($"No Lejer found with id: {id}")
      : Result.Ok(result);
  }

  public Result<List<Database.Lejer>> GetByLejemaalId(Guid id)
  {
    var result = _context.Lejere.Where(x => x.ApiKey == _apiKey).Where(x => x.LejemaalId == id).ToList();
    return result.Any() is false
      ? Result.Fail<List<Database.Lejer>>($"No Lejer found on Lejemaal with id: {id}")
      : Result.Ok(result);
  }

  public Result<Database.Lejer> Create(Database.Lejer data)
  {
    var result = GetById(data.Id);
    return result.IsSuccess
      ? result.ToResult()
      : Result.Try(() =>
      {
        _context.Lejere.Add(data);
        _context.SaveChanges();
        return data;
      });
  }

  public Result<Database.Lejer> Update(Database.Lejer data)
  {
    var result = GetById(data.Id);
    return result.IsFailed
      ? result.ToResult()
      : Result.Try(() =>
      {
        _context.Lejere.Remove(result.Value);
        _context.Lejere.Add(data with { LejemaalId = result.Value.LejemaalId });
        _context.SaveChanges();
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
        _context.Lejere.Remove(result.Value);
        _context.SaveChanges();
      });
  }
}