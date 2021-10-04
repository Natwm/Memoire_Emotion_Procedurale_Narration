using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bd_Elt_Behaviours : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    #region param
    [SerializeField] private float m_RadiusDetection;
    [SerializeField] private LayerMask m_LayerDetection;
    [SerializeField] private bool m_IsLook;
    [SerializeField] private List<GameObject> listOfAffectedObject = new List<GameObject>();
    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private Carte_SO value;
    private Vector3 offset;

    [Space]
    [Header("Param")]
    [SerializeField] private bool onGrid = false;
    [SerializeField] private Vector2 vignetteShape;
    [SerializeField] private float raycastSize = 65f;
    [SerializeField] private int VignetteSize = 1 ;
    [SerializeField] private List<Vector2> vignetteTilePosition = new List<Vector2> ();
    [SerializeField] private List<Vector2> vignetteTile = new List<Vector2>();

    [Space]
    [Header ("Text Status")]
    [SerializeField] private TMPro.TMP_Text staminaText;
    [SerializeField] private TMPro.TMP_Text healthText;
    [SerializeField] private TMPro.TMP_Text vignetteText;

    [Space]
    [Header("event")]
    [SerializeField] private EventContener myEvent;

    [Space]
    [Header("Movement")]
    [SerializeField] private Bd_Elt_Behaviours nextMove;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.Drag;

        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);

        myEvent = GetComponent<EventContener>();
        SetUpCard();
    }

    private void OnDragDelegate(PointerEventData data)
    {
        //Create a ray going from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(data.position);
        
        //Calculate the distance between the Camera and the GameObject, and go this distance along the ray
        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));

        rayPoint += offset;

        rayPoint.Set(rayPoint.x, rayPoint.y, -2f);
        //Move the GameObject when you drag it
        if(!m_IsLook)
            transform.position = rayPoint;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Bd_Elt_Behaviours CheckNextMove()
    {
        if (onGrid)
        {
            print("Check by : " + this.name);
            foreach (var overedTile in vignetteTile)
            {
                //print("aaaa" + " Shape = (" + (vignetteShape.x+1)+","+ (vignetteShape.y+1));
                for (int x = 0; x < vignetteShape.x +1; x++)
                {
                    for (int y = 0; y < vignetteShape.y +1; y++)
                    {
                        Vector2 tilePos = new Vector2((overedTile.x + x)%4, (overedTile.y + y) % 4);
                        //print("______________ first  : " + tilePos);
                        if (VectorMethods.ManhattanDistance(overedTile, tilePos, 1) && !vignetteTile.Contains(tilePos))
                        {
                            print("______________" + tilePos);
                            try
                            {
                                GameObject tile = GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)][Mathf.RoundToInt(tilePos.y)];
                                TileElt_Behaviours tileEvent;
                                
                                if (tile.TryGetComponent<TileElt_Behaviours>(out tileEvent))
                                {
                                    print(tileEvent.EventAssocier.gameObject);
                                    return tileEvent.EventAssocier;
                                }
                            }
                            catch { }
                        }

                        //print(GridManager.instance.ListOfTile2D[Mathf.RoundToInt(overedTile.x + x)][Mathf.RoundToInt(overedTile.y + y)]);
                    }
                }
            }
        }
        
        return null;
    }

    public void GetNextMove()
    {
        NextMove = CheckNextMove();
        if(NextMove !=null)
            print("Next move is :    " + NextMove.gameObject);
    }

    public void SetUpCard()
    {
        cardImage.sprite = value.CardSprite;
        MyEvent.SetUpEvent(value);
        SetUpUI();
    }

    public void SetUpCard(IModifier modifier)
    {
        print("SetUpCard with Modifier");
        modifier.CollectElement(myEvent);
        print("SetUpCard");
        SetUpUI();
    }

    //quand tu drop, ne mets pas a jour le script event
    private void SetUpUI()
    {
        if(myEvent.Health >= 0)
        {
            healthText.text = "+" + MyEvent.Health.ToString();
            healthText.color = value.Health >0? Color.red : Color.black;
        }

        else
        {
            healthText.text = MyEvent.Health.ToString();
        }

        if (myEvent.Movement >= 0)
        {
            staminaText.text = value.Movement >= 0 ? "+" + MyEvent.Movement.ToString() :  MyEvent.Movement.ToString();
            staminaText.color = value.Movement >= 0 ? Color.red : Color.black;
        }

        else
        {
            staminaText.text = MyEvent.Movement.ToString();
        }

        if (myEvent.Vignette >= 0)
        {
            vignetteText.text = value.AmountOfVignetteToDraw >= 0 ? "+" + MyEvent.Vignette.ToString() :  MyEvent.Vignette.ToString();
            vignetteText.color = value.AmountOfVignetteToDraw >= 0 ? Color.red : Color.black;
        }
        else
        {
            vignetteText.text = MyEvent.Vignette.ToString();
        }
        
    }

    #region Interface
    public void OnPointerUp(PointerEventData eventData)
    {
        GridManager.instance.CheckTile();
        
        RaycastHit[] hit;
        int amountOfModifier = 0;

        hit = Physics.BoxCastAll(transform.GetChild(0).position, transform.localScale / raycastSize, Vector3.forward, Quaternion.identity, Mathf.Infinity, m_LayerDetection);
        if(hit.Length > 0)
        {
            foreach (var item in hit)
            {
                IModifier myModifier;
                if(item.collider.TryGetComponent<IModifier>(out myModifier))
                {
                    SetUpCard(myModifier);
                    amountOfModifier++;
                }
                vignetteTilePosition.Add(item.collider.gameObject.transform.position);
                vignetteTile.Add(item.collider.gameObject.GetComponent<TileElt_Behaviours>().Tileposition);
            }
            onGrid = true;
        }

        if (!(amountOfModifier > 0))
        {
            SetUpCard();
        }

        foreach (var item in FindObjectsOfType<Bd_Elt_Behaviours>())
        {
            if (item.onGrid)
                item.GetNextMove();
        }
        GameManager.instance.IsMovementvalid();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 data = Camera.main.ScreenToWorldPoint(eventData.position);
        data.z = transform.position.z;
        //AJouter la distance entre le pivot et le curseur;
        offset = transform.position - (Vector3)data;

        SetUpCard();
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.GetChild(0).position, transform.localScale / raycastSize);
    }


    #region Getter & Setter
    public Carte_SO Value { get => value; set => this.value = value; }
    public List<GameObject> ListOfAffectedObject { get => listOfAffectedObject; set => listOfAffectedObject = value; }
    public EventContener MyEvent { get => myEvent; set => myEvent = value; }
    public Bd_Elt_Behaviours NextMove { get => nextMove; set => nextMove = value; }
    #endregion
}
