using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleOnPlayerJoin : MonoBehaviour
{
    #region Variables
    private PlayerInputManager inputManager;
    #endregion

    #region Awake
    private void Awake()
    {
        inputManager = FindObjectOfType<PlayerInputManager>();
    }
    #endregion

    #region OnEnable
    private void OnEnable()
    {
        inputManager.onPlayerJoined += ToggleGameObject;
    }
    #endregion

    #region OnDisable
    private void OnDisable()
    {
        inputManager.onPlayerJoined -= ToggleGameObject;
    }
    #endregion

    #region ToggleGameObject
    private void ToggleGameObject(PlayerInput player)
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
