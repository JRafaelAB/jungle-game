using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Contexts;
using Moq;
using Xunit;

namespace UnitTests.DataAccess;

public class UnitOfWorkTest
{
    private readonly Mock<JungleContext> _context;
    private readonly UnitOfWork _unitOfWork;
        
    public UnitOfWorkTest()
    {
        this._context = new Mock<JungleContext>();
        this._unitOfWork = new UnitOfWork(this._context.Object);
    }
        
    [Fact]
    public async Task Test_Save()
    {
        await this._unitOfWork.Save();
        this._context.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()));
    }
        
    [Fact]
    public void Test_Dispose()
    {
        this._unitOfWork.Dispose();
        this._context.Verify(context => context.Dispose(), Times.Exactly(1));
        this._unitOfWork.Dispose();
        this._context.Verify(context => context.Dispose(), Times.Exactly(1));
    }
}
