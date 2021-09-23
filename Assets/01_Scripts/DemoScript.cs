using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DemoScript : MonoBehaviour
{
    private enum PlayerState
    {
        ANGRY,
        HAPPY,
        SAD,
        NORMAL
    }
    int buttonIndex;

    [Header("Debug")]
    public bool showDebugValue = false;
    public bool showDebugConsoleValue = false;

    [Space]
    [Header("Enum")]
    [SerializeField]private PlayerState status = PlayerState.NORMAL;

    [Space]
    [Header("Panel")]
    public GameObject debugPanel;

    [Space]
    [Header ("Slider")]
    public Slider intensitySlider;
    public float intensityValue;
    public float secondTriggerValue;
    public Slider positionSourcilSlider;

    [Space]
    [Header("Image")]
    public Image playerStatus;

    [Space]
    [Header("Image")]
    public List<Image> playerSourcille;
    public int indexPlayerSourcille = 0;

    [Space]
    [Header("Inputs")]
    PlayerInput InputController;
    public Vector2 axisMovement;
    public Vector2 rightAxisStick;
    Face_Manager faceManager;
    //INITIALISATION DE L'INPUT SYSTEM
    public void Awake()
    {
        InputController = GetComponent<PlayerInput>();
        InputController.ActivateInput();
        faceManager = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Face_Manager>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (showDebugConsoleValue)
            ShowDataOnConsole();

        if (showDebugValue)
        {
            debugPanel.SetActive(true);
           // ShowData();
        }

       // GetPressedButton();
       // GetBumper();
       // intensityValue = Input.GetAxis("RightTrigger");
       // secondTriggerValue = Input.GetAxis("LeftTrigger");
        //axisMovement = new Vector2(-Input.GetAxis("LeftJoystickX"), Input.GetAxis("LeftJoystickY"));
    }

    #region Input Manager // Fonctions d'appels des différents boutons

    public void StoreMovementVector(InputAction.CallbackContext ctx)
    {
        axisMovement = -ctx.ReadValue<Vector2>();
      
    }

    public void StoreRightStick(InputAction.CallbackContext ctx)
    {
       rightAxisStick = ctx.ReadValue<Vector2>();

    }

    public void ReadTriggerValue(InputAction.CallbackContext ctx)
    {
        intensityValue = ctx.ReadValue<float>();
       
    }

    public void GetLeftBumper(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            faceManager.UpdateEyebrow(0);
        }
    }

    public void GetRightBumper(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            faceManager.UpdateEyebrow(1);
        }
    }

    public void Y_button(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ChangeState(PlayerState.NORMAL);
        }
    }

    public void B_button(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ChangeState(PlayerState.ANGRY);
        }
    }

    public void A_button(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ChangeState(PlayerState.HAPPY);
        }
    }

    public void X_button(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ChangeState(PlayerState.SAD);
        }
    }

    #endregion 

    void ShowDataOnConsole()
    {
        print("========================== Triggers =============================================");
        print("LeftTrigger " + Input.GetAxis("LeftTrigger"));
        print("RightTrigger " + Input.GetAxis("RightTrigger"));
        print("========================== Bumpers =============================================");
        print("Bumper " + Input.GetAxis("Bumper"));
        print("========================== Joysticks =============================================");
        print("LeftJoystick = LeftJoystickX " + Input.GetAxis("LeftJoystickX") + "    LeftJoystickY " + Input.GetAxis("LeftJoystickY"));
        print("RightJoystick = RightJoystickX " + Input.GetAxis("RightJoystickX") + "    RightJoystickY " + Input.GetAxis("RightJoystickY"));
        print("========================== D_Pad =============================================");
        print("D_Pad = D_PadX " + Input.GetAxis("D_PadX") + "    D_PadY " + Input.GetAxis("D_PadY"));
        print("========================== Button =============================================");
        print("A_Button " + Input.GetAxis("A_Button"));
        print("B_Button " + Input.GetAxis("B_Button"));
        print("X_Button " + Input.GetAxis("X_Button"));
        print("Y_Button " + Input.GetAxis("Y_Button"));
    }

    void ShowData()
    {
        intensitySlider.value = Input.GetAxis("RightTrigger");
        positionSourcilSlider.value = Input.GetAxis("LeftTrigger");
    }

    void GetPressedButton()
    {
        if (Input.GetAxis("A_Button") > 0)
            ChangeState(PlayerState.HAPPY);
        else if (Input.GetAxis("B_Button") > 0)
            ChangeState(PlayerState.ANGRY);
        else if (Input.GetAxis("Y_Button") > 0)
            ChangeState(PlayerState.NORMAL);
        else if (Input.GetAxis("X_Button") > 0)
            ChangeState(PlayerState.SAD);

    }

    

    void GetPressedBumper()
    {
        indexPlayerSourcille += (int)Input.GetAxis("Bumper");
        indexPlayerSourcille = Mathf.Clamp(indexPlayerSourcille, 0, playerSourcille.Count-1);
        GetRightEyeBrown();
    }

    void ChangeState(PlayerState status)
    {
        switch (status)
        {
            case PlayerState.ANGRY:
                buttonIndex = 3;
                break;
            case PlayerState.HAPPY:
                buttonIndex = 2;

                break;
            case PlayerState.SAD:
                 buttonIndex = 0;

                break;
            case PlayerState.NORMAL:
                 buttonIndex = 1;

                break;
            default:
                break;
        }
        faceManager.UpdateMouth(buttonIndex);
    }

  

    void GetRightEyeBrown()
    {
        for (int i = 0; i < playerSourcille.Count; i++)
        {
            if (i != indexPlayerSourcille)
                playerSourcille[i].gameObject.SetActive(false);
            else
                playerSourcille[i].gameObject.SetActive(true);
        }
    }
}
