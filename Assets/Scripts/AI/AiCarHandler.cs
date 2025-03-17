using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AiCarHandler : MonoBehaviour
{
    #region Variables
    public enum AiState {followPlayer, followWaypoint};
    public AiState aiState;
    public float maxSpeed = 70;
    public bool isAvoidingCars = true;

    // local
    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;
    PathNode currentPathNode = null;
    PathNode[] allPathNodes;

    Vector2 avoidanceVectorLerped = Vector3.zero;

    // components
    CarController carController;
    PolygonCollider2D polygonCollider2D;

    [Header("Adjustments")]
    float steerAmountValue = 45.0f;
    float carInFrontOfAiCircleCastValue = 1.2f;
    float AvoidanceLerpedValue = 4f; // higher the value = jitters more but more responsive
    #endregion

    #region Awake
    void Awake()
    {
        carController = GetComponent<CarController>();
        allPathNodes = FindObjectsOfType<PathNode>();
        polygonCollider2D = GetComponentInChildren<PolygonCollider2D>();
    }
    #endregion

    #region Fixed Update
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;
        
        switch (aiState)
        {
            case AiState.followPlayer:
                FollowPlayer();
                break;
            case AiState.followWaypoint:
                FollowWaypoint();
                break;
        }

        inputVector.x = TurnTowardsTarget();
        inputVector.y = SpeedUpOrSlowDown(inputVector.x);

        carController.SetInputVector(inputVector);
    }
    #endregion

    #region Follow Player
    void FollowPlayer()
    {
        if (targetTransform == null)
        {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
        }
    }
    #endregion

    #region Turn Towards Target
    float TurnTowardsTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        if (isAvoidingCars)
        {
            AvoidCars(vectorToTarget, out vectorToTarget);
        }

        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        float steerAmount = angleToTarget / steerAmountValue;
        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }
    #endregion

    #region Follow Waypoint
    void FollowWaypoint()
    {
        if (currentPathNode == null)
        {
            currentPathNode = FindClosestPathNode();
        }

        if (currentPathNode != null)
        {
            targetPosition = currentPathNode.transform.position;

            float ditanceToPathNode = (targetPosition - transform.position).magnitude;
            
            if (ditanceToPathNode <= currentPathNode.minDistanceToReachNode)
            {   
                if (currentPathNode.aiMaxSpeed > 0)
                {
                    maxSpeed = currentPathNode.aiMaxSpeed;
                }
                else
                {
                    maxSpeed = 1000;
                }

                currentPathNode = currentPathNode.nextPathNode[Random.Range(0, currentPathNode.nextPathNode.Length)];
            }
        }
    }
    #endregion

    #region Find Closest Path Node
    PathNode FindClosestPathNode()
    {
        return allPathNodes
            .OrderBy(x => Vector3.Distance(transform.position, x.transform.position))
            .FirstOrDefault();
    }
    #endregion

    #region Speed Up Or Slow Down
    float SpeedUpOrSlowDown(float inputX)
    {
        if (carController.GetVelocityMagnitude() > maxSpeed)
        {
            return 0f;
        }
        return 1.05f - Mathf.Abs(inputX) / 1.0f;
    }
    #endregion

    #region Car in front of AI car bool
    bool CarInFrontOfAiCar(out Vector3 position, out Vector3 otherCarRightVector)
    {
        polygonCollider2D.enabled = false;
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position + transform.up * 0.5f, carInFrontOfAiCircleCastValue, transform.up, 12, 1 << LayerMask.NameToLayer("Car"));
        polygonCollider2D.enabled = true;

        if (raycastHit2D.collider != null)
        {
            Debug.DrawRay(transform.position, transform.up * 12, Color.red);

            position = raycastHit2D.collider.transform.position;
            otherCarRightVector = raycastHit2D.collider.transform.right;
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up * 12, Color.black);
        }

        position = Vector3.zero;
        otherCarRightVector = Vector3.zero;
        return false;
    }
    #endregion

    #region Avoid Cars
    void AvoidCars(Vector2 vectorToTarget, out Vector2 newVectorToTarget)
    {
        if (CarInFrontOfAiCar(out Vector3 otherCarPosition, out Vector3 otherCarRightVector))
        {
            Vector2 avoidanceVector = Vector2.zero;

            avoidanceVector = Vector2.Reflect((otherCarPosition - transform.position).normalized, otherCarRightVector);

            float distanceToTarget = (targetPosition - transform.position).magnitude;

            float driveToTargetInfluence = 6.0f / distanceToTarget;
            driveToTargetInfluence = Mathf.Clamp(driveToTargetInfluence, 0.30f, 1.0f);

            float avoidanceInfluence = 1.0f - driveToTargetInfluence;

            avoidanceVectorLerped = Vector2.Lerp(avoidanceVectorLerped, avoidanceVector, Time.deltaTime * AvoidanceLerpedValue);

            newVectorToTarget = vectorToTarget * driveToTargetInfluence + avoidanceVectorLerped * avoidanceInfluence;
            newVectorToTarget.Normalize();

            Debug.DrawRay(transform.position, avoidanceVector * 10, Color.green);
            Debug.DrawRay(transform.position, newVectorToTarget * 10, Color.yellow);
            return;
        }

        newVectorToTarget = vectorToTarget;
    }
    #endregion
}
