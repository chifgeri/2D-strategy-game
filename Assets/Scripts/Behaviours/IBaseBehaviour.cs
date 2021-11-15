using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Behaviours
{
    public interface IBaseBehaviour
    {
        public void Action(EnemyCharacter enemy, PlayerCharacter[] players, EnemyCharacter[] enemyCharacters);
    }
}
