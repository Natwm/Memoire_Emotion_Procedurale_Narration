using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Vignette_Behaviours : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    #region param

    private Vector3 offset;

    [Space]
    [Header("Raycast")]
    [SerializeField] private float m_RadiusDetection;
    [SerializeField] private float raycastSize = 65f;
    [SerializeField] private LayerMask m_LayerDetection;
    [SerializeField] private LayerMask m_LayerDetectionGrid;


    [Space]
    [Header("Param")]
    [SerializeField] private Vector2 vignetteShape;
    [SerializeField] private int VignetteSize = 1;
    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private GameObject vignetteScene;
    [SerializeField] private GameObject vignetteImage;
    [SerializeField] private GameObject vignetteInfo;

    [Header("List")]
    [SerializeField] private List<Vector2> vignetteTilePosition = new List<Vector2>();
    [SerializeField] private List<Vector2> vignetteTile = new List<Vector2>();
    [SerializeField] private List<GameObject> listOfAffectedObject = new List<GameObject>();

    [Header("Boolean/flag")]
    [SerializeField] private bool onGrid = false;
    [SerializeField] private bool m_IsLook;
    [SerializeField] private bool m_IsVignetteShowUp;

    [Space]
    [Header("Text Status")]
    [SerializeField] private TMPro.TMP_Text staminaText;
    [SerializeField] private TMPro.TMP_Text healthText;
    [SerializeField] private TMPro.TMP_Text vignetteText;

    [Space]
    [Header("event")]
    [SerializeField] private EventContener myEvent;

    [Space]
    [Header("Movement")]
    [SerializeField] private Vignette_Behaviours nextMove;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.Drag;

        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);

        m_IsVignetteShowUp = false;

        vignetteInfo = transform.GetChild(0).gameObject;
        //vignetteInfo.SetActive(true);

        vignetteScene = transform.GetChild(1).gameObject;
        //vignetteScene.SetActive(false);

        vignetteImage = vignetteScene.transform.GetChild(0).gameObject;
        vignetteImage.GetComponent<SpriteRenderer>().DOFade(0, 0f);

        myEvent = GetComponent<EventContener>();
        SetUpCard();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Vignette_Behaviours CheckNextMove()
    {
        if (OnGrid)
        {
            bool isNewLine = false;
            foreach (var overedTile in VignetteTile)
            {
                //print("aaaa" + " Shape = (" + (vignetteShape.x+1)+","+ (vignetteShape.y+1));
                for (int x = 0; x < VignetteShape.x + 1; x++)
                {
                    for (int y = 0; y < VignetteShape.y + 1; y++)
                    {

                        Vector2 tilePos = new Vector2((overedTile.x + x), (overedTile.y + y));
                        if (tilePos.x >= 4 && tilePos.y < 4)
                        {
                            tilePos.Set(0, tilePos.y + 1);
                            isNewLine = true;
                        }

                        if (tilePos.y >= 4 && tilePos.x < 4)
                        {
                            tilePos.Set(tilePos.x + 1, 0);
                            isNewLine = true;
                        }
                        //print("Check by : " + this.name +" at tilePos : " + tilePos);

                        //print(" pomme = " + "tilePos  " + tilePos);
                        if ((VectorMethods.ManhattanDistance(overedTile, tilePos, 1) && !VignetteTile.Contains(tilePos)) || isNewLine)
                        {
                            //print(" pomme2 = " + "tilePos  " + tilePos);
                            try
                            {
                                GameObject tile = GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)][Mathf.RoundToInt(tilePos.y)];
                                TileElt_Behaviours tileEvent;

                                if (tile.TryGetComponent<TileElt_Behaviours>(out tileEvent))
                                {
                                    if(tileEvent.EventAssocier != this && tileEvent.EventAssocier != null)
                                    {
                                        return tileEvent.EventAssocier;
                                    }
                                }
                            }
                            catch { }
                        }
                        else if (isNewLine)
                        {
                            print("rsersfese " + tilePos);
                            GameObject tile = GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)][Mathf.RoundToInt(tilePos.y)];
                            TileElt_Behaviours tileEvent;

                            if (tile != null)
                            {
                                if (tile.TryGetComponent<TileElt_Behaviours>(out tileEvent))
                                {
                                    if (tileEvent.EventAssocier != this && tileEvent.EventAssocier != null)
                                    {
                                        return tileEvent.EventAssocier;
                                    }
                                }
                            }
                        }
                        //print(GridManager.instance.ListOfTile2D[Mathf.RoundToInt(overedTile.x + x)][Mathf.RoundToInt(overedTile.y + y)]);
                    }
                }
            }
        }
        else
        {
            nextMove = null;
        }

        return null;
    }

    public void GetNextMove()
    {
        NextMove = CheckNextMove() != this ? CheckNextMove() : null;
        if (NextMove != null)
            print("Next move is :    " + NextMove.gameObject);
    }

    public void SetUpCard(int happySad_Value = 0, int angryFear_Value = 0, int amountofVignetteToDraw = 0, bool isKey = false, Sprite vignetteRender = null)
    {
        myEvent.SetUp(happySad_Value, angryFear_Value, amountofVignetteToDraw, isKey);
        cardImage.sprite = vignetteRender;
        SetUpUI();
    }

    public void SetUpCard(IModifier modifier)
    {
        modifier.CollectElement(myEvent);
        SetUpUI();
    }

    //quand tu drop, ne mets pas a jour le script event
    private void SetUpUI()
    {
        if (myEvent.CurrentHappy_Sad >= 0)
        {
            healthText.text = "+" + MyEvent.CurrentHappy_Sad.ToString();
            healthText.color = MyEvent.CurrentHappy_Sad > 0 ? Color.red : Color.black;
        }

        else
        {
            healthText.text = MyEvent.CurrentHappy_Sad.ToString();
        }

        if (myEvent.CurrentAngry_Fear >= 0)
        {
            staminaText.text = MyEvent.CurrentAngry_Fear >= 0 ? "+" + MyEvent.CurrentAngry_Fear.ToString() : MyEvent.CurrentAngry_Fear.ToString();
            staminaText.color = MyEvent.CurrentAngry_Fear >= 0 ? Color.red : Color.black;
        }

        else
        {
            staminaText.text = MyEvent.CurrentAngry_Fear.ToString();
        }

        if (myEvent.CurrentAmountOfVignetteToDraw >= 0)
        {
            vignetteText.text = MyEvent.CurrentAmountOfVignetteToDraw >= 0 ? "+" + MyEvent.CurrentAmountOfVignetteToDraw.ToString() : MyEvent.CurrentAmountOfVignetteToDraw.ToString();
            vignetteText.color = MyEvent.CurrentAmountOfVignetteToDraw >= 0 ? Color.red : Color.black;
        }
        else
        {
            vignetteText.text = MyEvent.CurrentAmountOfVignetteToDraw.ToString();
        }

    }

    private void ShowVignetteElt(GameObject inObject, GameObject outObject, float speed = 0.25f)
    {
        outObject.GetComponent<SpriteRenderer>().DOFade(0, speed);
        inObject.GetComponent<SpriteRenderer>().DOFade(1, speed);
    }

    private void CheckIsPositionIsValid()
    {
        if (GridManager.instance.DoesVignetteIsValid(this))
        {
            OnGrid = true;
            vignetteScene.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            OnGrid = false;
            vignetteScene.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    #region Interface
    public void OnPointerUp(PointerEventData eventData)
    {
        vignetteTilePosition.Clear();
        vignetteTile.Clear();

        GridManager.instance.CheckTile();
        GridManager.instance.SortList();

        RaycastHit[] hit;
        int amountOfModifier = 0;

        hit = Physics.BoxCastAll(transform.GetChild(0).position, transform.localScale / raycastSize, Vector3.forward, Quaternion.identity, Mathf.Infinity, m_LayerDetection);
        if (hit.Length > 0)
        {
            foreach (var item in hit)
            {
                IModifier myModifier;
                if (item.collider.TryGetComponent<IModifier>(out myModifier))
                {
                    SetUpCard(myModifier);
                    amountOfModifier++;
                }
                vignetteTilePosition.Add(item.collider.gameObject.transform.position);
                VignetteTile.Add(item.collider.gameObject.GetComponent<TileElt_Behaviours>().Tileposition);
            }

        }
        else
        {
            OnGrid = false;
            nextMove = null;
            vignetteScene.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (!(amountOfModifier > 0))
        {
            myEvent.ResetEvent();
        }

        if (OnGrid)
        {
            foreach (var item in FindObjectsOfType<Vignette_Behaviours>())
            {
                if (item.OnGrid)
                {
                    item.GetNextMove();
                }

            }
        }

        foreach (var item in FindObjectsOfType<Vignette_Behaviours>())
        {
                item.CheckIsPositionIsValid();
        }

        GameManager.instance.IsMovementvalid();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 data = Camera.main.ScreenToWorldPoint(eventData.position);
        data.z = transform.position.z;
        //AJouter la distance entre le pivot et le curseur;
        offset = transform.position - (Vector3)data;

        myEvent.ResetEvent();
        SetUpUI();
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
        if (!m_IsLook)
            transform.position = rayPoint;


        RaycastHit hit;
        Physics.Raycast(this.gameObject.transform.position, Vector3.forward, out hit, Mathf.Infinity, m_LayerDetectionGrid);
        if (hit.collider != null)
        {
            m_IsVignetteShowUp = true;
            /*vignetteScene.SetActive(true);
            vignetteInfo.SetActive(false);*/
            ShowVignetteElt(vignetteImage, vignetteInfo, .2f);
        }
        else
        {
            m_IsVignetteShowUp = false;
            /*vignetteScene.SetActive(false);
            vignetteInfo.SetActive(true);*/

            ShowVignetteElt(vignetteInfo, vignetteImage, .2f);
        }
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.GetChild(0).position, transform.localScale / raycastSize);
    }


    #region Getter & Setter
    public List<GameObject> ListOfAffectedObject { get => listOfAffectedObject; set => listOfAffectedObject = value; }
    public EventContener MyEvent { get => myEvent; set => myEvent = value; }
    public Vignette_Behaviours NextMove { get => nextMove; set => nextMove = value; }
    public List<Vector2> VignetteTile { get => vignetteTile; set => vignetteTile = value; }
    public Vector2 VignetteShape { get => vignetteShape; set => vignetteShape = value; }
    public bool OnGrid { get => onGrid; set => onGrid = value; }
    #endregion
}
