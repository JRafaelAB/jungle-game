using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models.Requests;
using Domain.Repositories;
using Domain.Resources;
using Domain.UnitOfWork;

namespace Application.UseCases.UpdateUser;

public class UpdateUserUseCase(IUserRepository repository, IUnitOfWork unitOfWork) : IUpdateUserUseCase
{
    public async Task Execute(UserUpdateRequest request, string user)
    {
        await ValidateExistingUser(request, user);

        await Save(repository, unitOfWork, request, user);        
    }

    static async Task Save(IUserRepository repository, IUnitOfWork unitOfWork, UserUpdateRequest request, string user)
    {
        if (!await repository.UpdateUser(new UserDto(request), user))
        {
            throw new UserNotFoundException(Messages.UserNotFound);
        }
        await unitOfWork.Save();
    }
    private async Task ValidateExistingUser(UserUpdateRequest request, string user)
    {
        var isValid = true;
        var errors = new List<string>();

        

        if (request.Username != null && IsExistingUser(await repository.GetUserByUsername(request.Username), user))
        {
            isValid = false;
            errors.Add(Messages.ConflictUsername);
        }
        if (request.Email != null && IsExistingUser(await repository.GetUserByEmail(request.Email), user))
        {
            isValid = false;
            errors.Add(Messages.ConflictEmail);
        }
        if (isValid) return;

        throw new LoginConflictException(errors);
    }
    private bool IsExistingUser(UserDto? userDto, string user)
    {
        if (userDto == null) return false;
        return !userDto.ValidateUser(user);
    }
}