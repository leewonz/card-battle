using Con;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    [SerializeField]
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null) {
                instance = FindObjectOfType<T>();
                if(instance == null)
                {
                    instance = GameObject.FindGameObjectWithTag(Tags.gameController).AddComponent<T>();
                }
                instance.Init(); 
            };
            
            return instance;
        }
    }

    protected virtual void Init()
    {

    }
}
