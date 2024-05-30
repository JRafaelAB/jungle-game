using Domain.Models.Requests;

namespace Application.UseCases.CreateUser;

public interface ICreateUserUseCase
{
    public Task<ulong?> Execute(CreateUserRequest request);
}
