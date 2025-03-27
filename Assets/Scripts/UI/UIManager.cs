using System.Collections;
using System.Collections.Generic;
#if (UNITY_EDITOR)
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Variables
    GameObject deathCanvas = null;
    GameObject inGameCanvas = null;
    GameObject winCanvas = null;
    GameObject pauseCanvas = null;
    public PlayerManager playerManager;
    public PlayerData playerData;
    public LevelData levelData;
    public Toggle player2Toggle;
    public Toggle player3Toggle;
    public Toggle player4Toggle;
    public TMP_Text CountdownText = null;
    #endregion

    #region Awake
    private void Awake()
    {
        deathCanvas = GameObject.Find("DeathCanvas");
        inGameCanvas = GameObject.Find("InGameCanvas");
        winCanvas = GameObject.Find("WinCanvas");
        pauseCanvas = GameObject.Find("PauseCanvas");
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerData = Resources.Load<PlayerData>("PlayerData");
        levelData = Resources.Load<LevelData>("LevelData");
        if (CountdownText != null)
        {
            CountdownText.text = "";
        }

    }
    #endregion

    #region Start
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == NameStrings.MainMenu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            StartCoroutine(CountdownCO());
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        
            if (deathCanvas != null)
            {
                deathCanvas.SetActive(false);
            }

            if (inGameCanvas != null)
            {
                inGameCanvas.SetActive(true);
            }
            if (winCanvas != null)
            {
                winCanvas.SetActive(false);
            }
            if (pauseCanvas != null)
            {
                pauseCanvas.SetActive(false);
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

    #region Countdown Coroutine
    IEnumerator CountdownCO()
    {
        yield return new WaitForSeconds(0.3f);
        int count = 3;

        while (true)
        {
            if (count !=0)
            {
                CountdownText.text = count.ToString();
            }
            else
            {
                CountdownText.text = "G!";
                GameManager.instance.OnRaceStart();
                break;
            }
            count--;
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);
        CountdownText.text = "";
    }
    #endregion

    #region Select Car
    public void SelectCarMultiplayer()
    {
        SceneManager.LoadScene(NameStrings.MultiplayerLevel1);
    }

    public void SelectCarSingleplayer()
    {
        SceneManager.LoadScene(NameStrings.SingleplayerLevel1);
    }
    #endregion

    #region Restart Level
    public void RestartLevel()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.LevelStart();
        ResumeGame();
    }
    #endregion

    #region Pause Menu
    public void PauseGame()
    {
        Time.timeScale = 0;
        if(deathCanvas != null)
        {
            deathCanvas.SetActive(false);
        }
        
        if(inGameCanvas != null)
        {
            inGameCanvas.SetActive(false);
        }

        if(pauseCanvas != null)
        {
            pauseCanvas.SetActive(true);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    #endregion

    #region Death Menu
    public void Death()
    {
        Time.timeScale = 0;
        if(deathCanvas != null)
        {
            deathCanvas.SetActive(true);
        }
        
        if(inGameCanvas != null)
        {
            inGameCanvas.SetActive(false);
        }

        if(pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    #endregion

    #region End Menu
    public void WinMenu()
    {
        Time.timeScale = 0;
        if(winCanvas != null)
        {
            winCanvas.SetActive(true);
        }
        
        if(inGameCanvas != null)
        {
            inGameCanvas.SetActive(false);
        }
        if(deathCanvas != null)
        {
            deathCanvas.SetActive(false);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    #endregion

    #region Resume Game
    public void ResumeGame()
    {
        Time.timeScale = 1;
        if(deathCanvas != null)
        {
            deathCanvas.SetActive(false);
        }
        
        if(inGameCanvas != null)
        {
            inGameCanvas.SetActive(true);
        }

        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region MainMenu
    public void MainMenu()
    {
        SceneManager.LoadScene(NameStrings.MainMenu);
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    #endregion

    #region Quit Game
    public void QuitGame()
    {
            #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            #endif

            #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE)
                Application.Quit();
            #elif (UNITY_WEBGL)
                SceneManager.LoadScene(NameStrings.Quit);
            #endif
    }
    #endregion

    #region On Finish Line
    public void Finish()
    {
        Time.timeScale = 0;
        if(deathCanvas != null)
        {
            deathCanvas.SetActive(true);
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

    #region SocialPlug
    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
    #endregion
}
