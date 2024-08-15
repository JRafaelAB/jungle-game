using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models.Requests;
using Domain.Repositories;
using Domain.Resources;
using Domain.UnitOfWork;

namespace Application.UseCases.CreateUser;

public class CreateUserUseCase(IUserRepository repository, IUnitOfWork unitOfWork) : ICreateUserUseCase
{
    public async Task<string?> Execute(UserRequest request)
    {
        await ValidateExistingUser(request);
        UserDto user = new(request);
        await repository.AddUser(user);
        await unitOfWork.Save();
        return user.Username;
    }

    private async Task ValidateExistingUser(UserRequest request)
    {
        var isValid = true;
        var errors = new List<string>();
        
        if (await repository.GetUserByUsername(request.Username) != null)
        {
            isValid = false;
            errors.Add(Messages.ConflictUsername);
        }
        if (await repository.GetUserByEmail(request.Email) != null)
        {
            isValid = false;
            errors.Add(Messages.ConflictEmail);
        }
        if (isValid) return;
        
        throw new LoginConflictException(errors);
    }
}