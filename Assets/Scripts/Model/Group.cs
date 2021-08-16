using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {

/**
* A Group which contains the players or the enemy characters
* They move together and fight together
**/

public class Group {
    private List<Character> characters;

    public List<Character> Characters {
        get {
            if(characters.Count > 4){
                Debug.LogError("[Group] More characters than expected.");
                return null;
            } else {
                return characters;
            }
        }
    }

    public Character SelectedCharacer {
        get;
        set;
    }

    protected void Awake() {
        characters = new List<Character>(4);
    }

    public void AddCharacter(Character character){
        characters.Add(character);
    }
}

}
