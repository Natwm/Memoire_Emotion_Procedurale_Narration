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
    public RoomExit RoomToExitTo { get => RoomToExitTo; set => RoomToExitTo = value; }
    

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : RoomGenerator");
        else
            instance = this;
    }

    public void GenerateRoom(Room_So RoomToCreate)
    {
        int distribution = (RoomToCreate.Room_Size.x * RoomToCreate.Room_Size.y) / 3;
        EventGenerator.instance.ClearGrid();
        GridManager.instance.CreateTerrainWithParam(TestRoom.Room_Size);
        EventGenerator.instance.DetermineDoors(false);
        EventGenerator.instance.DetermineKey();
        EventGenerator.instance.PopulateTiles(TestRoom.ObjectDistribution);
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
    }

    public void FadeIn()
    {
        RoomPanel.GetComponent<Animator>().SetTrigger("Reveal");
        Intro.SetActive(true);
        Outro.SetActive(true);
    }

    public void FadeOut()
    {
        RoomPanel.GetComponent<Animator>().SetTrigger("FadeOut");
        Intro.SetActive(false);
        Outro.SetActive(false);
    }

    public void InitialiseRoomToUi(Room_So Room)
    {
        TitleText.text = Room.RoomName;
        IntroText.text = Room.Room_IntroText;
        OutroText.text = Room.Room_OutroText;
        for (int i = 0; i < Room.PossibleExits.Length; i++)
        {
            CreateExitButton(Room.PossibleExits[i]);
        }
    }

    public void CreateExitButton(RoomExit Exit)
    {
        GameObject ExitButton = GameObject.Instantiate(BaseOutroButton);
        ExitButton.GetComponent<TMP_Text>().text = Exit.Exit_Text;
        ExitButton.GetComponent<RoomExitButton>().RoomToExitTo = Exit;
        ExitButton.transform.parent = LayoutGroup.transform;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        InitialiseRoomToUi(TestRoom);
        GenerateRoom(TestRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
