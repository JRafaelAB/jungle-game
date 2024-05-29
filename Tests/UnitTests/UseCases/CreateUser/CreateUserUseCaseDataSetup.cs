using Domain.Models.Requests;

namespace UnitTests.UseCases.CreateUser;

public static class CreateUserUseCaseDataSetup
{
    public static readonly UserRequest validUser = new("usuario", "email", "senha");
}
