using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class EnemyCharacters : Singleton<EnemyCharacters>
    {
        public HeroController knightPrefab;
        public HeroController golemPrefab;
        public HeroController orcPrefab;
        public HeroController skeletonPrefab;
        public HeroController zombiePrefab;

        public HeroController EnemyTypeToPrefab(EnemyTypes playableType)
        {
            switch (playableType)
            {
                case EnemyTypes.Golem:
                    return golemPrefab;
                case EnemyTypes.Knight:
                    return knightPrefab;
                case EnemyTypes.Orc:
                    return orcPrefab;
                case EnemyTypes.Skeleton:
                    return skeletonPrefab;
                case EnemyTypes.Zombie:
                    return zombiePrefab;
                default:
                    return null;
            }
        }
    }
}
