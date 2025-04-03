using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

public class AiCarHandler : MonoBehaviour
{
    #region Variables
    public enum AiState {followPlayer, followWaypoint};
    public AiState aiState;
    public float maxSpeed = 70;
    public bool isAvoidingCars = true;
    [Range(0f, 1f)]
    public float skillLevel = 1f;

    // local
    Vector3 targetPosition = Vector3.zero;
    float originalMaxSpeed = 0;
    Transform targetTransform = null;
    PathNode currentPathNode = null;
    PathNode previousPathNode = null;
    PathNode[] allPathNodes;

    Vector2 avoidanceVectorLerped = Vector3.zero;

    // components
    CarController carController;
    public PolygonCollider2D carCheckerCollider;

    [Header("Adjustments")]
    float steerAmountValue = 45.0f;
    float carInFrontOfAiCircleCastValue = 1.5f;
    float AvoidanceLerpedValue = 3f; // higher the value = jitters more but more responsive
    #endregion

    #region Awake
    void Awake()
    {
        carController = GetComponent<CarController>();
        allPathNodes = FindObjectsOfType<PathNode>();
        originalMaxSpeed = maxSpeed;
    }
    #endregion

    #region Start
    void Start()
    {
        SetMaxSpeedBasedOnSkillLevel(maxSpeed);
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
            previousPathNode = currentPathNode;
        }

        if (currentPathNode != null)
        {
            targetPosition = currentPathNode.transform.position;

            float distanceToPathNode = (targetPosition - transform.position).magnitude;

            if (distanceToPathNode > 20)
            {
                Vector3 closestPointOnLine = FindClosestPointOnLine(previousPathNode.transform.position, currentPathNode.transform.position, transform.position);

                float segments = distanceToPathNode / 20.0f;
                targetPosition = (targetPosition + closestPointOnLine * segments) / (segments + 1);

                Debug.DrawLine(transform.position, targetPosition, Color.blue);
            }
            
            if (distanceToPathNode <= currentPathNode.minDistanceToReachNode)
            {   
                if (currentPathNode.aiMaxSpeed > 0)
                {
                    SetMaxSpeedBasedOnSkillLevel(currentPathNode.aiMaxSpeed);
                }
                else
                {
                    SetMaxSpeedBasedOnSkillLevel(1000);
                }

                previousPathNode = currentPathNode;

                currentPathNode = currentPathNode.nextPathNode[UnityEngine.Random.Range(0, currentPathNode.nextPathNode.Length)];
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

    #region Set Max Speed Based On Skill Level
    public void SetMaxSpeedBasedOnSkillLevel(float newSpeed)
    {
        maxSpeed = Mathf.Clamp(newSpeed, 0, originalMaxSpeed);

        float skillBasedMaxSpeed = Mathf.Clamp(skillLevel, 0.3f, 1.0f);
        maxSpeed = maxSpeed * skillBasedMaxSpeed;
    }
    #endregion

    #region Speed Up Or Slow Down
    float SpeedUpOrSlowDown(float inputX)
    {
        if (carController.GetVelocityMagnitude() > maxSpeed)
        {
            return 0f;
        }

        float reduceSpeedDueToCorner = Mathf.Abs(inputX) / 1f;

        return 1.05f - reduceSpeedDueToCorner * skillLevel;
    }
    #endregion

    #region Find Closest Point On Line
    Vector2 FindClosestPointOnLine(Vector2 lineStartPos, Vector2 lineEndPos, Vector2 point)
    {
        Vector2 lineHeadingVector = (lineEndPos - lineStartPos);

        float maxDist = lineHeadingVector.magnitude;
        lineHeadingVector.Normalize();

        Vector2 lineVectorStartPoint = point - lineStartPos;
        float dotProduct = Vector2.Dot(lineVectorStartPoint, lineHeadingVector);

        dotProduct = Mathf.Clamp(dotProduct, 0f, maxDist);

        return lineStartPos + lineHeadingVector * dotProduct;
    }
    #endregion

    #region Car in front of AI car bool
    bool CarInFrontOfAiCar(out Vector3 position, out Vector3 otherCarRightVector)
    {
        carCheckerCollider.enabled = false;
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position + transform.up * 0.5f, carInFrontOfAiCircleCastValue, transform.up, 30, 1 << LayerMask.NameToLayer("Car"));
        carCheckerCollider.enabled = true;

        if (raycastHit2D.collider != null)
        {
            position = raycastHit2D.collider.transform.position;
            otherCarRightVector = raycastHit2D.collider.transform.right;
            return true;
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

            return;
        }

        newVectorToTarget = vectorToTarget;
    }
    #endregion
}
