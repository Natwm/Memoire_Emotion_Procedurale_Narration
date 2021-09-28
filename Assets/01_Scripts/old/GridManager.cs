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


    public List<TileElt_Behaviours> ListOfEvent { get => listOfEvent; set => listOfEvent = value; }
    public List<GameObject> ListOfTile { get => listOfTile; set => listOfTile = value; }
    public List<TileElt_Behaviours> ListOfMovement { get => listOfMovement; set => listOfMovement = value; }

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
    }

    public void CheckTile()
    {
        foreach (var item in listOfTile)
        {
            RaycastHit[] hit;
            hit = Physics.BoxCastAll(item.transform.position,transform.localScale/1.65f,Vector3.back,Quaternion.identity,Mathf.Infinity, m_LayerDetection);
            print("name = " + item.name +" "+hit.Length);
            if (hit.Length > 0)
            {
                item.GetComponent<TileElt_Behaviours>().AssociateEventToTile(hit[0].collider.GetComponent<Bd_Elt_Behaviours>());

                switch (hit[0].collider.GetComponent<Bd_Elt_Behaviours>().Value.HealthEffect)
                {
                    case Carte_SO.Status.BONUS:
                        item.GetComponent<MeshRenderer>().material.color = Color.blue;
                        break;
                    case Carte_SO.Status.MALUS:
                        item.GetComponent<MeshRenderer>().material.color = Color.red;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                item.GetComponent<TileElt_Behaviours>().EventAssocier = null;
                item.GetComponent<MeshRenderer>().material.color = Color.white;
                ListOfEvent.Remove(item.GetComponent<TileElt_Behaviours>());

                /*if (hit[0].collider.GetComponent<Bd_Elt_Behaviours>().ListOfAffectedObject.Count > 0 && hit[0].collider.GetComponent<Bd_Elt_Behaviours>().ListOfAffectedObject.Contains(item))
                {
                    
                    hit[0].collider.GetComponent<Bd_Elt_Behaviours>().ListOfAffectedObject.Remove
                }*/
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

        print(FindObjectsOfType<Bd_Elt_Behaviours>().Length);

        foreach (var item in FindObjectsOfType<Bd_Elt_Behaviours>())
        {
            Destroy(item.gameObject);
        }
        LevelManager.instance.SpawnObject();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position,)
    }

}
