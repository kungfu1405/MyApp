using System.ComponentModel;

namespace DbData.Entities
{
    public enum EnumTouristObject
    {
        [Description("Family object")]
        Family = 1,
        [Description("Couple object")]
        Couple = 2,
        [Description("Solo object")]
        Solo = 4,
        [Description("Friends object")]
        Friends = 8,
        [Description("Group object")]
        Group = 16
    }
}
