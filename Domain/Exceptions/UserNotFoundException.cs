using Microsoft.AspNetCore.Http;

namespace Domain.Exceptions;

public class UserNotFoundException(string message) : ExceptionBase(message, StatusCodes.Status404NotFound);
