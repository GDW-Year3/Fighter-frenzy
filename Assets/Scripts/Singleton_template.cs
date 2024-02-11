using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton_template<T> : MonoBehaviour
    where T : MonoBehaviour
{
    static T s_instance;

    public static T Instance()
    {
        if (s_instance == null)
        {
            s_instance = FindObjectOfType<T>();

            if (s_instance == null)
            {
                GameObject instanceObject = new GameObject("Singleton", typeof(T));
            }
        }

        return s_instance;
    }

    protected virtual void Awake()
    {
        if (s_instance != null && FindObjectsOfType<T>().Length > 1)
        {
            Destroy(this);
        }
    }

    protected virtual void OnDestroy()
    {
        if (s_instance == this)
        {
            s_instance = null;
        }
    }
}
