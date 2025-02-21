using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetLeaderboardInfo : MonoBehaviour
{
    #region Variables
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI nameText;
    #endregion

    #region Start
    void Start()
    {
        
    }
    #endregion

    #region Set Position
    public void SetPositionTXT(string newPosition)
    {
        positionText.text = newPosition;
    }
    #endregion

    #region Set Name
    public void SetNameTXT(string newName)
    {
        nameText.text = newName;
    }
    #endregion
}
