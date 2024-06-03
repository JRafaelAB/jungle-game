using Domain.Models.Requests;

namespace Application.UseCases.CreateUser;

public interface ICreateUserUseCase
{
    public Task<string?> Execute(UserRequest request);
}
