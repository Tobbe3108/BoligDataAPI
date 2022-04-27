using BoligDataAPI.Features.Database;
using FluentResults;

namespace BoligDataAPI.Features.Ejendom;

public class EjendomService
{
  private readonly DataContext _context;
  private readonly string _apiKey;

  public delegate EjendomService Factory(string apiKey);

  public EjendomService(DataContext context, string apiKey)
  {
    _context = context;
    _apiKey = apiKey;
  }

  public Result<Ejendom> GetById(Guid id)
  {
    var result = _context.Ejendomme.Where(x => x.ApiKey == _apiKey).FirstOrDefault(x => x.Id == id);
    return result is null
      ? Result.Fail<Ejendom>($"No Ejendom found with id: {id}")
      : Result.Ok(result);
  }

  public Result<IEnumerable<Ejendom>> GetAll() => Result.Ok(_context.Ejendomme.Where(x => x.ApiKey == _apiKey).AsEnumerable());
}