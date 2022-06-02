using NicoKingdomManager.Shared.Models.Common;

namespace NicoKingdomManager.Shared.Models;

public class User : BaseObject
{
    public string UserName { get; set; }
    public string NickName { get; set; }
    public Role Role { get; set; }
}