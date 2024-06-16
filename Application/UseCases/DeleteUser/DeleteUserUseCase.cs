using Domain.Exceptions;
using Domain.Repositories;
using Domain.Resources;
using Domain.UnitOfWork;

namespace Application.UseCases.DeleteUser;

public class DeleteUserUseCase(IUserRepository repository, IUnitOfWork unitOfWork) : IDeleteUserUseCase
{
    public async Task Execute(string user)
    {
        if(await repository.DeleteUser(user) == false)
        {
            throw new UserNotFoundException(Messages.UserNotFound);
        }
        await unitOfWork.Save();
    }
}
