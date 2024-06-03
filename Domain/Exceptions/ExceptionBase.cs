using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Domain.Exceptions;

[JsonObject(MemberSerialization.OptIn)]
public class ExceptionBase(IEnumerable<string> errors, int? StatusCode = null) : Exception
{
    public ExceptionBase(string message, int? StatusCode = null) : this(new []{message}, StatusCode) { }

    [JsonProperty]
    public int HttpStatus { get; } = StatusCode ?? StatusCodes.Status500InternalServerError;
    
    [JsonProperty]
    public IList<string> ErrorMessages { get; } = errors.ToList();
}
