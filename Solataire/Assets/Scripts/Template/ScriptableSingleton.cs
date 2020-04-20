using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
{
    protected static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var type = typeof(T);
                var instances = Resources.LoadAll<T>("GameAssets");
                _instance = instances.FirstOrDefault();
                if (_instance == null)
                {
                    Debug.LogErrorFormat("[ScriptableSingleton] No instance of {0} found!", type.ToString());
                }
                else if (instances.Length > 1)
                {
                    Debug.LogErrorFormat("[ScriptableSingleton] Multiple instances of {0} found!", type.ToString());
                }
                else
                {
                    Debug.LogFormat("[ScriptableSingleton] An instance of {0} was found!", type.ToString());
                }
            }
            return _instance;
        }
    }
}
