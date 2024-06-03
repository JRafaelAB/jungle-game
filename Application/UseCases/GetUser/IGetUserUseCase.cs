using Domain.Models.Responses;

namespace Application.UseCases.GetUser;

public interface IGetUserUseCase
{
    Task<GetUserResponse> Execute(string user);
}
