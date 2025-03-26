using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#region GameState Enumeration
public enum GameState
{
    Countdown,
    Running,
    RaceOver,
}
#endregion

public class GameManager : MonoBehaviour
{
    #region Variables
    GameState gameState = GameState.Countdown;
    public static GameManager instance = null;

    public PlayerStats playerStats;
    #endregion

    #region Awake
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Level Start
    void LevelStart()
    {
        gameState = GameState.Countdown;
        Debug.Log("Level Start");
    }
    #endregion

    #region Letting other scripts use GameState
    public GameState GetGameState()
    {
        return gameState;
    }
    #endregion

    #region On Race Start
    public void OnRaceStart()
    {
        gameState = GameState.Running;
        Debug.Log("Race Start");
    }
    #endregion

    #region OnEnabled
    private void OnEnabled()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion

    #region On Scene Loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelStart();
    }
    #endregion
}
