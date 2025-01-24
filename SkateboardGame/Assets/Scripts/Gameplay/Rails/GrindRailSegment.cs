using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindRailSegment : MonoBehaviour
{
    [SerializeField] GrindRail parentGrindRail;

    private void Awake()
    {
        if (parentGrindRail == null)
        {
            parentGrindRail = GetComponentInParent<GrindRail>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        DriveSkateboard skateboard = collision.transform.GetComponentInParent<DriveSkateboard>();

        if (skateboard != null)
        {
            if (!skateboard.isGrinding)
            {
                Debug.Log("Start Grind");
                parentGrindRail.AttachPlayerToGrindRail(skateboard);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DriveSkateboard skateboard = collision.transform.GetComponentInParent<DriveSkateboard>();

        if (skateboard != null)
        {
            if (!skateboard.isGrinding)
            {
                Debug.Log("Start Grind");
                parentGrindRail.AttachPlayerToGrindRail(skateboard);
            }
        }
    }
}
