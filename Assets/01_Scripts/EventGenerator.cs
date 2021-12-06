using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventGenerator : MonoBehaviour
{
    public static EventGenerator instance;

    GridManager m_GridManager;
    public List<GameObject> occupiedTiles;
    public int tilenumber;
    public Vector2Int distribution;

    public List<GameObject> allGraphics;


    public GameObject[] modifiers;
    public CaseContener_SO[] modifiersEvent;
    public GameObject[] doors;
    GameObject[] stamina;
    GameObject[] cartes;
    GameObject[] pvs;

    GameObject keyTile;

    public GameObject key;

    GameObject entryTile;
    GameObject exitTile;

    public Vector2Int gridSize;

    public GameObject EntryTile { get => entryTile; set => entryTile = value; }
    public GameObject ExitTile { get => exitTile; set => exitTile = value; }


    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : EventGenerator");
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_GridManager = GridManager.instance;
       // GenerateGrid();
    }

    // Get All LeftTiles
    // Get All RightTils
    /* Get A Tile in the list.
    //      > CANNOT BE FIRST
            > CANNOT BE LAST
    
        Get a random Tile
        is the tile alreadyoccupied

    */
    // Update is called once per frame
    public void DetermineDoors(bool isRandom)
    {
        if (isRandom)
        {
            // Tableaux de tiles pour déterminer le bord droit et le bord gauche
            GameObject[] leftTiles = new GameObject[gridSize.x];
            GameObject[] rightTiles = new GameObject[gridSize.x];
            int index_Left = 0;
            int index_Right = 0;
            for (int i = 0; i < GridManager.instance.ListOfTile.Capacity; i++)
            {
                TileElt_Behaviours tile = GridManager.instance.ListOfTile[i].GetComponent<TileElt_Behaviours>();
                //LeftTile
                if (tile.Tileposition.y == 0)
                {
                    leftTiles[index_Left] = tile.gameObject;
                    index_Left++;

                }
                if (tile.Tileposition.y == gridSize.y - 1)
                {
                    rightTiles[index_Right] = tile.gameObject;
                    index_Right++;
                }
            }

            int RandomEntry = Random.Range(0, leftTiles.Length);
            int RandomExit = Random.Range(0, rightTiles.Length);

            EntryTile = leftTiles[RandomEntry];
            ExitTile = rightTiles[RandomExit];
        }
        else
        {
            EntryTile = GridManager.instance.ListOfTile[0]; 
            ExitTile = GridManager.instance.ListOfTile[GridManager.instance.ListOfTile.Count-1];
        }
        GameObject newEntry = Instantiate(doors[0], EntryTile.transform);
        newEntry.transform.localPosition = Vector3.zero;
        GameObject newExit = Instantiate(doors[1], ExitTile.transform);
        newExit.transform.localPosition = Vector3.zero;
        allGraphics.Add(newEntry);
        allGraphics.Add(newExit);
        
    }

    public void PopulateTiles(int iteration)
    {
        ClearGrid();
        DetermineDoors(false);
        foreach (GameObject item in occupiedTiles)
        {
            item.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        
        for (int i = 0; i < iteration; i++)
        {
            occupiedTiles.Add(GetRandomClearTile());
        }
        print("occupiedTiles.Count " + occupiedTiles.Count);
    }

    public void DetermineKey()
    {
        int randomInt = Random.Range(1, GridManager.instance.ListOfTile.Count - 1);
        GameObject keyObject = GridManager.instance.ListOfTile[randomInt];
        keyObject.GetComponent<MeshRenderer>().material.color = Color.green;

        keyTile = keyObject;
        SpawnGraphics(key, keyTile);
    }

    public GameObject GetRandomClearTile()
    {
        int randomInt = Random.Range(1, GridManager.instance.ListOfTile.Count-1);
        GameObject potentialTile = GridManager.instance.ListOfTile[randomInt];
        if (potentialTile != keyTile)
        {
            if (potentialTile != ExitTile && potentialTile != EntryTile)
            {
                foreach (GameObject item in occupiedTiles)
                {
                    if (item == potentialTile)
                    {
                        // Debug.Log("Hit Already Occupied Tile : " + item.name);

                        return GetRandomClearTile();
                    }
                    
                }
            }
            else
            {
                Debug.Log("Hit Door");
                return GetRandomClearTile();
            }
        }
        else
        {
            return GetRandomClearTile();
        }
        potentialTile.GetComponent<MeshRenderer>().material.color = Color.yellow;
        return potentialTile;
        
    }

    public void DetermineTileType()
    {
        
        for (int i = 0; i < occupiedTiles.Count-2; i++)
        {
            print("i =  " + i + "   " + occupiedTiles[i]);
            GameObject tmp = SpawnGraphics(DetermineEventType(occupiedTiles[i]), occupiedTiles[i]);
            allGraphics.Add(tmp);
        }
        GameObject keyGraphics = Instantiate(key, occupiedTiles[occupiedTiles.Count-1].transform);
        keyGraphics.transform.localPosition = Vector3.zero;
        allGraphics.Add(keyGraphics);
    }

    public GameObject SpawnGraphics(GameObject objToSpawn, GameObject tile)
    {
        //Debug.Log("SpawnGraphic_CALL " + tile.name);
        GameObject newGraphic = Instantiate(objToSpawn, tile.transform);
        //print("SpawnGraphic_CALL newGraphic = " + newGraphic.transform.parent.gameObject.name);
        newGraphic.transform.localPosition = Vector3.zero;
        return newGraphic;
    }

    public CaseContener_SO DetermineTypeFromRoom(Room_So _roomTiles)
    {
        int RandomListIndex = Random.Range(0, _roomTiles.PossibleTiles.Count - 1);
        return _roomTiles.PossibleTiles[RandomListIndex];
    }

    ///RÉGLER LE BORDEL LA DEDANS
    public GameObject DetermineEventType(GameObject tileToModify)
    {
        //Debug.Log("EventType_CALL " + tileToModify.name);
        int RandomType = Random.Range(0, modifiers.Length);
        GameObject tileType=null;
        //Debug.Log("TILETYPE:  " + RandomType);
        
        tileType = modifiers[RandomType];
        tileToModify.GetComponent<Case_Behaviours>().CaseEffects = modifiersEvent[RandomType];
        //Debug.Break();
        return tileType;
    }

    public void ClearGrid()
    {
       
        GameObject[] listToDestroy = new GameObject[allGraphics.Capacity];
        for (int i = 0; i < allGraphics.Count; i++)
        {
            listToDestroy[i] = allGraphics[i];
        }
        allGraphics.Clear();

        foreach (GameObject item in occupiedTiles)
        {
            print("Item name destroyed = " + item.name);
            ElementBehaviours dest = item.GetComponent<ElementBehaviours>();
            Destroy(dest);
        }

        occupiedTiles.Clear();
        foreach (GameObject item in listToDestroy)
        {
            Destroy(item);
        }
    }

    bool coinToss()
    {
        int coin = Random.Range(0, 2);
        if (coin == 0)
        {
            //Positif
            return true;
        }
        else
        {
            return false;
            //Negatif
        }
    }

    public void GenerateGrid()
    {
        PopulateTiles(tilenumber);
        DetermineTileType();

        foreach (GameObject item in GridManager.instance.ListOfTile)
        {
            
            if (item.transform.childCount == 0)
            {
                print(item.name + "item.transform.childCount == 0  = " + (item.transform.childCount == 0));
                item.GetComponent<Case_Behaviours>().CaseEffects = null;
                print(item.GetComponent<Case_Behaviours>().CaseEffects);
            }
        }

        //key.GetComponent<Case_Behaviours>().CaseEffects = null;
    }

    void Update()
    {        
    }
}
