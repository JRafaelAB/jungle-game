using Domain.Models.Requests;

namespace Application.UseCases.UpdateUser;
public interface IUpdateUserUseCase
{
    public Task Execute(UserRequest request, string user);
}
