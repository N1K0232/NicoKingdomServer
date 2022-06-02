using Microsoft.AspNetCore.Mvc;
using NicoKingdomManager.BusinessLayer.Services.Common;
using NicoKingdomManager.Shared.Models.Requests;

namespace NicoKingdomManager.Controllers;

public sealed class ServerLogsController : ControllerBase
{
    private readonly IServerLogsService serverLogsService;

    public ServerLogsController(IServerLogsService serverLogsService)
    {
        this.serverLogsService = serverLogsService;
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await serverLogsService.DeleteAsync(id);
        return Ok("server log successfully deleted");
    }

    [HttpGet("Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid? userId = null)
    {
        var serverLogs = await serverLogsService.GetAsync(userId);
        return serverLogs != null ? Ok(serverLogs) : NotFound("no log found");
    }

    [HttpPost("Save")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save([FromBody] SaveLogRequest request)
    {
        var savedLog = await serverLogsService.SaveAsync(request);
        return Ok(savedLog);
    }
}