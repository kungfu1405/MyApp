
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DbData.Entities.Enums
{
    public enum EnumPillar
    {
        [Description("Wellness & Retreats")]
        WellnessAndRetreats = 1,

        [Description("Wine & Dine")]
        WineAndDine = 2,

        [Description("Cities & Culture")]
        CitiesAndCulture = 3,

        [Description("Adventure & Exploration")]
        AdventureAndExploration = 4,

        [Description("Nature & Preservation")]
        NatureAndPreservation = 5
    }
}
