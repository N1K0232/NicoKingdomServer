using NicoKingdomManager.Shared.Models.Common;

namespace NicoKingdomManager.Shared.Models.Requests;

#nullable enable

//the properties UserName and RoleName have the default value set
//to "" because they must have a value... the NickName property is the only
//one property that can have the null value because if it's null,
//the database will set the value of the column as the value of the UserName property

public class SaveUserRequest : BaseRequestObject
{
    public string UserName { get; set; } = "";
    public string? NickName { get; set; }
    public string RoleName { get; set; } = "";
}

#nullable disable