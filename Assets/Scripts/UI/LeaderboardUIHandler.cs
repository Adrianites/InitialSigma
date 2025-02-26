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
        VerticalLayoutGroup leaderboardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        // Resize the array if necessary
        if (lapCounters.Count > setLeaderboardItems.Length)
        {
            int oldLength = setLeaderboardItems.Length;
            System.Array.Resize(ref setLeaderboardItems, lapCounters.Count);

            for (int i = oldLength; i < lapCounters.Count; i++)
            {
                GameObject leaderboardInfoGO = Instantiate(leaderboardItemPrefab, leaderboardLayoutGroup.transform);
                setLeaderboardItems[i] = leaderboardInfoGO.GetComponent<SetLeaderboardInfo>();
                setLeaderboardItems[i].SetPositionTXT($"{i + 1}.");
            }
        }

        for (int i = 0; i < lapCounters.Count; i++)
        {
            if (setLeaderboardItems[i] == null)
            {
                GameObject leaderboardInfoGO = Instantiate(leaderboardItemPrefab, leaderboardLayoutGroup.transform);
                setLeaderboardItems[i] = leaderboardInfoGO.GetComponent<SetLeaderboardInfo>();
                setLeaderboardItems[i].SetPositionTXT($"{i + 1}.");
            }
            setLeaderboardItems[i].SetNameTXT(lapCounters[i].gameObject.name);
        }
    }
    #endregion
}
