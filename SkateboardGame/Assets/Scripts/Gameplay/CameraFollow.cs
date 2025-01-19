using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private Camera cameraToMove;

    // Update is called once per frame
    void Update()
    {
        cameraToMove.transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, -10);
    }
}
