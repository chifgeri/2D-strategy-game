using Model;
using System.Collections.Generic;


namespace Utils
{
    public static class HeroRoster
    {
        public static Dictionary<int, PlayableData> heroes = new Dictionary<int, PlayableData>()
        {
            { 1, new PlayableData(System.Guid.NewGuid().ToString(), PlayableTypes.Archer, 1, 100, 0, null, null, 1500 )},
            { 2, new PlayableData(System.Guid.NewGuid().ToString(), PlayableTypes.Paladin, 1, 100, 0, null, null, 1500) },
            { 3, new PlayableData(System.Guid.NewGuid().ToString(), PlayableTypes.Axeman, 1, 100, 0, null, null, 2000 )},
            { 4, new PlayableData(System.Guid.NewGuid().ToString(), PlayableTypes.Medic, 1, 100, 0, null, null, 2000 )},
            { 5, new PlayableData(System.Guid.NewGuid().ToString(), PlayableTypes.Wizard, 1, 100, 0, null, null, 4500 )},
        };
    }
}
