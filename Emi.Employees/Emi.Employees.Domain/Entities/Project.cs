namespace Emi.Employees.Domain.Entities
{
    public class Project: MainTable
    {
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<PositionHistory> PositionHistories { get; set; }
    }
}
