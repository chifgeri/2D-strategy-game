using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAnimation : MonoBehaviour
{
    //[SerializeField]
    //private Camera camera;

    private Vector3 casterLastPosition;

    private Vector3 targetLastPosition;
    
    public void PlayActionSequence(PlayerCharacter caster, Character target)
    {
        
        // Előtérbe kerül
        // Kamera zoomol kicsit
        // 
        // Caster támad
        // Belül megtörténik a Hit vagy Heal vagy stb.
        // 
    }

    //private IEnumerator WaitAnimationFinish(Animation animation)
    //{

    //}



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
