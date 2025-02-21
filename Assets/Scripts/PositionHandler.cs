using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    #region Variables
    LeaderboardUIHandler leaderboardUIHandler;
    public List<CarLapCounter> lapCounters = new List<CarLapCounter>();
    #endregion

    #region Awake
    void Awake()
    {
        CarLapCounter[] lapCountersArray = FindObjectsOfType<CarLapCounter>();

        lapCounters = lapCountersArray.ToList<CarLapCounter>();

        foreach(CarLapCounter lapCounter in lapCounters)
        {
            lapCounter.OnPassCheckpoint += OnPassCheckpoint;
        }

        leaderboardUIHandler = FindObjectOfType<LeaderboardUIHandler>();
    }
    #endregion

    #region Start
    void Start()
    {
        leaderboardUIHandler.UpdateList(lapCounters);
    }
    #endregion

    #region On Pass Checkpoint
    void OnPassCheckpoint(CarLapCounter carLapCounter)
    {
        lapCounters = lapCounters.OrderByDescending(s => s.GetNumOfPassedCheckpoints()).ThenBy(s => s.GetTimeSinceLastCheckpoint()).ToList();
    
        int carPosition = lapCounters.IndexOf(carLapCounter) + 1;

        carLapCounter.SetCarPos(carPosition);

        leaderboardUIHandler.UpdateList(lapCounters);
    }
    #endregion


}
