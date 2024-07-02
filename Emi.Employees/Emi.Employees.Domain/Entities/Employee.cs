using Emi.Employees.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Emi.Employees.Domain.Entities
{
    public class Employee : IdentityUser<int>
    {
        public int DepartmentId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public virtual Department Department { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<PositionHistory> PositionHistories { get; set; }
    }
}
