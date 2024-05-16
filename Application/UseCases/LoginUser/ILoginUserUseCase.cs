using Domain.Models.Requests;

namespace Application.UseCases.LoginUser;

public interface ILoginUserUseCase
{
    Task<ulong> Execute(LoginUserRequest request);
}
