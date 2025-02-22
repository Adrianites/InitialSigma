using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostCarPlayback : MonoBehaviour
{
    #region Variables
    GhostCarData ghostCarData = new GhostCarData();
    List<GhostCarDataList> ghostCarDataList = new List<GhostCarDataList>();
    int currentPlaybackIndex = 0;

    float lastStoredTime = 0.1f;
    Vector2 lastStoredPosition = Vector2.zero;
    float lastStoredRotation = 0;
    Vector3 lastStoredScale = Vector3.one;

    float duration = 0.1f;
    #endregion

    #region Update
    void Update()
    {
        if (ghostCarDataList.Count == 0)
        {
            return;
        }

        if (Time.timeSinceLevelLoad >= ghostCarDataList[currentPlaybackIndex].timeSinceLevelLoaded)
        {
            lastStoredTime = ghostCarDataList[currentPlaybackIndex].timeSinceLevelLoaded;
            lastStoredPosition = ghostCarDataList[currentPlaybackIndex].postion;
            lastStoredRotation = ghostCarDataList[currentPlaybackIndex].rotationZ;
            lastStoredScale = ghostCarDataList[currentPlaybackIndex].localScale;

            if (currentPlaybackIndex < ghostCarDataList.Count - 1)
            {
                currentPlaybackIndex++;
            }

            duration = ghostCarDataList[currentPlaybackIndex].timeSinceLevelLoaded - lastStoredTime;
        }

        float timePassed = Time.timeSinceLevelLoad - lastStoredTime;
        float lerpValue = timePassed / duration;

        transform.position = Vector2.Lerp(lastStoredPosition, ghostCarDataList[currentPlaybackIndex].postion, lerpValue);
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, lastStoredRotation), Quaternion.Euler(0, 0, ghostCarDataList[currentPlaybackIndex].rotationZ), lerpValue);
        transform.localScale = Vector3.Lerp(lastStoredScale, ghostCarDataList[currentPlaybackIndex].localScale, lerpValue);
    }
    #endregion

    #region Load Data
    public void LoadData(int playerID)
    {
        if(!PlayerPrefs.HasKey($"{SceneManager.GetActiveScene().name}_{playerID}_ghost"))
        {
            Destroy(gameObject);
        }
        else
        {
            string jsonEncodedData = PlayerPrefs.GetString($"{SceneManager.GetActiveScene().name}_{playerID}_ghost");

            ghostCarData = JsonUtility.FromJson<GhostCarData>(jsonEncodedData);
            ghostCarDataList = ghostCarData.GetDataList();
        }
    }
    #endregion


}
