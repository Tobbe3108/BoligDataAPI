using BoligDataAPI.Features.Database;
using FluentResults;

namespace BoligDataAPI.Features.Lejemaal;

public class LejemaalService
{
  private readonly DataContext _context;
  private readonly string _apiKey;

  public delegate LejemaalService Factory(string apiKey);

  public LejemaalService(DataContext dataContext, string apiKey)
  {
    _context = dataContext;
    _apiKey = apiKey;
  }

  public Result<Database.Lejemaal> GetById(Guid id)
  {
    var result = _context.Lejemaal.Where(x => x.ApiKey == _apiKey).FirstOrDefault(x => x.Id == id);
    return result is null
      ? Result.Fail<Database.Lejemaal>($"No Lejemaal found with id: {id}")
      : Result.Ok(result);
  }

  public Result<List<Database.Lejemaal>> GetByEjendomId(Guid id)
  {
    var result = _context.Lejemaal.Where(x => x.ApiKey == _apiKey).Where(x => x.EjendomId == id).ToList();
    return result.Any() is false
      ? Result.Fail<List<Database.Lejemaal>>($"No Lejemaal found on Ejendom with id: {id}")
      : Result.Ok(result);
  }
}