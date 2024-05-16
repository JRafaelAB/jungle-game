using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models.Requests;
using Domain.Repositories;
using Domain.Resources;
using Domain.UnitOfWork;

namespace Application.UseCases.CreateUser;

public class CreateUserUseCase(IUserRepository repository, IUnitOfWork unitOfWork) : ICreateUserUseCase
{
    public async Task Execute(CreateUserRequest request)
    {
        await ValidateExistingUser(request);
        UserDto user = new(request);
        await repository.AddUser(user);
        await unitOfWork.Save();
    }

    private async Task ValidateExistingUser(CreateUserRequest request)
    {
        var isValid = true;
        var errors = new List<string>();
        
        var taskGetUserByUsername = repository.GetUserByUsername(request.Username);
        var taskGetUserByEmail = repository.GetUserByEmail(request.Email);

        await Task.WhenAll(taskGetUserByUsername, taskGetUserByEmail);
        
        if (taskGetUserByUsername.Result != null)
        {
            isValid = false;
            errors.Add(Messages.ConflictUsername);
        }
        if (taskGetUserByEmail.Result != null)
        {
            isValid = false;
            errors.Add(Messages.ConflictEmail);
        }
        if (isValid) return;
        
        throw new LoginConflictException(errors);
    }
}

