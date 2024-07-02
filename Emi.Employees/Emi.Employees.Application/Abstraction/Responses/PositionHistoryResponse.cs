namespace Emi.Employees.Application.Abstraction.Responses;

public class PositionHistoryResponse
{
    public string Name { get; set; }
    public List<History> History { get; set; }
}
public class History
{
    public string Department { get; set; }
    public string Project { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
