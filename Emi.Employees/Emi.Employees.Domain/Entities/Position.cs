using Microsoft.AspNetCore.Identity;

namespace Emi.Employees.Domain.Entities;

public class Position : IdentityRole<int>
{
    public virtual ICollection<PositionHistory> PositionHistories { get; set; }
}
