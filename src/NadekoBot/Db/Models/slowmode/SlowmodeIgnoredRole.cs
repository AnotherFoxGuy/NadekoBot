#nullable disable
namespace NadekoBot.Db.Models;

public class SlowmodeIgnoredRole : DbEntity
{
    public ulong RoleId { get; set; }

    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;

        return ((SlowmodeIgnoredRole)obj).RoleId == RoleId;
    }

    // override object.GetHashCode
    public override int GetHashCode()
        => RoleId.GetHashCode();
}