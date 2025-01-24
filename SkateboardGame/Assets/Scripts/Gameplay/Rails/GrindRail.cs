using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindRail : MonoBehaviour
{
    [SerializeField] List<GrindRailSegment> grindRailSegments;
    [SerializeField] List<Transform> grindRailWaypoints;
    [SerializeField] float heightOffset = 0.3f;

    private void Start()
    {
        grindRailWaypoints = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GrindRailSegment railSegment = transform.GetChild(i).GetComponent<GrindRailSegment>();
            if (railSegment != null)
            {
                grindRailWaypoints.AddRange(railSegment.waypoints);
            }
        }

        //foreach (GrindRailSegment segment in grindRailSegments)
        //{
        //    grindRailWaypoints.AddRange(segment.waypoints);
        //}
    }

    public void AttachPlayerToGrindRail(DriveSkateboard skateboard)
    {
        int nearestWaypointIndex = FindNearestWaypointIndex(skateboard.transform);
        List<Transform> waypointsToFollow;

        Vector2 railDirection;

        if (nearestWaypointIndex == 0)
        {
            railDirection = grindRailWaypoints[nearestWaypointIndex + 1].position - grindRailWaypoints[nearestWaypointIndex].position;
            railDirection.Normalize();
        }
        else
        {
            railDirection = grindRailWaypoints[nearestWaypointIndex].position - grindRailWaypoints[nearestWaypointIndex - 1].position;
            railDirection.Normalize();
        }

        float relativeDirection = Vector2.Dot(railDirection, skateboard.playerRB.velocity.normalized);

        if (relativeDirection > 0)
        {
            waypointsToFollow = grindRailWaypoints.GetRange(nearestWaypointIndex, grindRailWaypoints.Count - nearestWaypointIndex);
            waypointsToFollow.Reverse();

            //Debug.Log("Forward Stack: ");
            //foreach(Transform waypoint in waypointsToFollow)
            //{
            //    Debug.Log(waypoint.ToString());
            //}
        }
        else
        {
            waypointsToFollow = grindRailWaypoints.GetRange(0, nearestWaypointIndex);
            //waypointsToFollow.Reverse();

            //Debug.Log("Backward Stack: ");
            //foreach (Transform waypoint in waypointsToFollow)
            //{
            //    Debug.Log(waypoint.ToString());
            //}
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
