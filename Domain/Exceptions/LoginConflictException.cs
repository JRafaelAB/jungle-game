using Microsoft.AspNetCore.Http;

namespace Domain.Exceptions;

public class LoginConflictException(IEnumerable<string> errors) : ExceptionBase(errors, StatusCodes.Status409Conflict);
