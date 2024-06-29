﻿using Emi.Employees.Domain.Entities;
using Emi.Employees.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emi.Employees.Infrastructure.Persistence.Configurations;

internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable(TableNames.Project);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired();

        builder.HasMany<Employee>("Project").WithOne().HasForeignKey(b => b.CurrentProjectId)
            .HasConstraintName("FK_Employee_ProjectId");

        builder.HasMany<PositionHistory>("Project").WithOne().HasForeignKey(b => b.ProjectId)
            .HasConstraintName("FK_PositionHistory_ProjectId");

    }
}
