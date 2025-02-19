using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLayerHandler : MonoBehaviour
{
    #region Variables
    public SpriteRenderer carOutlineSR;
    List<SpriteRenderer> defaultLayerSR = new List<SpriteRenderer>();
    List<Collider2D> BridgeColliderList = new List<Collider2D>();
    List<Collider2D> TunnelColliderList = new List<Collider2D>();
    Collider2D carCollider;
    bool isDrivingOnBridge = false;
    #endregion

    #region Awake
    void Awake()
    {
        foreach (SpriteRenderer sr in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (sr.sortingLayerName == "Default")
            {
                defaultLayerSR.Add(sr);
            }
        }

        foreach (GameObject bridgeColliderGO in GameObject.FindGameObjectsWithTag("BridgeCollider"))
        {
            BridgeColliderList.Add(bridgeColliderGO.GetComponent<Collider2D>());
        }

        foreach (GameObject tunnelColliderGO in GameObject.FindGameObjectsWithTag("TunnelCollider"))
        {
            TunnelColliderList.Add(tunnelColliderGO.GetComponent<Collider2D>());
        }

        carCollider = GetComponentInChildren<Collider2D>();
    }
    #endregion

    #region Start
    void Start()
    {
        UpdateSortingAndCollisionLayers();
    }
    #endregion

    #region UpdateSortingAndCollisionLayers
    void UpdateSortingAndCollisionLayers()
    {
        if (isDrivingOnBridge)
        {
            SetSortingLayer("RaceTrackBridge");
            carOutlineSR.enabled = false;
        }   
        else
        {
            SetSortingLayer("Default");
            carOutlineSR.enabled = true;
        }

        SetCollisionWithBridge();
    }
    #endregion

    #region SetCollision
    void SetCollisionWithBridge()
    {
        foreach (Collider2D collider2D in BridgeColliderList)
        {
            Physics2D.IgnoreCollision(carCollider, collider2D, !isDrivingOnBridge);
        }

        foreach (Collider2D collider2D in TunnelColliderList)
        {
            if (isDrivingOnBridge)
            {
                Physics2D.IgnoreCollision(carCollider, collider2D, true);
            }
            else
            {
                Physics2D.IgnoreCollision(carCollider, collider2D, false);
            }
        }
    }
    #endregion

    #region SetSortingLayer
    void SetSortingLayer(string layerName)
    {
        foreach (SpriteRenderer sr in defaultLayerSR)
        {
            sr.sortingLayerName = layerName;
        }
    }
    #endregion

    #region IsDrivingOnBridge
    public bool IsDrivingOnBridge()
    {
        return isDrivingOnBridge;
    }
    #endregion

    #region OnTriggerEnter2D
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("BridgeTrigger"))
        {
            isDrivingOnBridge = true;
            UpdateSortingAndCollisionLayers();
        }
        else if (collider2D.CompareTag("TunnelTrigger"))
        {
            isDrivingOnBridge = false;
            UpdateSortingAndCollisionLayers();
        }
    }
    #endregion
}
