using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models.Requests;
using Domain.Repositories;
using Domain.Resources;
using Domain.UnitOfWork;

namespace Application.UseCases.CreateUser;

public class CreateUserUseCase(IUserRepository repository, IUnitOfWork unitOfWork) : ICreateUserUseCase
{
    public async Task<ulong?> Execute(CreateUserRequest request)
    {
        await ValidateExistingUser(request);
        UserDto user = new(request);
        var id = await repository.AddUser(user);
        await unitOfWork.Save();
        return id;
    }

    private async Task ValidateExistingUser(CreateUserRequest request)
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

