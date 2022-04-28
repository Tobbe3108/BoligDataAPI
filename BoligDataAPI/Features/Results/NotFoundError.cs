using FluentResults;

namespace BoligDataAPI.Features.Results;

public class NotFoundError : Error
{
  public NotFoundError(string message) : base(message)
  {
  }
}