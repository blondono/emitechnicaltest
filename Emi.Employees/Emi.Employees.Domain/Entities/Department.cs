namespace Emi.Employees.Domain.Entities
{
    public class Department : MainTable
    {
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<PositionHistory> PositionHistories { get; set; }
    }
}
