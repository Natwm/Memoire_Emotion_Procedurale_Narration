﻿using System.Collections;
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
        m_GridManager = GetComponent<GridManager>();
        kb = InputSystem.GetDevice<Keyboard>();
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
        occupiedTiles.Clear();
        for (int i = 0; i < iteration; i++)
        {
            occupiedTiles.Add(GetRandomClearTile());
        }
    }


    public GameObject GetRandomClearTile()
    {
        int randomInt = Random.Range(1, m_GridManager.ListOfTile.Capacity-1);
        GameObject potentialTile = m_GridManager.ListOfTile[randomInt];
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
            GameObject tmp = SpawnGraphics(DetermineEventType(), occupiedTiles[i]);
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

    public GameObject DetermineEventType()
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
            }
            else
            {
                tileType = stamina[1];
            }
        }
        else if (RandomType == 1)
        {
            // Cartes
            if (coinToss())
            {
                tileType = cartes[0];
            }
            else
            {
                tileType = cartes[1];
            }
        }
        else if (RandomType == 2)
        {
            // PV
            if (coinToss())
            {
                tileType = pvs[0];
            }
            else
            {
                tileType = pvs[1];
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

    void Update()
    {
        if (kb.spaceKey.wasReleasedThisFrame)
        {
           
            PopulateTiles(tilenumber);
            DetermineTileType();
        }
        if (kb.rKey.wasReleasedThisFrame)
        {
            ClearGrid();
        }
    }
}