using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BoostPad : MonoBehaviour
{
    [SerializeField] private float launchForce = 500f;
    [SerializeField] private float scrollSpeed = 1f;
    private Renderer renderer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            DriveSkateboard driveSkateboard = collision.GetComponent<DriveSkateboard>();
            if (driveSkateboard != null)
            {
                driveSkateboard.ApplyLaunchPadForce(launchForce, transform.up);
            }
        }
    }

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    void Update()
    {
        renderer.material.mainTextureOffset = new Vector2(0, -Time.realtimeSinceStartup * scrollSpeed);
    }
}
