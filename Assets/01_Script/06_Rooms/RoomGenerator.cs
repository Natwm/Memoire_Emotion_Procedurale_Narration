using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomGenerator : MonoBehaviour
{
    // Test Variables
    public static RoomGenerator instance;

    // RoomManagement
    public Room_SO CurrentRoom;
    public RoomExit GoToRoom;
    List<Room_SO> CompletedRoom;    

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : RoomGenerator");
        else
            instance = this;

        //
        //InitializeToCanvas();
        CompletedRoom = new List<Room_SO>();
    }

    public void Start()
    {
       
    }
  
    public void GenerateRoom(Room_SO RoomToCreate)
    {
        if (RoomToCreate.Effect_Of_Room != Room_SO.CustomEffect.NEGO)
        {
            int distribution = (RoomToCreate.Room_Size.x * RoomToCreate.Room_Size.y) / 3;
            GridManager.instance.ListOfTile.Clear();
            GridManager.instance.ListOfTile2D.Clear();
            GridManager.instance.CreateTerrainWithParam(RoomToCreate.Room_Size);
            EventGenerator.instance.DetermineDoors(false);
            EventGenerator.instance.DetermineKey();
            EventGenerator.instance.PopulateTiles(RoomToCreate.ObjectDistribution);
            RoomToCreate.SetColorPalette();
            foreach (GameObject item in EventGenerator.instance.occupiedTiles)
            {
                int randomInt = Random.Range(0, RoomToCreate.PossibleTiles.Count);
                item.GetComponent<Case_Behaviours>().CaseEffects = RoomToCreate.PossibleTiles[randomInt].SpawnAsset(item);
            }
            foreach (CaseContener_SO item in RoomToCreate.GaranteedTiles)
            {
                GameObject newcase = EventGenerator.instance.GetRandomClearTile();

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
            LevelManager.instance.SpawnObject(GameManager.instance.CurrentCharacter.AssignedElement.BaseHand);
        }
        
        RoomToCreate.ApplyEffect();
        
    }

    public void SwitchToRoom(Room_SO _roomToGo)
    {
        CurrentRoom = _roomToGo;
    }


    public bool checkCompletion(Room_SO _current)
    {
        foreach (Room_SO item in CompletedRoom)
        {
            if (_current == item)
            {
                Debug.Log("COMPLETED");
                return true;
            }
        }
        return false;
    }

    

    public void OnRoomCompletion()
    {
        if (CurrentRoom.Effect_Of_Room == Room_SO.CustomEffect.HUB && !checkCompletion(CurrentRoom))
        {
            CompletedRoom.Add(CurrentRoom);
        }
        CanvasManager.instance.FadeIn();
        CanvasManager.instance.SetOutro();
        EventGenerator.instance.TempClearGrid();
    }

    public void InitialiseGame()
    {       
        CanvasManager.instance.InitialiseRoomToUi(CurrentRoom);
    }
    
    public void StartRoom(Room_SO RoomToStart)
    {

    }


    // Start is called before the first frame update
   

  
}
