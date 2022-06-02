using NicoKingdomManager.Shared.Models.Common;
using NicoKingdomManager.Shared.Models.Enums;

namespace NicoKingdomManager.Shared.Models;

public class ServerLog : BaseObject
{
    public string Title { get; set; }
    public string Action { get; set; }
    public string Description { get; set; }
    public string Reason { get; set; }
    public DateTime LogDate { get; set; }
    public LogType LogType { get; set; }
    public User User { get; set; }
}