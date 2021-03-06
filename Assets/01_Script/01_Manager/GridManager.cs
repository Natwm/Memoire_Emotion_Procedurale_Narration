using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    [SerializeField] private float m_Size;
    [SerializeField] private Vector2 m_GridSize;
    [SerializeField] private GameObject m_TilesPrefabs;

    [Space]
    [Header("List")]
    [SerializeField] private List<GameObject> listOfTile = new List<GameObject>();
    [SerializeField] private List<GameObject> listOfHoveredTile = new List<GameObject>();
    [SerializeField] private List<List<GameObject>> listOfTile2D = new List<List<GameObject>>();
    [SerializeField] private List<TileElt_Behaviours> listOfEvent;
    [SerializeField] private List<TileElt_Behaviours> listOfMovement;
    [SerializeField] private List<Vignette_Behaviours> listOfPosition = new List<Vignette_Behaviours>();

    [Space]
    [SerializeField] private LayerMask m_LayerDetection;

    [Space]
    [Header ("Event")]
    [SerializeField] private EventGenerator m_EventGenerator;

    public List<TileElt_Behaviours> ListOfEvent { get => listOfEvent; set => listOfEvent = value; }
    public List<TileElt_Behaviours> ListOfMovement { get => listOfMovement; set => listOfMovement = value; }
    public List<List<GameObject>> ListOfTile2D { get => listOfTile2D; set => listOfTile2D = value; }
    public List<GameObject> ListOfTile { get => listOfTile; set => listOfTile = value; }
    public List<GameObject> ListOfHoveredTile { get => listOfHoveredTile; set => listOfHoveredTile = value; }
    public Vector2 GridSize { get => m_GridSize; set => m_GridSize = value; }

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
        m_EventGenerator = GetComponent<EventGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateTerrainWithParam(Vector2 _parameters)
    {
        int index = 0;
        for (int i = 0; i < _parameters.x; i++)
        {
            ListOfTile2D.Add(new List<GameObject>());
        }
        for (int x = 0; x < _parameters.x; x++)
        {
            for (int y = 0; y < _parameters.y; y++)
            {
                GameObject tile = Instantiate(m_TilesPrefabs, new Vector3(this.transform.position.x + y, this.transform.position.y - x, -.5f) * 1.2f * m_Size, Quaternion.identity, this.transform);
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
        GridSize = _parameters;
        //m_EventGenerator.GenerateGrid();
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

    public void CheckTile()
    {
        foreach (var item in listOfTile)
        {
            RaycastHit[] hit;
            hit = Physics.BoxCastAll(item.transform.position, transform.localScale / 1.65f, Vector3.back, Quaternion.identity, Mathf.Infinity, m_LayerDetection);
            if (hit.Length > 0)
            {
                item.GetComponent<TileElt_Behaviours>().AssociateEventToTile(hit[0].collider.GetComponent<Vignette_Behaviours>());
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
        Vector2 end = GridSize - Vector2.one;

        if (listOfMovement.Count <= 1 || vignette.VignetteTile.Contains(start) || vignette.VignetteTile.Contains(end))
        {
            return true;
        }


        if (vignette.VignetteTile.Count > 0)
        {
            foreach (var overedTile in vignette.VignetteTile)
            {
                for (int x = (int)-vignette.VignetteShape.x; x <= vignette.VignetteShape.x; x++)
                {
                    for (int y = (int)-vignette.VignetteShape.y; y <= vignette.VignetteShape.y; y++)
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
                                        if (tileEvent.EventAssocier != vignette && tileEvent.EventAssocier != null)
                                        {
                                            return tileEvent.EventAssocier != null;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                //print("error");

                            }
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
        }

        return false;
    }

    public void GetVignetteOrderByNeighbourg()
    {
        List<Vignette_Behaviours> listOfVignetteMovement = new List<Vignette_Behaviours>();
        Vignette_Behaviours checkedVignette;

        GameObject firstTile = listOfTile2D[0][0];
        Vignette_Behaviours firstVignette = firstTile.GetComponent<TileElt_Behaviours>().EventAssocier != null ? firstTile.GetComponent<TileElt_Behaviours>().EventAssocier : null;

        if (firstVignette != null)
        {
            checkedVignette = firstVignette;
            listOfVignetteMovement.Add(firstVignette);
            while (checkedVignette.NextMove != null)
            {
                checkedVignette = checkedVignette.NextMove;
                listOfVignetteMovement.Add(checkedVignette);
            }
            if (checkedVignette.NextMove == null)
            {
                if (!listOfVignetteMovement.Contains(checkedVignette))
                    listOfVignetteMovement.Add(checkedVignette);
            }
        }
        listOfPosition.Clear();

        foreach (var item in listOfVignetteMovement)
        {
            if (item.OnGrid)
                listOfPosition.Add(item);
        }
        /*foreach (var item in listOfVignetteMovement)
        {
            print(item);
        }*/

    }

    public void ClearGame()
    {
        if (GameManager.instance.OrderCharacter.Count >= 0)
        {
            ClearScene();
        }
        else
        {
            ClearAll();
        }
    }

    public void ClearAll()
    {
        List<GameObject> toDelete = new List<GameObject>(listOfTile);
        Vignette_Behaviours[] allVignette = FindObjectsOfType<Vignette_Behaviours>();

        ClearList();

        for (int i = 0; i < allVignette.Length; i++)
        {
            Destroy(allVignette[i].gameObject);
        }

        LevelManager.instance.HandOfVignette.Clear();
        EventGenerator.instance.ClearGrid();
        //CanvasManager.instance.UpdatePageIndicator();
    }

    public void ClearAllList()
    {
        ListOfTile.Clear();
        listOfEvent.Clear();
        listOfTile2D.Clear();
        ListOfHoveredTile.Clear();
        listOfMovement.Clear();
        listOfPosition.Clear();
    }

    public void ClearScene()
    {
        List<GameObject> toDelete = new List<GameObject>(listOfTile);
        Vignette_Behaviours[] allVignette = FindObjectsOfType<Vignette_Behaviours>();

        for (int i = 0; i < allVignette.Length; i++)
        {
            Destroy(allVignette[i].gameObject);
        }

        ClearList();

        LevelManager.instance.HandOfVignette.Clear();

        //CanvasManager.instance.UpdatePageIndicator();
    }

    private void ClearList()
    {
        listOfEvent.Clear();
        ListOfMovement.Clear();
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

    public void AssigneHoveredTile()
    {
        listOfHoveredTile.Clear();
        foreach (var item in FindObjectsOfType<Vignette_Behaviours>())
        {
            foreach (var vignette in item.ListOfAffectedObject)
            {
                listOfHoveredTile.Add(vignette);
            }
        }
    }

    public void CheckIfAllAreConnect()
    {
        int value = 0;
        EventGenerator eventgenerator = FindObjectOfType<EventGenerator>();
        GameObject entryGO = null;
        GameObject exitGO = null;
        GameObject keyGO = null;

        if (eventgenerator.EntryTile.GetComponent<TileElt_Behaviours>().EventAssocier != null)
            entryGO = eventgenerator.EntryTile.GetComponent<TileElt_Behaviours>().EventAssocier.gameObject;

        if (eventgenerator.ExitTile.GetComponent<TileElt_Behaviours>().EventAssocier != null)
            exitGO = eventgenerator.ExitTile.GetComponent<TileElt_Behaviours>().EventAssocier.gameObject;

        if (eventgenerator.keyTile.GetComponent<TileElt_Behaviours>().EventAssocier != null)
            keyGO = eventgenerator.keyTile.GetComponent<TileElt_Behaviours>().EventAssocier.gameObject;

        bool isEntryConnected = eventgenerator.EntryTile.GetComponent<TileElt_Behaviours>().EventAssocier != null;
        bool isExitConnected = eventgenerator.ExitTile.GetComponent<TileElt_Behaviours>().EventAssocier != null;
        bool isKeyConnected = keyGO != null;

        if (isEntryConnected && isExitConnected && isKeyConnected)
        {


            if (entryGO != null && exitGO != null && keyGO != null)
            {
                print("all Are Conected");
                if (entryGO.GetComponent<Vignette_Behaviours>().OnGrid && exitGO.GetComponent<Vignette_Behaviours>().OnGrid && keyGO.GetComponent<Vignette_Behaviours>().OnGrid)
                {
                    GameObject current = entryGO.GetComponent<Vignette_Behaviours>().gameObject;

                    while (current != null)
                    {
                        if (current == entryGO)
                        {
                            value++;
                        }

                        if (current == exitGO)
                        {
                            value++;
                        }

                        if (current == keyGO)
                        {
                            value++;
                        }

                        if (current.GetComponent<Vignette_Behaviours>().NextMove != null)
                            if (current.GetComponent<Vignette_Behaviours>().NextMove.gameObject != current)
                                current = current.GetComponent<Vignette_Behaviours>().NextMove.gameObject;
                            else
                                current = null;
                        else
                            current = null;
                    }
                }
            }
        }

        if (value >= 3)
        {
            CanvasManager.instance.SetActiveMoveButton(true);
            //SoundManager.instance.PlaySound_ResolutionAvailable();
        }
        else
        {
            CanvasManager.instance.SetActiveMoveButton(false);
        }
    }

}
