
using System.ComponentModel;

namespace DbData.Entities
{
    public enum EnumPropertyGroup
    {
        [Description("Hotel")]
        Hotel = 1,
        [Description("Restaurant")]
        Restaurant = 2,
        [Description("Beach")]
        Beach = 4
    }
}
