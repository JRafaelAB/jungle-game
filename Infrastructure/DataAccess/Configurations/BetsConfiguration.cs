using Domain.Entities;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.DataAccess.Configurations;

[ExcludeFromCodeCoverage]
public sealed class BetsConfiguration : IEntityTypeConfiguration<Bets>
{
    public void Configure(EntityTypeBuilder<Bets> builder)
    {
        builder.ValidateNullArgument(nameof(builder));

        builder.ToTable(nameof(Bets));

        builder.HasKey(bet => bet.Id);
        builder.Property(bet => bet.UserId);
        builder.HasOne<User>().WithMany().HasForeignKey(bet => bet.UserId);
        builder.HasIndex(bet => bet.UserId);
        builder.Property(bet => bet.Date).IsRequired();
        builder.Property(bet => bet.Value).HasColumnType("decimal(20,2)").IsRequired();
        builder.Property(bet => bet.Type).IsRequired();
        builder.Property(bet => bet.Data).IsRequired();
        builder.Property(bet => bet.LotteryNumber).IsRequired();
    }
}