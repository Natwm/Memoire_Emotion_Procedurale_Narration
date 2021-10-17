using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventGenerator : MonoBehaviour
{
    GridManager m_GridManager;
    public List<GameObject> occupiedTiles;
    public int tilenumber;
    Keyboard kb;
    public Vector2Int distribution;

    public List<GameObject> allGraphics;


    public GameObject[] modifiers;
    public GameObject[] doors;
    GameObject[] stamina;
    GameObject[] cartes;
    GameObject[] pvs;
    

    public GameObject key;

    GameObject entryTile;
    GameObject exitTile;

    public Vector2Int gridSize;

    public GameObject EntryTile { get => entryTile; set => entryTile = value; }
    public GameObject ExitTile { get => exitTile; set => exitTile = value; }

    // Start is called before the first frame update
    void Start()
    {
        kb = InputSystem.GetDevice<Keyboard>();
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
    public void DetermineDoors()
    {
        GameObject[] leftTiles = new GameObject[gridSize.x];
        GameObject[] rightTiles = new GameObject[gridSize.x];
        int index_Left=0;
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
            if (tile.Tileposition.y == gridSize.y-1)
            {
                rightTiles[index_Right] = tile.gameObject;
                index_Right++;
            }
        }

        int RandomEntry = Random.Range(0, leftTiles.Length);
        int RandomExit = Random.Range(0, rightTiles.Length);

        EntryTile = leftTiles[RandomEntry];
        ExitTile = rightTiles[RandomExit];

        GameObject newEntry = Instantiate(doors[0], EntryTile.transform);
        newEntry.transform.localPosition = Vector3.zero;
        GameObject newExit = Instantiate(doors[1], ExitTile.transform);
        newExit.transform.localPosition = Vector3.zero;
        allGraphics.Add(newEntry);
        allGraphics.Add(newExit);
        Debug.Log(newEntry.name);
        Debug.Log(newExit.name);
    }

    public void PopulateTiles(int iteration)
    {
        ClearGrid();
        DetermineDoors();

        foreach (GameObject item in occupiedTiles)
        {
            item.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        
        for (int i = 0; i < iteration; i++)
        {
            occupiedTiles.Add(GetRandomClearTile());
        }
    }


    public GameObject GetRandomClearTile()
    {
        int randomInt = Random.Range(1, GridManager.instance.ListOfTile.Capacity-1);
        GameObject potentialTile = GridManager.instance.ListOfTile[randomInt];
        if (potentialTile != ExitTile && potentialTile != EntryTile)
        {
            foreach (GameObject item in occupiedTiles)
            {
                if (item == potentialTile)
                {
                    Debug.Log("Hit Already Occupied Tile : " + item.name);

                    return GetRandomClearTile();
                }
            }
        }
        else
        {
            Debug.Log("Hit Door");
            return GetRandomClearTile();
        }
        potentialTile.GetComponent<MeshRenderer>().material.color = Color.yellow;
        return potentialTile;
    }

    public void DetermineTileType()
    {
        
        for (int i = 0; i < occupiedTiles.Count-1; i++)
        {
            GameObject tmp = SpawnGraphics(DetermineEventType(occupiedTiles[i]), occupiedTiles[i]);
            allGraphics.Add(tmp);
            
        }
        GameObject keyGraphics = Instantiate(key, occupiedTiles[occupiedTiles.Count-1].transform);
        keyGraphics.transform.localPosition = Vector3.zero;
        allGraphics.Add(keyGraphics);

    }

    public GameObject SpawnGraphics(GameObject objToSpawn, GameObject tile)
    {
        GameObject newGraphic = Instantiate(objToSpawn, tile.transform);
        newGraphic.transform.localPosition = Vector3.zero;
        return newGraphic;
    }


    ///RÉGLER LE BORDEL LA DEDANS
    public GameObject DetermineEventType(GameObject tileToModify)
    {
        int RandomType = Random.Range(0, modifiers.Length);
        GameObject tileType=null;
        if (RandomType == 0)
        {
            
                tileType = modifiers[0];
                tileToModify.AddComponent<ElementBehaviours_Stamina>().AmountOfAngryOrfear = 1;
        }
        else if (RandomType == 1)
        {
            tileType = modifiers[1];
            tileToModify.AddComponent<ElementBehaviours_Stamina>().AmountOfAngryOrfear = 1;
        }
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
    }

    void Update()
    {
     
        if (kb.jKey.wasReleasedThisFrame)
        {
            FindObjectOfType<Porte>().SetDoor(true);
        }
        if (kb.kKey.wasReleasedThisFrame)
        {
            DetermineDoors();
        }
    }
}
