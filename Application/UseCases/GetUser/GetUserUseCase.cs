using Domain.Exceptions;
using Domain.Models.Responses;
using Domain.Repositories;
using Domain.Resources;

namespace Application.UseCases.GetUser;

public class GetUserUseCase(IUserRepository repository) : IGetUserUseCase
{
    public async Task<GetUserResponse> Execute(ulong userId)
    {
        var user = await repository.GetUser(userId);
        
        if (user == null) throw new UserNotFoundException(Messages.UserNotFound);

        return new GetUserResponse(user);
    }
}
