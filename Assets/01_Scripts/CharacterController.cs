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

    [Header("Part")]
    public GameObject haut;
    public GameObject bas;
    public GameObject tete;

    public bool canplay;
    float mouthtimer;
    Face_Manager FaceManager;
    DemoScript InputManager;
    // Start is called before the first frame update
    private void Awake()
    {
        transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        FaceManager = FindObjectOfType<Face_Manager>();
        InputManager = FindObjectOfType<DemoScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying == false)
        {
            FaceManager.ToggleMouth(false);
            if ((InputManager.rightAxisStick.x >0.2f || InputManager.rightAxisStick.x < -0.2f) && (InputManager.rightAxisStick.y > 0.2f || InputManager.rightAxisStick.y < -0.2f))
            playSoundByJoystick();
        }
        else
        {
           // 
        }
        
    }

    void Cooldown()
    {
        if (true)
        {

        }
    }

    void playSoundByJoystick()
    {
        Vector2 joystickPosition = InputManager.rightAxisStick;
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
        audioSource.PlayOneShot(soundToPlay);
        FaceManager.ToggleMouth(true);
        //canplay = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("p");
        ObjetScript obj;
        if(other.gameObject.TryGetComponent<ObjetScript>(out obj))
        {
            switch (obj.typeOfObject)
            {
                case ObjetScript.Status.HAUT:
                    haut.GetComponent<SpriteRenderer>().sprite = obj.sprite.sprite;
                    haut.GetComponent<SpriteRenderer>().color = obj.sprite.color;
                    break;
                case ObjetScript.Status.BAS:
                    bas.GetComponent<SpriteRenderer>().sprite = obj.sprite.sprite;
                    bas.GetComponent<SpriteRenderer>().color = obj.sprite.color;
                    break;

                    case ObjetScript.Status.TETE:
                    tete.GetComponent<SpriteRenderer>().sprite = obj.sprite.sprite;
                    tete.GetComponent<SpriteRenderer>().color = obj.sprite.color;
                    break;
                default:
                    break;
            }
            Destroy(other.gameObject);
        }
    }
}
