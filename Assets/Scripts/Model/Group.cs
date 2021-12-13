using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {

/**
* A Group which contains the players or the enemy characters
* They move together and fight together
**/

    public class Group<T> where T : Character {
        private List<T> characters;

        public List<T> Characters {
            get {
                if(characters.Count > 4){
                    Debug.LogError("[Group] More characters than expected.");
                    return null;
                } else {
                    return characters;
                }
            }
        }

        public Group() {
            characters = new List<T>(4);
        }

        public void AddCharacter(T character){
            if(characters.Count < 4 && character != null)
            {
                characters.Add(character);
            }
        }

        public void RemoveCharacter(T character)
        {
            characters.Remove(character);
        }
    }

}
