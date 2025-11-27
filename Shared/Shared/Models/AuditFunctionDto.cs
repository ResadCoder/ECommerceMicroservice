using Shared.Enums;

namespace Shared.Models;

public record AuditFunctionDto(
    string ServiceName,
    string Action,
    string? Details,
    LogStatus Status,
    DateTime Timestamp
);
