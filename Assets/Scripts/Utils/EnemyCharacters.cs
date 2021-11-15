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
        public EnemyController knightPrefab;
        public EnemyController golemPrefab;
        public EnemyController orcPrefab;
        public EnemyController skeletonPrefab;
        public EnemyController zombiePrefab;

        public EnemyController EnemyTypeToPrefab(EnemyTypes playableType)
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
