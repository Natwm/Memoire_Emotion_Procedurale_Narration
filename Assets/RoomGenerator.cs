using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomGenerator : MonoBehaviour
{
    // Test Variables
    public Room_So TestRoom;
    public static RoomGenerator instance;

    [Header ("UI")]
    // UI Management
    public GameObject RoomPanel;
    public TMP_Text TitleText;
    [Space]
    [Header("Intro")]
    public GameObject Intro;
    public TMP_Text IntroText;

    [Space]
    [Header("Outro")]
    public GameObject Outro;
    public TMP_Text OutroText;
    public GameObject BaseOutroButton;
    public GameObject LayoutGroup;

    // RoomManagement
    public Room_So CurrentRoom;
    public RoomExit GoToRoom;

    

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : RoomGenerator");
        else
            instance = this;

        InitializeToCanvas();
    }

    public void InitializeToCanvas()
    {
        RoomPanel.transform.parent = GameObject.Find("Canvas").transform;
    }

    public void SetIntro()
    {
        Intro.SetActive(true);
        Outro.SetActive(false);
    }

    public void SetOutro()
    {
        Debug.Log("Outro");
        Outro.SetActive(true);
        Intro.SetActive(false);
    }

    public void GenerateRoom(Room_So RoomToCreate)
    {
        int distribution = (RoomToCreate.Room_Size.x * RoomToCreate.Room_Size.y) / 3;
        
        GridManager.instance.CreateTerrainWithParam(RoomToCreate.Room_Size);
        EventGenerator.instance.DetermineDoors(false);
        EventGenerator.instance.DetermineKey();
        EventGenerator.instance.PopulateTiles(RoomToCreate.ObjectDistribution);
        foreach (GameObject item in EventGenerator.instance.occupiedTiles)
        {
           int randomInt = Random.Range(0, RoomToCreate.PossibleTiles.Count - 1);
            item.GetComponent<Case_Behaviours>().CaseEffects = RoomToCreate.PossibleTiles[randomInt].SpawnAsset(item);
        }
        foreach (CaseContener_SO item in RoomToCreate.GaranteedTiles)
        {
            GameObject newcase = EventGenerator.instance.GetRandomClearTile();
            Debug.Log("Garanteed " + newcase.name);
            if (newcase.GetComponent<Case_Behaviours>().CaseEffects == null)
            {
                newcase.GetComponent<Case_Behaviours>().CaseEffects = item.SpawnAsset(newcase);
            }
            else
            {
                Destroy(newcase.transform.GetChild(0).gameObject);
                newcase.GetComponent<Case_Behaviours>().CaseEffects = item.SpawnAsset(newcase);
            }
        }
        RoomToCreate.ApplyEffect();
    }

    public void FadeIn()
    {
        RoomPanel.GetComponent<Animator>().SetTrigger("Reveal");
        Intro.SetActive(true);
        Outro.SetActive(true);
        IntroText.gameObject.SetActive(true);
       
    }

    public void FadeOut()
    {
        
        GenerateRoom(CurrentRoom);
        RoomPanel.GetComponent<Animator>().SetTrigger("FadeOut");
        Intro.SetActive(false);
        Outro.SetActive(false);
        IntroText.gameObject.SetActive(false);
    }

    public void TransitionAnim()
    {
        RoomPanel.GetComponent<Animator>().SetTrigger("RoomTransition");
    }

    public void OnRoomCompletion()
    {
        FadeIn();
        SetOutro();
        EventGenerator.instance.TempClearGrid();
    }

    public void InitialiseRoomToUi(Room_So Room)
    {
        TitleText.text = Room.RoomName;
        IntroText.text = Room.Room_IntroText;
        OutroText.text = Room.Room_OutroText;
        if (LayoutGroup.transform.childCount>0)
        {
            foreach (Transform child in LayoutGroup.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        
        for (int i = 0; i < Room.PossibleExits.Length; i++)
        {
            CreateExitButton(Room.PossibleExits[i]);
        }
    }

    public void CreateExitButton(RoomExit Exit)
    {
        GameObject ExitButton = GameObject.Instantiate(BaseOutroButton);
        if (Exit.Exit_Text != null)
        {
            ExitButton.GetComponent<TMP_Text>().text = Exit.Exit_Text;
        }
        else
        {
            ExitButton.GetComponent<TMP_Text>().text = "Aller à " + Exit.Exit_To_Room.name;
        }
        
        ExitButton.GetComponent<RoomExitButton>().RoomToExitTo = Exit;
        ExitButton.transform.parent = LayoutGroup.transform;
    }
    


    // Start is called before the first frame update
    void Start()
    {
        InitialiseRoomToUi(TestRoom);
        SetIntro();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnRoomCompletion();
        }   
    }
}
