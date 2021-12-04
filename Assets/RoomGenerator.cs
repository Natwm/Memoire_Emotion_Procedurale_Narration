using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomGenerator : MonoBehaviour
{
    public Room_So TestRoom;
    public static RoomGenerator instance;
    
    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : EventGenerator");
        else
            instance = this;
    }

    public void GenerateRoom(Room_So RoomToCreate)
    {
        int distribution = (RoomToCreate.Room_Size.x * RoomToCreate.Room_Size.y) / 4;
        EventGenerator.instance.ClearGrid();
        GridManager.instance.CreateTerrainWithParam(TestRoom.Room_Size);
        EventGenerator.instance.DetermineDoors(false);
        EventGenerator.instance.PopulateTiles(distribution);
        EventGenerator.instance.DetermineTypeFromRoom(RoomToCreate);


        //Get Room List Of Obj
        
        //Spawn List on Obj
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateRoom(TestRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
