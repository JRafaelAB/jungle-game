using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Contexts;

[ExcludeFromCodeCoverage]
public class JungleContext : DbContext
{
    public virtual DbSet<User> Users { get; init; } = null!;
    public virtual DbSet<LotteryResults> LotteryResults { get; init; } = null!;
    
    public JungleContext()
    {
    }

    public JungleContext(DbContextOptions<JungleContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ValidateNullArgument(nameof(modelBuilder));

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JungleContext).Assembly);
    }
}
