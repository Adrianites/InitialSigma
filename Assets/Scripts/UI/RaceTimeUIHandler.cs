using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceTimeUIHandler : MonoBehaviour
{
    #region Variables
    TMP_Text time;
    float lastRaceTimeUpdate = 0;
    #endregion

    #region Awake
    void Awake()
    {
        time = GetComponent<TMP_Text>();
    }
    #endregion

    #region Start
    void Start()
    {
        StartCoroutine(UpdateTimeCO());
    }
    #endregion

    #region Update Time Coroutine
    IEnumerator UpdateTimeCO()
    {
        while (true)
        {
            float raceTime = GameManager.instance.GetRaceTime();

            if (lastRaceTimeUpdate != raceTime)
            {
                int raceTimeMinutes = (int)Mathf.Floor(raceTime / 60);
                int raceTimeSeconds = (int)Mathf.Floor(raceTime % 60);

                time.text = $"{raceTimeMinutes.ToString("00")}:{raceTimeSeconds.ToString("00")}";

                lastRaceTimeUpdate = raceTime;
            }
        yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion
}
