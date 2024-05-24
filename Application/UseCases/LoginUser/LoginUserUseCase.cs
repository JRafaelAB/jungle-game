using Domain.Exceptions;
using Domain.Models.Requests;
using Domain.Repositories;
using Domain.Resources;

namespace Application.UseCases.LoginUser;

public class LoginUserUseCase(IUserRepository repository) : ILoginUserUseCase
{
    public async Task<ulong> Execute(LoginUserRequest request)
    {
        var user = await repository.GetUserByUsernameOrEmail(request.UsernameOrEmail);
        
        if (user != null)
        {
            if (user.ValidatePassword(request.Password)) return 1;
        }
        
        throw new UserNotFoundException(Messages.InvalidUser);
    }
}

