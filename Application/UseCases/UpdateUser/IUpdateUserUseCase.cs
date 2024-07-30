using Domain.Models.Requests;

namespace Application.UseCases.UpdateUser;
public interface IUpdateUserUseCase
{
    public Task Execute(UserUpdateRequest request, string user);
}
