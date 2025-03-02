using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICarHandler : MonoBehaviour
{   
    #region Variables
    public Image carImage = null;
    public Color carColour = Color.white;
    public string carName = "";
    Animator animator = null;
    NameStrings animStrings;
    #endregion

    #region Awake
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    #endregion

    #region Setup Car
    public void SetupCar(CarData carData)
    {
        carImage.sprite = carData.CarUISprite;
        carColour = carData.carColour;
        carName = carData.carName;
    }
    #endregion

    #region Enter Car Animation
    public void EnterCarAnim()
    {
        animator.Play(NameStrings.CarAppear);
        
    }
    #endregion

    #region Exit Car Animation
    public void ExitCarAnim()
    {
        animator.Play(NameStrings.CarDisappear);
    }
    #endregion

    #region On Car Exit Animation Complete
    public void OnCarExitAnimComplete()
    {
        Destroy(gameObject);
    }
    #endregion

}
