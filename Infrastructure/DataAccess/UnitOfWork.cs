using Domain.UnitOfWork;
using Infrastructure.DataAccess.Contexts;

namespace Infrastructure.DataAccess;

public class UnitOfWork(UsersContext usersContext) : IUnitOfWork, IDisposable
{
    private bool _disposed;

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<int> Save()
    {
        int affectedRows = await usersContext
            .SaveChangesAsync();
        return affectedRows;
    }
        
    private void Dispose(bool disposing)
    {
        if (!this._disposed && disposing)
        {
            usersContext.Dispose();
        }

        this._disposed = true;
    }
}
