using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GhostCarDataList : ISerializationCallbackReceiver
{
    #region Variables
    [System.NonSerialized]
    public Vector2 postion = Vector2.zero;

    [System.NonSerialized]
    public float rotationZ = 0;

    [System.NonSerialized]
    public float timeSinceLevelLoaded = 0;

    [System.NonSerialized]
    public Vector3 localScale = Vector3.one;

    [SerializeField]
    int x = 0;

    [SerializeField]
    int y = 0;  

    [SerializeField]
    int r = 0; // rotation

    [SerializeField]
    int t = 0; // time

    [SerializeField]
    int s = 0; // scale
    #endregion

    #region Constructor
    public GhostCarDataList(Vector2 _postion, float _rotationZ,  Vector3 _localScale, float _timeSinceLevelLoaded)
    {
        postion = _postion;
        rotationZ = _rotationZ;
        timeSinceLevelLoaded = _timeSinceLevelLoaded;
        localScale = _localScale;
    }
    #endregion

    #region On Before Serialize
    public void OnBeforeSerialize()
    {
        t = (int)(timeSinceLevelLoaded * 1000.0f);

        x = (int)(postion.x * 1000.0f);
        y = (int)(postion.y * 1000.0f);

        s = (int)(localScale.x * 1000.0f);

        r = Mathf.RoundToInt(rotationZ);
    }
    #endregion

    #region On After Deserialize
    public void OnAfterDeserialize()
    {
        timeSinceLevelLoaded = t / 1000.0f;
        postion.x = x / 1000.0f;
        postion.y = y / 1000.0f;
        localScale = new Vector3(s / 1000.0f, s / 1000.0f, s / 1000.0f);
        rotationZ = r;
    }
    #endregion
}
