using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Emi.Employees.Domain.ValueObjects;

namespace Emi.Employees.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public EmployeePosition CurrentPosition { get; set; }
        public decimal Salary { get; set; }
        public virtual Department Department { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<PositionHistory> PositionHistories { get; set; }
    }
}
