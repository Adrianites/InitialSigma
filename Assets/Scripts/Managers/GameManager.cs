using System;
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
    float raceStartTime = 0;
    float raceFinishTime = 0;
    #endregion

    #region Events
    public event Action<GameManager> OnGameStateChange;
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

    #region Get Race Time
    public float GetRaceTime()
    {
        if (gameState == GameState.RaceOver)
        {
            return raceFinishTime - raceStartTime;
        }
        else
        {
            return Time.time - raceStartTime;
        }
    }
    #endregion

    #region Level Start
    public void LevelStart()
    {
        ChangeGameState(GameState.Countdown);
    }
    #endregion

    #region Letting other scripts use GameState
    public GameState GetGameState()
    {
        return gameState;
    }
    #endregion

    #region Change Game State
    public void ChangeGameState(GameState newGameState)
    {
        if (gameState != newGameState)
        {
            gameState = newGameState;
            OnGameStateChange?.Invoke(this);
        }
    }
    #endregion

    #region On Race Start
    public void OnRaceStart()
    {
        raceStartTime = Time.time;
        ChangeGameState(GameState.Running);
    }
    #endregion

    #region On Race Ended
    public void OnRaceEnded()
    {   
        raceFinishTime = Time.time;
        ChangeGameState(GameState.RaceOver);
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
