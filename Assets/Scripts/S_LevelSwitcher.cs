using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LevelSwitcher : MonoBehaviour
{
    #region Variables
    public Scene scene;
    public enum Scene{MainMenu, Scene1, Scene2, Scene3, Scene4}
    #endregion

    #region On Trigger
    public void OnTriggerEnter2D(Collider2D collision)
    {   if (collision.gameObject.tag == "Player")
        {
        OnSwitchScene();
        }
        else
        {
            Debug.Log("Object Not Assigned Tag");
        }
    }
    #endregion

    #region Switch Scene
    public void OnSwitchScene()
    {
        switch(scene)
        {
            case Scene.MainMenu:
                GameManager.instance.ChangeGameState(GameState.Countdown);
                SceneManager.LoadScene(NameStrings.MainMenu);
                break;
            case Scene.Scene1:
                GameManager.instance.ChangeGameState(GameState.Countdown);
                SceneManager.LoadScene(NameStrings.SingleplayerLevel1);
                break;
            case Scene.Scene2:
                GameManager.instance.ChangeGameState(GameState.Countdown);
                SceneManager.LoadScene(NameStrings.SingleplayerLevel2);
                break;
            case Scene.Scene3:
                GameManager.instance.ChangeGameState(GameState.Countdown);
                SceneManager.LoadScene(NameStrings.SingleplayerLevel3);
                break;
            case Scene.Scene4:
                GameManager.instance.ChangeGameState(GameState.Countdown);
                SceneManager.LoadScene(NameStrings.SingleplayerLevel4);
                break;


        }
    }
    #endregion
}
