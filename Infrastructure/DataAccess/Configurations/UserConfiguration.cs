using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;

[ExcludeFromCodeCoverage]
public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ValidateNullArgument(nameof(builder));

        builder.ToTable(nameof(User));
        
        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Email).IsUnique();
        builder.HasIndex(user => user.Username).IsUnique();
        builder.Property(user => user.Email).HasMaxLength(200).IsRequired();
        builder.Property(user => user.Username).HasMaxLength(200).IsRequired();
        builder.Property(user => user.Password).HasMaxLength(300).IsRequired();
        builder.Property(user => user.Salt).HasMaxLength(200).IsRequired();
    }
}
