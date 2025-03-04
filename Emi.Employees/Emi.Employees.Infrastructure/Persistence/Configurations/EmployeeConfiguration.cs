﻿using Emi.Employees.Domain.Entities;
using Emi.Employees.Domain.ValueObjects;
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
        builder.Property(t => t.DepartmentId).IsRequired();
        builder.Property(t => t.ProjectId).IsRequired();
        builder.Property(t => t.PositionId).IsRequired();
        builder.Property(t => t.Salary).IsRequired();

        builder.HasOne(e => e.Department).WithMany(e => e.Employees)
            .HasForeignKey(b => b.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Project).WithMany(e => e.Employees)
            .HasForeignKey(b => b.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Position)
                  .WithMany()
                  .HasForeignKey(e => e.PositionId)
                  .IsRequired();
    }
}
