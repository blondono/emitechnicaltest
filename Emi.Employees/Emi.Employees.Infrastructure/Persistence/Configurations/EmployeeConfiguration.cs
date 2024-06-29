using Emi.Employees.Domain.Entities;
using Emi.Employees.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emi.Employees.Infrastructure.Persistence.Configurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(TableNames.Employee);

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name).IsRequired();
        builder.Property(t => t.CurrentPosition).IsRequired();
        builder.Property(t => t.CurrentDepartmentId).IsRequired();
        builder.Property(t => t.Salary).IsRequired();

        builder.HasMany<PositionHistory>("Employees").WithOne().HasForeignKey(b => b.EmployeeId)
            .HasConstraintName("FK_PositionHistory_EmployeeId");

        builder.HasMany(a => a.PositionHistories).WithOne(a => a.Employee).OnDelete(DeleteBehavior.Cascade);
    }
}
