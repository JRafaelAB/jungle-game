using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models.Requests;
using Domain.Repositories;
using Domain.Resources;
using Domain.UnitOfWork;

namespace Application.UseCases.UpdateUser;

public class UpdateUserUseCase(IUserRepository repository, IUnitOfWork unitOfWork) : IUpdateUserUseCase
{
    public async Task Execute(UserRequest request, string user)
    {
        await ValidateExistingUser(request, user);
        if (!await repository.UpdateUser(new UserDto(request), user))
        {
            throw new UserNotFoundException(Messages.UserNotFound);
        }
        await unitOfWork.Save();
    }
    private async Task ValidateExistingUser(UserRequest request, string user)
    {
        var isValid = true;
        var errors = new List<string>();

        if (IsExistingUser(await repository.GetUserByUsername(request.Username), user))
        {
            isValid = false;
            errors.Add(Messages.ConflictUsername);
        }
        if (IsExistingUser(await repository.GetUserByEmail(request.Email), user))
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