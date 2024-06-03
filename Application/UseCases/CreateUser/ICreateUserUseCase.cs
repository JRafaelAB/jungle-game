using Domain.Models.Requests;

namespace Application.UseCases.CreateUser;

public interface ICreateUserUseCase
{
<<<<<<< HEAD
    public Task Execute(UserRequest request);
=======
    public Task<string?> Execute(CreateUserRequest request);
>>>>>>> main
}
