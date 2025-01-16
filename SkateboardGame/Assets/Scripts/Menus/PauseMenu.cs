using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseOverlay;
    bool paused = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused) 
            {
                Time.timeScale = 1f;
                paused = false;
                pauseOverlay.active = false;
            } 
            else 
            {
                Time.timeScale = 0f;
                paused = true;
                pauseOverlay.active = true;
            }
        }
    }
}
