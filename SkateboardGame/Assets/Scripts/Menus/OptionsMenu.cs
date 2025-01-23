using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    private bool isActive = false;

    public void ToggleOptionsMenu()
    {
        if(isActive)
        {
            isActive = false;
            optionsMenu.SetActive(false);
        }
        else
        {
            isActive = true;
            optionsMenu.SetActive(true);
        }
    }
}
