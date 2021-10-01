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

    [SerializeField] private LayerMask m_LayerDetection;

    [SerializeField] private IModifier a;

    public List<TileElt_Behaviours> ListOfEvent { get => listOfEvent; set => listOfEvent = value; }
    public List<GameObject> ListOfTile { get => listOfTile; set => listOfTile = value; }
    public List<TileElt_Behaviours> ListOfMovement { get => listOfMovement; set => listOfMovement = value; }

    EventGenerator m_EventGenerator;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GridManager");
        else
            instance = this;
        m_EventGenerator = GetComponent<EventGenerator>();
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
                GameObject tile = Instantiate(m_TilesPrefabs, new Vector3(y, -x, -.5f) * 1.2f * m_Size, Quaternion.identity, this.transform);
                tile.transform.localScale *= m_Size;

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
            hit = Physics.BoxCastAll(item.transform.position,transform.localScale/1.65f,Vector3.back,Quaternion.identity,Mathf.Infinity, m_LayerDetection);
            if (hit.Length > 0)
            {
                item.GetComponent<TileElt_Behaviours>().AssociateEventToTile(hit[0].collider.GetComponent<Bd_Elt_Behaviours>());

                if (hit[0].collider.GetComponent<Bd_Elt_Behaviours>().Value.Health > 0)
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

    public void SortList()
    {
        ListOfMovement = ListOfEvent.OrderByDescending(x => x.Index).ToList();
        ListOfMovement.Reverse();
    }

    public void ClearScene()
    {
        List<GameObject> toDelete = new List<GameObject>(listOfTile);

        foreach (var item in FindObjectsOfType<Bd_Elt_Behaviours>())
        {
            Destroy(item.gameObject);
        }
        PlayerManager.instance.SetUp();
        LevelManager.instance.SpawnObject(PlayerManager.instance.AmountOfCardToDraw);
        m_EventGenerator.GenerateGrid();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position,)
    }

}
