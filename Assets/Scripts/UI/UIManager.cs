using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Variables
    GameObject pauseCanvas = null;
    GameObject inGameCanvas = null;
    public PlayerManager playerManager;
    public PlayerData playerData;
    public Toggle player2Toggle;
    public Toggle player3Toggle;
    public Toggle player4Toggle;
    #endregion

    #region Awake
    private void Awake()
    {
        pauseCanvas = GameObject.Find("PauseCanvas");
        inGameCanvas = GameObject.Find("InGameCanvas");
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerData = Resources.Load<PlayerData>("PlayerData");
    }
    #endregion

    #region Start
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        
            if (pauseCanvas != null)
            {
                pauseCanvas.SetActive(false);
            }

            if (inGameCanvas != null)
            {
                inGameCanvas.SetActive(true);
            }
        }

        if (player2Toggle != null)
        {
            player2Toggle.isOn = playerData.player2Joined;
            player2Toggle.onValueChanged.AddListener(delegate { Player2Joined(); });
        }
        if (player3Toggle != null)
        {
            player3Toggle.isOn = playerData.player3Joined;
            player3Toggle.onValueChanged.AddListener(delegate { Player3Joined(); });
        }
        if (player4Toggle != null)
        {
            player4Toggle.isOn = playerData.player4Joined;
            player4Toggle.onValueChanged.AddListener(delegate { Player4Joined(); });
        }
    }
    #endregion

    #region Restart Level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }
    #endregion

    #region Pause Menu
    public void PauseGame()
    {
        Time.timeScale = 0;
        if(pauseCanvas != null)
        {
            pauseCanvas.SetActive(true);
        }
        
        if(inGameCanvas != null)
        {
            inGameCanvas.SetActive(false);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    #endregion

    #region Resume Game
    public void ResumeGame()
    {
        Time.timeScale = 1;
        if(pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        }
        
        if(inGameCanvas != null)
        {
            inGameCanvas.SetActive(true);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region On Finish Line
    public void Finish()
    {
        Time.timeScale = 0;
        if(pauseCanvas != null)
        {
            pauseCanvas.SetActive(true);
        }
        
        if(inGameCanvas != null)
        {
            inGameCanvas.SetActive(false);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    #endregion

    public void SetPlayerData(PlayerData data)
    {
        playerData = data;
    }    

    #region Extra Players Joining
    public void Player2Joined()
    {
        playerData.player2Joined = player2Toggle.isOn;
    }

    public void Player3Joined()
    {
        playerData.player3Joined = player3Toggle.isOn;
    }

    public void Player4Joined()
    {
        playerData.player4Joined = player4Toggle.isOn;
    }
    #endregion
}
