using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region Variables
    private List<PlayerInput> players = new List<PlayerInput>();

    [SerializeField]
    private List<Transform> startingPoints;
    
    [SerializeField]
    private List<LayerMask> playerLayer;

    public PlayerInputManager inputManager;
    #endregion

    #region Awake
    private void Awake()
    {
        inputManager = FindObjectOfType<PlayerInputManager>();
        startingPoints = new List<Transform>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spawnpoint"))
        {
            startingPoints.Add(obj.transform);
        }
    }
    #endregion

    #region OnEnable
    private void OnEnable()
    {
        inputManager.onPlayerJoined += AddPlayer;
    }
    #endregion

    #region OnDisable
    private void OnDisable()
    {
        inputManager.onPlayerJoined -= AddPlayer;
    }
    #endregion

    #region AddPlayer
    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
        
        Transform playerParent = player.transform.parent;
        playerParent.position = startingPoints[players.Count - 1].position;

        int layerToAdd = (int)Mathf.Log(playerLayer[players.Count - 1].value, 2);

        playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        playerParent.GetComponentInChildren<InputHandler>().horizontal = player.actions.FindAction("Look");
        
    }
    #endregion
}