using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugBuild : MonoBehaviour
{

    DemoScript InputManager;
    Face_Manager FaceManager;
    MovementController MovementController;
   
    // Start is called before the first frame update
    void Start()
    {
        InputManager = FindObjectOfType<DemoScript>();
        FaceManager = FindObjectOfType<Face_Manager>();
        MovementController = FindObjectOfType<MovementController>();
    
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            ToggleSound();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            ToggleDebug();
        }*/
    }

    bool toggle;
    public void ToggleSound()
    {
        toggle = !toggle;

        if (toggle)
            AudioListener.volume = 1f;

        else
            AudioListener.volume = 0f;
    }
    bool debug_toggle;
    public void ToggleDebug()
    {
        debug_toggle = !debug_toggle;

        if (debug_toggle) { 
            InputManager.showDebugConsoleValue = true;
            InputManager.showDebugValue = true;
            InputManager.debugPanel.SetActive(true);
        }



        else { 
            InputManager.showDebugConsoleValue = false;
            InputManager.showDebugValue = false;
            InputManager.debugPanel.SetActive(false);
        }
    }
}
