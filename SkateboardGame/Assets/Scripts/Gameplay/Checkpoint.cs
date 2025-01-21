using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] bool hasCheckpointAlreadyBeenActivated = false;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>(); 
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!hasCheckpointAlreadyBeenActivated)
            {
                gm.CheckpointPos = transform.position;
                hasCheckpointAlreadyBeenActivated = true;
            }
        }
    }
}
