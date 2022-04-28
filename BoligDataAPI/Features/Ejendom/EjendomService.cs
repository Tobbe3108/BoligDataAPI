using BoligDataAPI.Features.Database;
using BoligDataAPI.Features.Results;
using FluentResults;

namespace BoligDataAPI.Features.Ejendom;

public class EjendomService : IEjendomService
{
  private readonly DataContext _context;
  private readonly string _apiKey;

  public EjendomService(DataContext context, string apiKey)
  {
    _context = context;
    _apiKey = apiKey;
  }

  public Result<Ejendom> GetById(Guid id)
  {
    try
    {
      var result = _context.Ejendomme.Where(x => x.ApiKey == _apiKey).FirstOrDefault(x => x.Id == id);
      return result is null
        ? Result.Fail(new NotFoundError($"No Ejendom found with id: {id}"))
        : Result.Ok(result);
    }
    catch (Exception e)
    {
      return Result.Fail(new ExceptionalError(e.Message, e));
    }
  }

  public Result<IEnumerable<Ejendom>> GetAll()
  {
    try
    {
      var data = _context.Ejendomme.Where(x => x.ApiKey == _apiKey).AsEnumerable();
      return Result.Ok(data);
    }
    catch (Exception e)
    {
      return Result.Fail(new ExceptionalError(e.Message, e));
    }
  }
}