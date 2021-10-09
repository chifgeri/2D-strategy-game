using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public class EnemyCharacter : Character
    {
        override public void Die(){
            base.Die();
            // Drop items and money
        }
    }
}
