namespace Emi.Employees.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int CurrentDepartmentId { get; set; }
        public int CurrentProjectId { get; set; }
        public string Name { get; set; }
        public string CurrentPosition { get; set; }
        public decimal Salary { get; set; }
        public virtual Department Department { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<PositionHistory> PositionHistories { get; set; }
    }
}
