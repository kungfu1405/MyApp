
using System.ComponentModel;

namespace DbData.Entities
{
    public enum EnumTouristClass
    {
        [Description("Budget class")]
        Budget = 1,
        [Description("Middle class")]
        MidRange = 2,
        [Description("Luxury class")]
        Luxury = 4
    }
}
