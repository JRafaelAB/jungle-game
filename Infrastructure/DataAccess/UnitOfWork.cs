using Domain.UnitOfWork;
using Infrastructure.DataAccess.Contexts;

namespace Infrastructure.DataAccess;

public class UnitOfWork(JungleContext jungleContext) : IUnitOfWork, IDisposable
{
    private bool _disposed;

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<int> Save()
    {
        int affectedRows = await jungleContext
            .SaveChangesAsync();
        return affectedRows;
    }
        
    private void Dispose(bool disposing)
    {
        if (!this._disposed && disposing)
        {
            jungleContext.Dispose();
        }

        this._disposed = true;
    }
}
