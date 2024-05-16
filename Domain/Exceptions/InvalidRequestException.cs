using Newtonsoft.Json;

namespace Domain.Exceptions;

[JsonObject(MemberSerialization.OptIn)]
public class InvalidRequestException(IEnumerable<string> errors) : Exception
{
    [JsonProperty]
    public IList<string> ErrorMessages { get; } = errors.ToList();
}
