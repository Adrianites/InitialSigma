using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarLapCounter : MonoBehaviour
{
    #region Variables
    public TextMeshProUGUI positionText;
    public Color carColour;
    UIManager uiManager;
    public float delayUntilHidePosition = 2;
    int passedCheckpointNum = 0;
    float timeSinceLastCheckpoint = 0;
    int NumOfPassedCheckpoints = 0;
    [SerializeField]
    int lapsCompleted = 0;
    [SerializeField]
    int lapsToComplete = 2;
    bool raceFinished = false;
    int carPosition = 0;
    bool isHideRoutineRunning = false;
    float hideUIDelayTime;
    #endregion

    #region Awake
    void Awake()
    {
        positionText.color = carColour;
        uiManager = FindObjectOfType<UIManager>();
    }
    #endregion

    #region Event
    public event Action<CarLapCounter> OnPassCheckpoint;
    #endregion

    #region Get Car Position
    public void SetCarPos(int position)
    {
        carPosition = position;
    }
    #endregion

    #region Get Num Of Passed Checkpoints
    public int GetNumOfPassedCheckpoints()
    {
        return NumOfPassedCheckpoints;
    }
    #endregion

    #region Get Time Since Last Checkpoint
    public float GetTimeSinceLastCheckpoint()
    {
        return timeSinceLastCheckpoint;
    }
    #endregion

    #region Car Position
    IEnumerator ShowPositionCO(float delayUntilHidePosition)
    {
        hideUIDelayTime += delayUntilHidePosition;
        positionText.text = carPosition.ToString();

        positionText.gameObject.SetActive(true);

        if (!isHideRoutineRunning)
        {
            isHideRoutineRunning = true;

            yield return new WaitForSeconds(hideUIDelayTime);

            positionText.gameObject.SetActive(false);
            isHideRoutineRunning = false;
        }
    }
    #endregion

    #region On Trigger Enter
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Checkpoint"))
        {
            if (raceFinished)
            {
                uiManager.WinMenu();
                return;
            }

            Checkpoint checkpoint = collider2D.GetComponent<Checkpoint>();
            
            if (passedCheckpointNum + 1 == checkpoint.checkpointNumber)
            {
                passedCheckpointNum = checkpoint.checkpointNumber;
                
                NumOfPassedCheckpoints++;
                
                timeSinceLastCheckpoint = Time.time;

                if (checkpoint.isFinishLine)
                {
                    passedCheckpointNum = 0;
                    lapsCompleted++;

                    if (lapsCompleted >= lapsToComplete)
                    {
                        raceFinished = true;
                    }
                }
                
                OnPassCheckpoint?.Invoke(this);
                
                if(raceFinished)
                {
                    StartCoroutine(ShowPositionCO(100));
                }
                else
                {
                    StartCoroutine(ShowPositionCO(delayUntilHidePosition));
                }
            }
        }
    }
    #endregion
}

