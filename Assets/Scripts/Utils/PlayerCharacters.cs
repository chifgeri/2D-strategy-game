using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class PlayerCharacters : Singleton<PlayerCharacters>
    {
        public HeroController axemanPrefab;
        public HeroController paldinPrefab;
        public HeroController archerPrefab;
        public HeroController wizardPrefab;
        public HeroController medicPrefab;

       public HeroController PlayerTypeToPrefab(PlayableTypes playableType)
        {
            switch (playableType)
            {
                case PlayableTypes.Axeman:
                    return axemanPrefab;
                case PlayableTypes.Paladin:
                    return paldinPrefab;
                case PlayableTypes.Archer:
                    return archerPrefab;
                case PlayableTypes.Medic:
                    return medicPrefab;
                case PlayableTypes.Wizard:
                    return wizardPrefab;
                default:
                    return null;
            }
        }
    }
}
