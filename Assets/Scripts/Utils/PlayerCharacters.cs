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
        public HeroController knightPrefab1;
        public HeroController knightPrefab2;
        public HeroController knightPrefab3;
        public HeroController archerPrefab;
        public HeroController wizardPrefab;
        public HeroController medicPrefab;

       public HeroController PlayerTypeToPrefab(PlayableTypes playableType)
        {
            switch (playableType)
            {
                case PlayableTypes.Knight1:
                    return knightPrefab1;
                case PlayableTypes.Knight2:
                    return knightPrefab2;
                case PlayableTypes.Knight3:
                    return knightPrefab3;
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
