using Domain.Exceptions;
using Domain.Models.Responses;
using Domain.Repositories;
using Domain.Resources;

namespace Application.UseCases.GetUser;

public class GetUserUseCase(IUserRepository repository) : IGetUserUseCase
{
    public async Task<GetUserResponse> Execute(string user)
    {
        var userDto = await repository.GetUserByUsernameOrEmail(user);
        
        if (userDto == null) throw new UserNotFoundException(Messages.UserNotFound);

        return new GetUserResponse(userDto);
    }
}
