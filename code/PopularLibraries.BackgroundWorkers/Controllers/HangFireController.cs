using Hangfire;
using Hangfire.Common;
using Microsoft.AspNetCore.Mvc;
using PopularLibraries.BackgroundWorkers.UseCases;

namespace PopularLibraries.BackgroundWorkers.Controllers;


[ApiController]
[Route("[controller]")]
public class HangFireController : ControllerBase
{
    private readonly SendPostCreatedNotification _sendPostNotification;

    public HangFireController(SendPostCreatedNotification sendPostNotification)
    {
        _sendPostNotification = sendPostNotification;
    }

    [HttpGet("hangifre-execution")]
    public bool HangfireExecution()
    {
        BackgroundJob.Enqueue(() => _sendPostNotification.Execute("identifier-1"));
        return true;
    }
}