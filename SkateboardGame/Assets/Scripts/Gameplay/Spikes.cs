using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private SkaturtleLogic player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SkaturtleLogic>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            player.Respawn();
        }
    }
}
