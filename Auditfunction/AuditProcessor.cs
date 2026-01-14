using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Auditfunction;

public class AuditProcessor(ILogger<AuditProcessor> logger, IAuditServiceBusProcessor serviceBusProcessor)
{
    private readonly ILogger<AuditProcessor> _logger = logger;
    private readonly IAuditServiceBusProcessor _serviceBusProcessor= serviceBusProcessor;

    [Function("AuditProcessor")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.User, "get", "post")] HttpRequest req)
    {
        try
        {
            _logger.LogInformation("C# AuditProcessor processed a request.");
            var auditData = req?.ReadFromJsonAsync<List<AuditData>>().Result;
            if (auditData != null && auditData.All(aud => string.IsNullOrEmpty(aud.WorkEffortId)))
            {
                return new BadRequestObjectResult("No Valid Audit Data present");
            }
            else
            {

                await _serviceBusProcessor.SendDataToAuditQueue(JsonSerializer.Serialize(auditData));
            }

            return new OkObjectResult("Data Sent to Audit Processor Successfully");
        }
       catch (Exception ex)
        {
            _logger.LogError($"Exception in AuditProcessor: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}