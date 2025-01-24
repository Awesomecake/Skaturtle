using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindRail : MonoBehaviour
{
    [SerializeField] List<Transform> grindRailWaypoints;
    [SerializeField] float heightOffset = 0.3f;

    public void AttachPlayerToGrindRail(DriveSkateboard skateboard)
    {
        int nearestWaypointIndex = FindNearestWaypointIndex(skateboard.transform);

        List<Transform> waypointsToFollow;

        if(skateboard.playerRB.velocity.x > 0)
        {
            waypointsToFollow = grindRailWaypoints.GetRange(nearestWaypointIndex, grindRailWaypoints.Count - nearestWaypointIndex);
            waypointsToFollow.Reverse();

            Debug.Log("Forward Stack: ");
            foreach(Transform waypoint in waypointsToFollow)
            {
                Debug.Log(waypoint.ToString());
            }
        }
        else
        {
            waypointsToFollow = grindRailWaypoints.GetRange(0, nearestWaypointIndex);
            //waypointsToFollow.Reverse();

            Debug.Log("Backward Stack: ");
            foreach (Transform waypoint in waypointsToFollow)
            {
                Debug.Log(waypoint.ToString());
            }
        }

        skateboard.StartGrind(waypointsToFollow);

        //skateboard.playerRB.position = grindRailWaypoints[nearestWaypointIndex].position
        //    + new Vector3(0, heightOffset);

        //skateboard.playerRB.SetRotation(grindRailWaypoints[nearestWaypointIndex].rotation);
    }

    private int FindNearestWaypointIndex(Transform playerTransform)
    {
        Vector2 playerPos = playerTransform.position;

        float minDistance = Vector2.Distance(playerPos, grindRailWaypoints[0].position);
        int closestWaypointIndex = 0;

        for (int i = 1; i < grindRailWaypoints.Count; i++)
        {
            float distance = Vector2.Distance(playerPos, grindRailWaypoints[i].position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestWaypointIndex = i;
            }
        }

        return closestWaypointIndex;
    }
}
