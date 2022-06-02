using Microsoft.AspNetCore.Mvc;
using NicoKingdomManager.BusinessLayer.Services.Common;
using NicoKingdomManager.Shared.Models.Requests;

namespace NicoKingdomManager.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class UsersController : ControllerBase
{
    private readonly IUsersService usersService;

    public UsersController(IUsersService usersService)
    {
        this.usersService = usersService;
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await usersService.DeleteAsync(id);
        return Ok("user deleted successfully");
    }

    [HttpGet("Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string nickName = null)
    {
        var users = await usersService.GetAsync(nickName);
        return users != null ? Ok(users) : NotFound("no user found");
    }

    [HttpPost("Save")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save([FromBody] SaveUserRequest request)
    {
        var savedUser = await usersService.SaveAsync(request);
        return Ok(savedUser);
    }
}