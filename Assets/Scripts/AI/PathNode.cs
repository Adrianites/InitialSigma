using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    #region Variables
    public float aiMaxSpeed = 0;
    public float minDistanceToReachNode = 5;
    public PathNode[] nextPathNode;
    #endregion
}
