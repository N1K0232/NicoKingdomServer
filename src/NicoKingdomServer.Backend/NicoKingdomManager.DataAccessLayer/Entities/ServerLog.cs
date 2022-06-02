using NicoKingdomManager.DataAccessLayer.Entities.Common;
using NicoKingdomManager.Shared.Models.Enums;

namespace NicoKingdomManager.DataAccessLayer.Entities;

public class ServerLog : BaseEntity
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Action { get; set; }
    public string Description { get; set; }
    public string Reason { get; set; }
    public DateTime LogDate { get; set; }
    public LogType LogType { get; set; }
    public User User { get; set; }
}