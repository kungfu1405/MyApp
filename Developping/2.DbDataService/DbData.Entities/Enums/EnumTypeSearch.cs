
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DbData.Entities.Enums
{
    public enum EnumTypeSearch
    {
        [Description("Place To Visit")]
        PlaceToVisit = 1,

        [Description("Place To Stay")]
        PlaceToStay = 2,

        [Description("Place To Eat")]
        PlaceToEat = 3,

        [Description("Story")]
        Story = 4,

        [Description("Collection")]
        Collection = 5
    }
}
