using NicoKingdomManager.Shared.Models.Common;
using NicoKingdomManager.Shared.Models.Enums;

namespace NicoKingdomManager.Shared.Models.Requests;

#nullable enable

//the properties User, Title, Action and Description have the default value set
//to "" because they must have a value... the Reason property is the only
//one property that can have the null value

public class SaveLogRequest : BaseRequestObject
{
    public string UserName { get; set; } = "";
    public string Title { get; set; } = "";
    public string Action { get; set; } = "";
    public string Description { get; set; } = "";
    public string? Reason { get; set; }
    public DateTime? LogDate { get; set; }
    public LogType LogType { get; set; }
}

#nullable disable