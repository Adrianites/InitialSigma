using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup
{   
    #region Run Functions On Startup
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    #endregion

    #region Instantiate Prefabs
    public static void InstantiatePrefabs()
    {
        GameObject[] prefabsToInstantiate = Resources.LoadAll<GameObject>("InstantiateOnLoad/");

        foreach (GameObject prefab in prefabsToInstantiate)
        {
            GameObject.Instantiate(prefab);
        }
    }
    #endregion
}
