using System.Text.Json;
using Audit.Context;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Shared.Models;

// or wherever your Audit entity is

namespace Audit;

public class AuditTrigger
{
    private readonly ILogger<AuditTrigger> _logger;
    private readonly AppDbContext _db;

    public AuditTrigger(ILogger<AuditTrigger> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    
    [Function("AuditTrigger")]
    public async Task Run(
        [ServiceBusTrigger("audit-queue", Connection = "ServiceBusConnection")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions,
        CancellationToken cancellationToken)
    {
        var auditDto = message.Body.ToObjectFromJson<AuditFunctionDto>();
    
        if (auditDto != null)
        {
            var audit = new Audit.Models.Audit
            {
                ServiceName = auditDto.ServiceName,
                Action = auditDto.Action,
                Details = auditDto.Details,
                Status = auditDto.Status
            };

            _db.Audits.Add(audit);
            await _db.SaveChangesAsync(cancellationToken);
        }

        await messageActions.CompleteMessageAsync(message, cancellationToken);
    }

}