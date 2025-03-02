using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectUICar : MonoBehaviour
{
    #region Variables
    public GameObject carPrefab;
    public Transform spawnPoint;
    bool isChangingCar = false;
    UICarHandler uiCarHandler = null;
    CarData[] carDataArray;
    int currentCarIndex = 0;
    int playerNumber = 0; // Add this to track the player number
    #endregion

    #region Start
    void Start()
    {
        carDataArray = Resources.LoadAll<CarData>("CarData/");

        StartCoroutine(SpawnCarCO());
    }
    #endregion

    #region Update
    void Update()
    {   
        // change inputs to use unity input system instead of hard coded keys
        // testing purposes only
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousCar();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextCar();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectCar();
        }
    }
    #endregion

    #region Previous Car
    public void PreviousCar()
    {
        if(isChangingCar)
        {
            return;
        }
        currentCarIndex--;

        if (currentCarIndex < 0)
        {
            currentCarIndex = carDataArray.Length - 1;
        }
        StartCoroutine(SpawnCarCO());
    }
    #endregion

    #region Next Car
    public void NextCar()
    {
        if(isChangingCar)
        {
            return;
        }
        currentCarIndex++;
        
        if (currentCarIndex > carDataArray.Length - 1)
        {
            currentCarIndex = 0;
        }
        StartCoroutine(SpawnCarCO());
    }
    #endregion

    #region Select Car
    public void SelectCar()
    {
        PlayerPrefs.SetInt($"P{playerNumber + 1}SelectedCarID", carDataArray[currentCarIndex].CarID);
        PlayerPrefs.Save();

        // Load the next scene or do something else after selecting the car
        SceneManager.LoadScene(NameStrings.Test);
    }
    #endregion

    #region Spawn Car Coroutine
    IEnumerator SpawnCarCO()
    {
        isChangingCar = true;
        if (uiCarHandler != null)
        {
            uiCarHandler.ExitCarAnim();
        }

        GameObject instantiatedCar = Instantiate(carPrefab, spawnPoint);
        uiCarHandler = instantiatedCar.GetComponent<UICarHandler>();
        uiCarHandler.SetupCar(carDataArray[currentCarIndex]);
        uiCarHandler.EnterCarAnim();

        yield return new WaitForSeconds(0.5f);
        isChangingCar = false;
    }
    #endregion

    #region Set Player Number
    public void SetPlayerNumber(int number)
    {
        playerNumber = number;
    }
    #endregion
}
