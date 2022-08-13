
using System.ComponentModel;

namespace DbData.Entities
{
    public enum EnumAttractionType
    {
        [Description("Place to visit")]
        PlaceToVisit = 1,
        [Description("Place to stay")]
        PlaceToStay = 2,
        [Description("Place to eat")]
        PlaceToEat = 4
    }
}
