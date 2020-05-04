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
#if UNITY_EDITOR
                if (!_instance)
                {
                    string[] configsGUIDs = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).Name);
                    if (configsGUIDs.Length > 0)
                    {
                        _instance = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(UnityEditor.AssetDatabase.GUIDToAssetPath(configsGUIDs[0]));
                    }
                }
#else
                var type = typeof(T);
                var instances = Resources.FindObjectsOfTypeAll<T>();
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
#endif
            }

            return _instance;
        }
    }
}
