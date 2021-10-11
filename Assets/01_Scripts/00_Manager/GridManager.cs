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
    [SerializeField] private List<TileElt_Behaviours> listOfMovement;
    [SerializeField] private List<List<GameObject>> listOfTile2D = new List<List<GameObject>>();

    [SerializeField] private LayerMask m_LayerDetection;

    [SerializeField] private IModifier a;

    public List<TileElt_Behaviours> ListOfEvent { get => listOfEvent; set => listOfEvent = value; }
    public List<GameObject> ListOfTile { get => listOfTile; set => listOfTile = value; }
    public List<TileElt_Behaviours> ListOfMovement { get => listOfMovement; set => listOfMovement = value; }
    public List<List<GameObject>> ListOfTile2D { get => listOfTile2D; set => listOfTile2D = value; }

    EventGenerator m_EventGenerator;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GridManager");
        else
            instance = this;
        m_EventGenerator = GetComponent<EventGenerator>();
        m_EventGenerator.gridSize = new Vector2Int(Mathf.FloorToInt(m_GridSize.x), Mathf.FloorToInt(m_GridSize.y));
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_GridSize.x; i++)
        {
            ListOfTile2D.Add(new List<GameObject>());
        }
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
                GameObject tile = Instantiate(m_TilesPrefabs, new Vector3(y, -x, -.5f) * 1.2f * m_Size, Quaternion.identity, this.transform);
                tile.transform.localScale *= m_Size;

                ListOfTile2D[x].Add(tile);

                TileElt_Behaviours tileBehaviours = tile.GetComponent<TileElt_Behaviours>();
                tileBehaviours.Tileposition = new Vector2(x, y);
                tileBehaviours.Index = index;

                tile.name += tile.transform.position.ToString();

                index++;
                ListOfTile.Add(tile);
            }
        }
        m_EventGenerator.GenerateGrid();
    }

    public void CheckTile()
    {
        foreach (var item in listOfTile)
        {
            RaycastHit[] hit;
            hit = Physics.BoxCastAll(item.transform.position, transform.localScale / 1.65f, Vector3.back, Quaternion.identity, Mathf.Infinity, m_LayerDetection);
            if (hit.Length > 0)
            {
                item.GetComponent<TileElt_Behaviours>().AssociateEventToTile(hit[0].collider.GetComponent<Vignette_Behaviours>());

                if (hit[0].collider.GetComponent<Vignette_Behaviours>().MyEvent.Happy_Sad > 0)
                {
                    item.GetComponent<MeshRenderer>().material.color = Color.blue;
                }
                else
                {
                    item.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
            else
            {
                item.GetComponent<TileElt_Behaviours>().EventAssocier = null;
                item.GetComponent<MeshRenderer>().material.color = Color.white;
                ListOfEvent.Remove(item.GetComponent<TileElt_Behaviours>());

            }

        }
    }

    public bool DoesVignetteIsValid(Vignette_Behaviours vignette)
    {
        bool test = false;
        Vector2 start = new Vector2(0, 0);
        Vector2 end = m_GridSize - Vector2.one;

        if (listOfMovement.Count <= 1 || vignette.VignetteTile.Contains(start) || vignette.VignetteTile.Contains(end))
            return true;

        foreach (var overedTile in vignette.VignetteTile)
        {
            for (int x = (int)-vignette.VignetteShape.x; x <= 1; x++)
            {
                for (int y = (int)-vignette.VignetteShape.y; y <= 1; y++)
                {
                    Vector2 tilePos = new Vector2(overedTile.x + x, overedTile.y + y);
                    //Logique pour monter a l'étage suppérieur
                    if (tilePos.y < 0 && tilePos.x >= 0)
                    {
                        tilePos.Set(tilePos.x - 1, 3);
                        test = true;
                    }
                    //Calcule si la distance est de 1
                    if (VectorMethods.ManhattanDistance(overedTile, tilePos, 1))
                    {
                        try
                        {
                            if (tilePos.x < GridManager.instance.ListOfTile2D.Count && tilePos.y < GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)].Count)
                            {
                                GameObject tile = GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)][Mathf.RoundToInt(tilePos.y)];
                                TileElt_Behaviours tileEvent;

                                if (tile.TryGetComponent<TileElt_Behaviours>(out tileEvent))
                                {
                                    if (tileEvent.EventAssocier != vignette)
                                    {
                                        return tileEvent.EventAssocier != null;
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                    // si il est passé a l'étage suppérieur 
                    else if (test && tilePos.x >= 0 && tilePos.y >= 0)
                    {

                        if (GridManager.instance.ListOfTile2D.Count > Mathf.RoundToInt(tilePos.x))
                        {
                            if (GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)].Count > Mathf.RoundToInt(tilePos.y))
                            {
                                GameObject tile = GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)][Mathf.RoundToInt(tilePos.y)];
                                TileElt_Behaviours tileEvent;

                                if (tile != null)
                                {
                                    if (tile.TryGetComponent<TileElt_Behaviours>(out tileEvent))
                                    {
                                        if (tileEvent.EventAssocier != vignette && tileEvent.EventAssocier != null)
                                        {
                                            tileEvent.EventAssocier.NextMove = vignette;
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        test = false;
                    }
                }

            }
        }
        return false;
    }

    public void SortList()
    {
        listOfMovement.Clear();
        Vignette_Behaviours currentVignette = null;
        TileElt_Behaviours currentTile = null;
        foreach (var item in listOfEvent)
        {
            if (currentVignette == null)
            {
                currentTile = item;
                currentVignette = item.EventAssocier;
                listOfMovement.Add(currentTile);
            }
            else if (item.EventAssocier != currentVignette)
            {
                currentTile = item;
                currentVignette = item.EventAssocier;
                listOfMovement.Add(currentTile);
            }
        }

        ListOfMovement = ListOfMovement.OrderByDescending(x => x.Index).ToList();
        ListOfMovement.Reverse();
    }

    public void ClearScene()
    {
        List<GameObject> toDelete = new List<GameObject>(listOfTile);

        foreach (var item in FindObjectsOfType<Vignette_Behaviours>())
        {
            Destroy(item.gameObject);
        }
        PlayerManager.instance.HandOfVignette.Clear();
        LevelManager.instance.SpawnObject(PlayerManager.instance.AmountOfCardToDraw);
        m_EventGenerator.GenerateGrid();
        PlayerManager.instance.SetUp();
    }

    void ShowDebugTile()
    {
        string a = "";
        foreach (var item in ListOfTile2D)
        {
            a += "\n";
            foreach (var obj in item)
            {
                a += obj.name + " | ";
            }
        }
        print(a);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position,)
    }

}
