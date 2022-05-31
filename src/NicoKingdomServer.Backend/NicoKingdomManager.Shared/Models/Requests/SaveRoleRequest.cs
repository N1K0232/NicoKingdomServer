using NicoKingdomManager.Shared.Models.Common;

namespace NicoKingdomManager.Shared.Models.Requests;

#nullable enable
public class SaveRoleRequest : BaseRequestObject
{
    public string Name { get; set; } = "";
    public string? Color { get; set; }
}
#nullable disable