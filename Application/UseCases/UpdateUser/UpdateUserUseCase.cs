using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models.Requests;
using Domain.Repositories;
using Domain.Resources;
using Domain.UnitOfWork;

namespace Application.UseCases.UpdateUser;

public class UpdateUserUseCase(IUserRepository repository, IUnitOfWork unitOfWork) : IUpdateUserUseCase
{
    public async Task Execute(UserRequest request, ulong userId)
    {
        if (!await repository.UpdateUser(new UserDto(request, userId)))
        {
            throw new UserNotFoundException(Messages.UserNotFound);
        }
        await unitOfWork.Save();
    }
}