namespace Application.UseCases.DeleteUser;

public interface IDeleteUserUseCase
{
    public Task Execute(string user);
}
