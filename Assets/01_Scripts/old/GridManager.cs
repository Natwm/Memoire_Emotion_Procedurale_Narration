using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    [SerializeField] private float m_Size;
    [SerializeField] private Vector2 m_GridSize;
    [SerializeField] private GameObject m_TilesPrefabs;

    [SerializeField] private List<GameObject> listOfTile = new List<GameObject>();
    [SerializeField] private List<TileElt_Behaviours> listOfEvent;


    public List<TileElt_Behaviours> ListOfEvent { get => listOfEvent; set => listOfEvent = value; }
    public List<GameObject> ListOfTile { get => listOfTile; set => listOfTile = value; }

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GridManager");
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateTerrain()
    {
        int index = 0;
        for (int x = 0; x < m_GridSize.x; x++)
        {
            for (int y = 0; y < m_GridSize.y; y++)
            {
                GameObject tile = Instantiate(m_TilesPrefabs, new Vector3(y, -x, -.5f) * 2 * m_Size, Quaternion.identity, this.transform);
                tile.transform.localScale *= m_Size;

                TileElt_Behaviours tileBehaviours = tile.GetComponent<TileElt_Behaviours>();
                tileBehaviours.Tileposition = new Vector2(x, y);
                tileBehaviours.Index = index;
                index++;
                ListOfTile.Add(tile);
            }
        }
    }

    void CheckTile()
    {

    }

    public void SortList()
    {
        ListOfEvent = ListOfEvent.OrderByDescending(x => x.Index).ToList();
        listOfEvent.Reverse();
    }

}
