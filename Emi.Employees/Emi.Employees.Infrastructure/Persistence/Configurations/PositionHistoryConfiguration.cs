using Emi.Employees.Domain.Entities;
using Emi.Employees.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emi.Employees.Infrastructure.Persistence.Configurations;

internal class PositionHistoryConfiguration : IEntityTypeConfiguration<PositionHistory>
{ 
    public void Configure(EntityTypeBuilder<PositionHistory> builder)
    {
        builder.ToTable(TableNames.PositionHistory);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.EmployeeId).IsRequired();
        builder.Property(p => p.DepartmentId).IsRequired();
        builder.Property(p => p.ProjectId).IsRequired();
        builder.Property(p => p.PositionId).IsRequired();
        builder.Property(p => p.StartDate).IsRequired();
        builder.Property(p => p.EndDate).IsRequired();

        builder.HasOne(e => e.Employee).WithMany(e => e.PositionHistories)
            .HasForeignKey(b => b.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(e => e.Department).WithMany(e => e.PositionHistories)
            .HasForeignKey(b => b.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(e => e.Project).WithMany(e => e.PositionHistories)
            .HasForeignKey(b => b.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
