using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkaturtleLogic : MonoBehaviour
{
    private static SkaturtleLogic _instance;

    //private GameManager gm;
    [SerializeField] private DriveSkateboard driveSkateboard;

    public UnityEvent OnRespawn;

    public static SkaturtleLogic Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SkaturtleLogic>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(GameManager).ToString());
                    _instance = singleton.AddComponent<SkaturtleLogic>();
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

    }

    public void Respawn()
    {
        transform.position = GameManager.Instance.CheckpointPos;
        transform.rotation = Quaternion.identity;
        driveSkateboard.ResetVelocity();

        OnRespawn?.Invoke();
    }
}
