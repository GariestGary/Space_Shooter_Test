using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    public static bool isDestroyed = false;
    private static T instance;
    private static object lockObject = new object();

    public static T Instance { get 
        {
            if (isDestroyed)
            {
                return null;
            }

            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        var singleton = new GameObject("[SINGLETON] " + typeof(T));
                        instance = singleton.AddComponent<T>();
                        DontDestroyOnLoad(singleton);
                    }
                }
                return instance;
            }
        } 
    }

    public virtual void OnDestroy()
    {
        isDestroyed = true;
    }

}
