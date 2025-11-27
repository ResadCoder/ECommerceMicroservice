using Shared.Enums;

namespace Audit.Models;

public class Audit
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = null!;
    public string Action { get; set; } = null!;
    public DateTime Timestamp  => DateTime.UtcNow;
    public string? Details { get; set; } 
    public LogStatus Status { get; set; }
}