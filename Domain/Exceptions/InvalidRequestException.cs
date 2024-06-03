using Microsoft.AspNetCore.Http;

namespace Domain.Exceptions;

public class InvalidRequestException(IEnumerable<string> errors) : ExceptionBase(errors, StatusCodes.Status400BadRequest);
