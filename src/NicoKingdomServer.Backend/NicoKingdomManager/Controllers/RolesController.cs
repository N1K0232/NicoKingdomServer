using Microsoft.AspNetCore.Mvc;
using NicoKingdomManager.BusinessLayer.Services.Common;
using NicoKingdomManager.Shared.Models.Requests;

namespace NicoKingdomManager.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class RolesController : ControllerBase
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
    public async Task<IActionResult> Get(string name, int pageIndex = 0, int itemsPerPage = 10)
    {
        var roles = await rolesService.GetAsync(name, pageIndex, itemsPerPage);
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