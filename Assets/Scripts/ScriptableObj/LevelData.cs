using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "LevelData", order =53)]
public class LevelData : ScriptableObject
{
    #region Variables
    [SerializeField]
    private int levelID = 0;
    [SerializeField]
    private Sprite levelPic;
    [SerializeField]
    private UnityEngine.SceneManagement.Scene level;
    [SerializeField]
    private string levelName;
    #endregion

    #region Get Level ID
    public int LevelID
    {
        get
        {
            return levelID;
        }
    }
    #endregion

    #region Get Level Pic
    public Sprite LevelPic
    {
        get
        {
            return levelPic;
        }
    }
    #endregion

    #region Get Scene
    public UnityEngine.SceneManagement.Scene Level
    {
        get
        {
            return level;
        }
    }
    #endregion

    #region Get Level Name
    public string LevelName
    {
        get
        {
            return levelName;
        }
    }
    #endregion
}
