using Microsoft.AspNetCore.Mvc;

namespace NicoKingdomManager.Controllers.Common;

/// <summary>
/// this controller is used only as placeholder to avoid code duplication of the
/// 3 attributes below
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Route("application/json")]
public abstract class ApiController : ControllerBase
{
    protected ApiController() : base()
    {
    }
}