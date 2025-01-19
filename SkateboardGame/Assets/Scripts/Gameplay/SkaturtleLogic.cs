using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaturtleLogic : MonoBehaviour
{
    //private GameManager gm;
    void Start()
    {
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

    }

    public void Respawn()
    {
        transform.position = GameManager.Instance.CheckpointPos;
    }
}
