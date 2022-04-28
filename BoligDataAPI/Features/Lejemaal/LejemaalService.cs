using BoligDataAPI.Features.Database;
using BoligDataAPI.Features.Results;
using FluentResults;

namespace BoligDataAPI.Features.Lejemaal;

public class LejemaalService : ILejemaalService
{
  private readonly DataContext _context;
  private readonly string _apiKey;

  public LejemaalService(DataContext dataContext, string apiKey)
  {
    _context = dataContext;
    _apiKey = apiKey;
  }

  public Result<Lejemaal> GetById(Guid id)
  {
    try
    {
      var result = _context.Lejemaal.Where(x => x.ApiKey == _apiKey).FirstOrDefault(x => x.Id == id);
      return result is null
        ? Result.Fail(new NotFoundError($"No Lejemaal found with id: {id}"))
        : Result.Ok(result);
    }
    catch (Exception e)
    {
      return Result.Fail(new ExceptionalError(e.Message, e));
    }
  }

  public Result<List<Lejemaal>> GetByEjendomId(Guid id)
  {
    try
    {
      var result = _context.Lejemaal.Where(x => x.ApiKey == _apiKey).Where(x => x.EjendomId == id).ToList();
      return result.Any() is false
        ? Result.Fail(new NotFoundError($"No Lejemaal found on Ejendom with id: {id}"))
        : Result.Ok(result);
    }
    catch (Exception e)
    {
      return Result.Fail(new ExceptionalError(e.Message, e));
    }
  }
}