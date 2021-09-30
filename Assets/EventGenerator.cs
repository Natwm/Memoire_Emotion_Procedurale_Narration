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

    public GameObject[] stamina;
    public GameObject[] cartes;
    public GameObject[] pvs;

    public GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        
        kb = InputSystem.GetDevice<Keyboard>();
       // GenerateGrid();
    }

    /* Get A Tile in the list.
    //      > CANNOT BE FIRST
            > CANNOT BE LAST
    
        Get a random Tile
        is the tile alreadyoccupied

    */
    // Update is called once per frame
    public void PopulateTiles(int iteration)
    {
        ClearGrid();
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
        foreach (GameObject item in occupiedTiles)
        {
            if (item == potentialTile)
            {
                Debug.Log("Hit Already Occupied Tile : " + item.name);
                
                return GetRandomClearTile();
            }
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
        int RandomType = Random.Range(0, 3);
        GameObject tileType=null;
        if (RandomType == 0)
        {
            // Stamina
            if (coinToss())
            {
                //Positif
                tileType = stamina[0];
                tileToModify.AddComponent<ElementBehaviours_Stamina>().AmountOfStamina = 1;
            }
            else
            {
                tileType = stamina[1];
                tileToModify.AddComponent<ElementBehaviours_Stamina>().AmountOfStamina = -1;
            }
        }
        else if (RandomType == 1)
        {
            // Cartes
            if (coinToss())
            {
                tileType = cartes[0];
                tileToModify.AddComponent<ElementBehaviours_Draw>().AmountOfCardToDraw = 1;
            }
            else
            {
                tileType = cartes[1];
                tileToModify.AddComponent<ElementBehaviours_Draw>().AmountOfCardToDraw = -1;
            }
        }
        else if (RandomType == 2)
        {
            // PV
            if (coinToss())
            {
                tileType = pvs[0];
                tileToModify.AddComponent<ElementBehaviours_Heal>().AmountOfHeal= 1;
            }
            else
            {
                tileType = pvs[1];
                tileToModify.AddComponent<ElementBehaviours_Heal>().AmountOfHeal = -1;
            }
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
            FindObjectOfType<Porte>().SetDoor(false);
        }
    }
}
