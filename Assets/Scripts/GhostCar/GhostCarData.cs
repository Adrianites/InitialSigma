using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GhostCarData 
{
    #region Variables
    [SerializeField]
    List<GhostCarDataList> ghostCarRecorderList = new List<GhostCarDataList>();
    #endregion

    #region Add Data
    public void AddData(GhostCarDataList _ghostCarDataList)
    {
        ghostCarRecorderList.Add(_ghostCarDataList);
    }
    #endregion

    #region Get Data List
    public List<GhostCarDataList> GetDataList()
    {
        return ghostCarRecorderList;
    }
    #endregion
}
