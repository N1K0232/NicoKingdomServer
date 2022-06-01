using NicoKingdomManager.Shared.Models.Common;

namespace NicoKingdomManager.Shared.Models.Requests;

#nullable enable
public class SaveUserRequest : BaseRequestObject
{
    public string UserName { get; set; } = "";
    public string? NickName { get; set; }
    public string[] Roles { get; set; } = Array.Empty<string>();
}
#nullable disable