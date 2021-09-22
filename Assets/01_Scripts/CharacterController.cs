using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("AudioClip")]
    public AudioClip upSound;
    public AudioClip downSound;
    public AudioClip leftSound;
    public AudioClip rightSound;

    public bool canplay;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if((Input.GetAxis("RightJoystickX")>0.2f || Input.GetAxis("RightJoystickX") < -0.2f) && (Input.GetAxis("RightJoystickY") > 0.2f || Input.GetAxis("RightJoystickY") < -0.2f) && canplay)
            playSoundByJoystick();
        else
            canplay = true;
    }

    void playSoundByJoystick()
    {
        Vector2 joystickPosition = new Vector2(Input.GetAxis("RightJoystickX"), Input.GetAxis("RightJoystickY"));
        print(joystickPosition);
        AudioClip soundToPlay = null;

        if (joystickPosition.x > 0 && joystickPosition.y > 0)
            soundToPlay = upSound;
        else if (joystickPosition.x < 0 && joystickPosition.y < 0)
            soundToPlay = leftSound;
        else if (joystickPosition.x > 0 && joystickPosition.y < 0)
            soundToPlay = downSound;
        else if (joystickPosition.x < 0 && joystickPosition.y > 0)
            soundToPlay = rightSound;
        else if (joystickPosition.x == 0 && joystickPosition.y == 0)
            soundToPlay = upSound;

        audioSource.clip = soundToPlay;
        audioSource.Play();
        canplay = false;
    }
}
