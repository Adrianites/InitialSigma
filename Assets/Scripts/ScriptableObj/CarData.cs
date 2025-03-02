using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCarData", menuName = "CarData", order = 51)]
public class CarData : ScriptableObject
{
    #region Variables
    [SerializeField]
    private int carID = 0;
    [SerializeField]
    private Sprite carUISprite;
    [SerializeField]
    public GameObject carPrefab;
    [SerializeField]
    public Color carColour;
    [SerializeField]
    public string carName;
    #endregion

    #region Car ID
    public int CarID
    {
        get
        {
            return carID;
        }
    }
    #endregion

    #region Car UI Sprite
    public Sprite CarUISprite
    {
        get
        {
            return carUISprite;
        }
    }
    #endregion

    #region Car Prefab
    public GameObject CarPrefab
    {
        get
        {
            return carPrefab;
        }
    }
    #endregion

    #region Car Colour
    public Color CarColour
    {
        get
        {
            return carColour;
        }
    }
    #endregion

    #region Car Name
    public string CarName
    {
        get
        {
            return carName;
        }
    }
    #endregion
}
