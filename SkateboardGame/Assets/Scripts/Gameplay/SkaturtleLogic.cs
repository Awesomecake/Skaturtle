using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SkaturtleLogic : MonoBehaviour
{
    private static SkaturtleLogic _instance;

    //private GameManager gm;
    [SerializeField] private DriveSkateboard driveSkateboard;

    public UnityEvent OnRespawn;

    //OnDeath Explosion
    [SerializeField] GameObject explosionPrefab;
    private bool isRespawning = false;

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

    public void InputTriggerRespawn(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Respawn();
        }
    }

    //Starts Respawn Logic, Triggers Explosion
    public void Respawn() 
    {
        if (!isRespawning)
        {
            isRespawning = true;
            Instantiate(explosionPrefab, driveSkateboard.transform.position, Quaternion.identity);
            driveSkateboard.Freeze();
            driveSkateboard.GetComponent<Renderer>().enabled = false;
            driveSkateboard.StopGrind();
            StartCoroutine(ResetRespawnEffects());
        }
    }

    //Resets Player Position and Other Interactions
    private void ResetGameScene()
    {
        transform.position = GameManager.Instance.CheckpointPos;
        transform.rotation = Quaternion.identity;
        driveSkateboard.ResetVelocity();
        driveSkateboard.StopGrind();
        driveSkateboard.canJump = true;
        isRespawning = false;

        OnRespawn?.Invoke();
    }

    //Ends Respawn Visual Effects
    IEnumerator ResetRespawnEffects()
    {
        yield return new WaitForSeconds(1.5f);
        ResetGameScene();
        driveSkateboard.UnFreeze();
        driveSkateboard.GetComponent<Renderer>().enabled = true;
    }
}
