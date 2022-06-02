using Microsoft.AspNetCore.Mvc;
using NicoKingdomManager.BusinessLayer.Services.Common;
using NicoKingdomManager.Controllers.Common;
using NicoKingdomManager.Shared.Models.Requests;

namespace NicoKingdomManager.Controllers;

public sealed class RolesController : ApiController
{
    private readonly IRolesService rolesService;

    public RolesController(IRolesService rolesService)
    {
        this.rolesService = rolesService;
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await rolesService.DeleteAsync(id);
        return Ok("role successfully deleted");
    }

    [HttpGet("Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string name)
    {
        var roles = await rolesService.GetAsync(name);
        return roles != null ? Ok(roles) : NotFound("no role found");
    }

    [HttpPost("Save")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save([FromBody] SaveRoleRequest request)
    {
        var savedRole = await rolesService.SaveAsync(request);
        return Ok(savedRole);
    }
}