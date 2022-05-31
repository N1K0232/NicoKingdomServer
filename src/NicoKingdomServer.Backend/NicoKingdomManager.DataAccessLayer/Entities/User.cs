using NicoKingdomManager.DataAccessLayer.Entities.Common;

namespace NicoKingdomManager.DataAccessLayer.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string NickName { get; set; }
    public List<UserRole> UserRoles { get; set; }
}