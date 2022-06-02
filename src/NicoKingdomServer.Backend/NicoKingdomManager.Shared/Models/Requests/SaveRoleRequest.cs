using NicoKingdomManager.Shared.Models.Common;

namespace NicoKingdomManager.Shared.Models.Requests;

#nullable enable

//the Color property is set to nullable because
//it can be specified later in another moment

public class SaveRoleRequest : BaseRequestObject
{
    public string Name { get; set; } = "";
    public string? Color { get; set; }
}

#nullable disable