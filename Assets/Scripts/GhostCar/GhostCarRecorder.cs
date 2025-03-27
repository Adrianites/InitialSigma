using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostCarRecorder : MonoBehaviour
{
    #region Variables
    public Transform carSprite;
    public GameObject ghostCarPlaybackPrefab;
    GhostCarData ghostCarData = new GhostCarData();
    bool isRecording = true;
    Rigidbody2D rb;
    CarInputHandler carInputHandler;
    #endregion

    #region Awake
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        carInputHandler = GetComponent<CarInputHandler>();

        GameManager.instance.OnGameStateChange += OnGameStateChange;
    }
    #endregion

    #region Start
    void Start()
    {
        GameObject ghostCar = Instantiate(ghostCarPlaybackPrefab);

        ghostCar.GetComponent<GhostCarPlayback>().LoadData(carInputHandler.playerID);
    }
    #endregion

    #region Record Ghost Car Data Couroutine
    IEnumerator RecordGhostCarDataCO()
    {
        while (isRecording)
        {
            if (carSprite != null)
            {
                ghostCarData.AddData(new GhostCarDataList(rb.position, rb.rotation, carSprite.localScale, Time.timeSinceLevelLoad));
            }
            yield return new WaitForSeconds(0.15f);
        }
    }
    #endregion

    #region Save Car Pos Coroutine
    IEnumerator SaveCarPositionCO()
    {
        yield return new WaitForSeconds(1);
        SaveData();
    }
    #endregion

    #region Save Data
    void SaveData()
    {
        string jsonEncodedData = JsonUtility.ToJson(ghostCarData);
        
        if (carInputHandler != null)
        {
            PlayerPrefs.SetString($"{SceneManager.GetActiveScene().name}_{carInputHandler.playerID}_ghost", jsonEncodedData);
            PlayerPrefs.Save();
        }

        isRecording = false;
    }
    #endregion

    #region On Game State Change
    void OnGameStateChange(GameManager gameManager)
    {
        if (gameManager.GetGameState() == GameState.Running)
        {
            StartCoroutine(RecordGhostCarDataCO());
        }

        if (gameManager.GetGameState() == GameState.RaceOver)
        {
            StartCoroutine(SaveCarPositionCO());
        }
    }
    #endregion

    #region On Destroy
    void OnDestroy()
    {
        GameManager.instance.OnGameStateChange -= OnGameStateChange;
    }
    #endregion
}
