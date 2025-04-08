using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LevelSwitcher : MonoBehaviour
{
    #region Variables
    public Scene scene;
    public enum Scene{MainMenu, Scene1, Scene2, Scene3, Scene4}
    public Animator sceneAnimator;
    #endregion

    #region Awake
    void Awake()
    {
        sceneAnimator.SetTrigger(NameStrings.StartLevel);
    }
    #endregion

    #region On Trigger
    public void OnTriggerEnter2D(Collider2D collision)
    {   if (collision.gameObject.tag == "Player")
        {
        StartCoroutine(FadeInCo(0.05f));
        }
        else
        {
            Debug.Log("Object Not Assigned Tag");
        }
    }
    #endregion


    IEnumerator FadeInCo(float WaitForSeconds)
    {
        switch(scene)
        {
        case Scene.MainMenu:
            sceneAnimator.SetTrigger(NameStrings.EndLevel);
            yield return new WaitForSeconds(WaitForSeconds);

            GameManager.instance.ChangeGameState(GameState.Countdown);
            SceneManager.LoadScene(NameStrings.MainMenu);
            break;
        case Scene.Scene1:
            sceneAnimator.SetTrigger(NameStrings.EndLevel);
            yield return new WaitForSeconds(WaitForSeconds);

            GameManager.instance.ChangeGameState(GameState.Countdown);
            SceneManager.LoadScene(NameStrings.SingleplayerLevel1);
            break;
        case Scene.Scene2:
            sceneAnimator.SetTrigger(NameStrings.EndLevel);
            yield return new WaitForSeconds(WaitForSeconds);

            GameManager.instance.ChangeGameState(GameState.Countdown);
            SceneManager.LoadScene(NameStrings.SingleplayerLevel2);
            break;
        case Scene.Scene3:
            sceneAnimator.SetTrigger(NameStrings.EndLevel);
            yield return new WaitForSeconds(WaitForSeconds);

            GameManager.instance.ChangeGameState(GameState.Countdown);
            SceneManager.LoadScene(NameStrings.SingleplayerLevel3);
            break;
        case Scene.Scene4:
            sceneAnimator.SetTrigger(NameStrings.EndLevel);
            yield return new WaitForSeconds(WaitForSeconds);

            GameManager.instance.ChangeGameState(GameState.Countdown);
            SceneManager.LoadScene(NameStrings.SingleplayerLevel4);
            break;    
        }
    }
}
