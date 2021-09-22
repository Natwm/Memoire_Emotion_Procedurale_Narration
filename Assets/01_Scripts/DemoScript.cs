using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour
{
    private enum PlayerState
    {
        ANGRY,
        HAPPY,
        SAD,
        NORMAL
    }

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
    public Slider positionSourcilSlider;

    [Space]
    [Header("Image")]
    public Image playerStatus;

    [Space]
    [Header("Image")]
    public List<Image> playerSourcille;
    public int indexPlayerSourcille = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showDebugConsoleValue)
            ShowDataOnConsole();

        if (showDebugValue)
        {
            debugPanel.SetActive(true);
            ShowData();
            GetPressedButton();
            GetPressedBumper();
        }
            
    }

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
                playerStatus.color = Color.red;
                break;
            case PlayerState.HAPPY:
                playerStatus.color = Color.green;
                break;
            case PlayerState.SAD:
                playerStatus.color = Color.blue;
                break;
            case PlayerState.NORMAL:
                playerStatus.color = Color.yellow;
                break;
            default:
                break;
        }
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
