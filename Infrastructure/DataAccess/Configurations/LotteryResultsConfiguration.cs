using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;

[ExcludeFromCodeCoverage]
public class LotteryResultsConfiguration : IEntityTypeConfiguration<LotteryResults>
{
    public void Configure(EntityTypeBuilder<LotteryResults> builder)
    {
        builder.ValidateNullArgument(nameof(builder));

        builder.ToTable(nameof(LotteryResults));
        
        builder.HasKey(lottery => lottery.Id);
        builder.Property(lottery => lottery.NumbersPerLottery).IsRequired();
        builder.Property(lottery => lottery.Lottery1).HasMaxLength(100).IsRequired();
        builder.Property(lottery => lottery.Lottery2).HasMaxLength(100).IsRequired();
        builder.Property(lottery => lottery.Lottery3).HasMaxLength(100).IsRequired();
        builder.Property(lottery => lottery.Lottery4).HasMaxLength(100).IsRequired();
    }
}
