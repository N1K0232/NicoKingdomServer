using NicoKingdomManager.DataAccessLayer.Entities.Common;

namespace NicoKingdomManager.DataAccessLayer.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }
    public string Color { get; set; }
    public List<User> Users { get; set; }
}