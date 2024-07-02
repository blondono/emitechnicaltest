using Emi.Employees.Domain.Entities;
using Emi.Employees.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emi.Employees.Infrastructure.Persistence.Configurations;

internal class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable(TableNames.Position);

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired();
    }
}
