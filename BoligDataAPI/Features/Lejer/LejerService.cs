using BoligDataAPI.Features.Database;
using BoligDataAPI.Features.Lejemaal;
using BoligDataAPI.Features.Results;
using FluentResults;

namespace BoligDataAPI.Features.Lejer;

public class LejerService : ILejerService
{
  private readonly DataContext _context;
  private readonly string _apiKey;
  private readonly ILejemaalService _lejemaalService;

  public LejerService(DataContext context, string apiKey, ILejemaalService.Factory lejemaalServiceFactory)
  {
    _context = context;
    _apiKey = apiKey;
    _lejemaalService = lejemaalServiceFactory(apiKey);
  }

  public Result<Lejer> GetById(Guid id)
  {
    try
    {
      var result = _context.Lejere.Where(x => x.ApiKey == _apiKey).FirstOrDefault(x => x.Id == id);
      return result is null
        ? Result.Fail(new NotFoundError($"No Lejer found with id: {id}"))
        : Result.Ok(result);
    }
    catch (Exception e)
    {
      return Result.Fail(new ExceptionalError(e.Message, e));
    }
  }

  public Result<List<Lejer>> GetByLejemaalId(Guid id)
  {
    try
    {
      var result = _context.Lejere.Where(x => x.ApiKey == _apiKey).Where(x => x.LejemaalId == id).ToList();
      return result.Any() is false
        ? Result.Fail(new NotFoundError($"No Lejer found on Lejemaal with id: {id}"))
        : Result.Ok(result);
    }
    catch (Exception e)
    {
      return Result.Fail(new ExceptionalError(e.Message, e));
    }
  }

  public Result<Lejer> Create(Lejer data)
  {
    try
    {
      var result = _lejemaalService.GetById(data.LejemaalId);
      return result.IsFailed
        ? result.ToResult()
        : Result.Try(() =>
        {
          _context.Lejere.Add(data);
          _context.SaveChanges();
          return data;
        });
    }
    catch (Exception e)
    {
      return Result.Fail(new ExceptionalError(e.Message, e));
    }
  }

  public Result<Lejer> Update(Lejer data)
  {
    try
    {
      var result = GetById(data.Id);
      return result.IsFailed
        ? result.ToResult()
        : Result.Try(() =>
        {
          _context.Lejere.Remove(result.Value);
          data = data with { LejemaalId = result.Value.LejemaalId };
          _context.Lejere.Add(data);
          _context.SaveChanges();
          return data;
        });
    }
    catch (Exception e)
    {
      return Result.Fail(new ExceptionalError(e.Message, e));
    }
  }

  public Result Delete(Guid id)
  {
    try
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
    catch (Exception e)
    {
      return Result.Fail(new ExceptionalError(e.Message, e));
    }
  }
}