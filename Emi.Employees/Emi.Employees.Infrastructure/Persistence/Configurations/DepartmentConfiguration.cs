﻿using Emi.Employees.Domain.Entities;
using Emi.Employees.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emi.Employees.Infrastructure.Persistence.Configurations;

internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable(TableNames.Department);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired();


    }
}
