using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUIHandler : MonoBehaviour
{
    #region Variables
    public GameObject leaderboardItemPrefab;
    SetLeaderboardInfo[] setLeaderboardItems;
    #endregion

    #region Awake
    void Awake()
    {
        VerticalLayoutGroup leaderboardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        setLeaderboardItems = new SetLeaderboardInfo[carLapCounterArray.Length];

        for (int i = 0; i < carLapCounterArray.Length; i++)
        {
            GameObject leaderboardInfoGO = Instantiate(leaderboardItemPrefab, leaderboardLayoutGroup.transform);
            setLeaderboardItems[i] = leaderboardInfoGO.GetComponent<SetLeaderboardInfo>();
            setLeaderboardItems[i].SetPositionTXT($"{i + 1}.");
        }
    }
    #endregion

    #region Update List
    public void UpdateList(List<CarLapCounter> lapCounters)
    {
        for (int i = 0; i < lapCounters.Count; i++)
        {
            setLeaderboardItems[i].SetNameTXT(lapCounters[i].gameObject.name);
        }
    }
    #endregion
}
