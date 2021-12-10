using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    static T instance;

    public static T Instance {
        get { return instance; }
    }

    public static void SetInstance(T inst)
    {
         instance = inst;
    }

    public static bool IsInitialized {
        get { return instance != null; }
    }

    protected virtual void Awake() {
        if(instance != null){
            Debug.LogError("Tried to instantiate a Singleton multiple times");
        } else {
            instance = (T) this;
        }
    }

     protected virtual void OnDestroy() {
         if(instance == this){
             instance = null;
         }
     }


}
