using NicoKingdomManager.DataAccessLayer.Entities.Common;

namespace NicoKingdomManager.DataAccessLayer.Entities;

public class User : BaseEntity
{
    public Guid RoleId { get; set; }
    public string UserName { get; set; }
    public string NickName { get; set; }
    public Role Role { get; set; }
    public List<ServerLog> ServerLogs { get; set; }
}