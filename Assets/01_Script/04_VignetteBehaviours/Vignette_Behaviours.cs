using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Vignette_Behaviours : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public enum VignetteCategories
    {
        NEUTRE,
        EXPLORER,
        PRENDRE,
        COMBATTRE,
        UTILISER,
        PIEGE,
        CURSE,
        PERTE_OBJET,
        VENT_GLACIAL,
        SAVOIR_OCCULTE,
        SMALL_HEAL,
        BIG_HEAL,
        ECLAIRER,
        RESSURECTION,
        PLANIFICATION,
        SOIN_EQUIPE,
        DEBROUILLARD,
        SOUFFLER,
        INSTANTANE,
        RESSEMBLACE_ETRANGE,
        EXPLORER_RARE,
        EXPLORER_MEDIC,
        EXPLORER_OCCULT,
        RASOIR,
        RASOIR_USE,
        ARTEFACT,
        FOOD_OLD,
        CANNOTOPEN
    }

    [SerializeField] private VignetteCategories currentCategorie;
    [SerializeField] protected VignetteCategories initCategorie;
    [SerializeField] private Vignette_Behaviours currentEffect;

    [Space]
    [Header("Text")]
    [SerializeField] protected string m_VignetteName;
    [SerializeField] private TMPro.TMP_Text categorieText;

    [Space]

    [SerializeField] private string vignette_objetText;
    [SerializeField] private string vignette_mentalHealthText;
    [SerializeField] private string vignette_healthText;

    [Space]
    [Header("Raycast")]
    [SerializeField] private LayerMask m_LayerDetection;
    [SerializeField] private LayerMask m_LayerDetectionGrid;
    [SerializeField] private float m_RadiusDetectionSize = 65f;

    [Space]
    [Header("Param")]
    [SerializeField] private Vector2 vignetteShape;
    [SerializeField] private int VignetteSize = 1;
    [SerializeField] private SpriteRenderer SpriteIndicator;
    [SerializeField] private GameObject vignetteScene;
    [SerializeField] private GameObject vignetteImage;

    [Header("List")]
    [SerializeField] private List<Vector2> vignetteTile = new List<Vector2>();
    [SerializeField] private List<GameObject> listOfAffectedObject = new List<GameObject>();
    [SerializeField] private List<CaseContener_SO> listOfCaseEventObject = new List<CaseContener_SO>();

    [Header("Boolean/flag")]
    [SerializeField] private bool onGrid = false;
    [SerializeField] private bool m_IsLock;
    private bool alreadyCheck;

    [Space]
    [Header("Text Status")]
    [SerializeField] private TMPro.TMP_Text objetText;
    [SerializeField] private TMPro.TMP_Text healthText;
    [SerializeField] private TMPro.TMP_Text mentalHealthText;

    /*[Space]
    [Header("event")]
    [SerializeField] private EventContener myEvent;*/

    [Space]
    [Header("Movement")]
    [SerializeField] private Vignette_Behaviours nextMove;
    public List<Vector2> neighbourgCheck = new List<Vector2>();

    [Space]
    [Header("Physics")]
    [SerializeField] private GameObject physicsCheck;

    [Space]
    [Header("Object")]
    [SerializeField] private UsableObject objectFrom;

    [Space]
    [Header("Sprite")]
    [SerializeField] private Sprite ResetRender;
    [SerializeField] private Sprite hoverRender;
    [SerializeField] private Sprite dragRender;

    [Space]
    [Header("FeedBack")]
    [SerializeField] private Animation_Feedback animation_Feedback;

    /*[Header("Fmod")]
    FMOD.Studio.EventInstance lockEffect;
    [FMODUnity.EventRef] [SerializeField] private string lockSound;

    FMOD.Studio.EventInstance transformationEffect;
    [FMODUnity.EventRef] [SerializeField] private string transformationSound;*/

    string vignetteText;
    string curseText;
    private Vector3 offset;

    public List<GameObject> ListOfAffectedObject { get => listOfAffectedObject; set => listOfAffectedObject = value; }
    public bool OnGrid { get => OnGrid1; set => OnGrid1 = value; }
    public Vignette_Behaviours NextMove { get => nextMove; set => nextMove = value; }
    public Vector2 VignetteShape { get => vignetteShape; set => vignetteShape = value; }
    public List<Vector2> VignetteTile { get => vignetteTile; set => vignetteTile = value; }
    public List<CaseContener_SO> ListOfCaseEventObject { get => listOfCaseEventObject; set => listOfCaseEventObject = value; }
    public bool OnGrid1 { get => onGrid; set => onGrid = value; }

    // Start is called before the first frame update
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;

        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);

        vignetteScene = transform.GetChild(1).gameObject;

        vignetteImage = vignetteScene.transform.GetChild(0).gameObject;
        vignetteImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ApplyVignetteEffect()
    {
        currentEffect.ApplyVignetteEffect();
    }

    public void SetUpVignette(VignetteCategories categorie, UsableObject useObject)
    {
        currentCategorie = initCategorie = categorie;
        vignetteText = m_VignetteName;

        SpriteIndicator.sprite = useObject.Data.Sprite;

        if (useObject.IsCurse)
        {
            SpriteIndicator.color = new Color32(104, 46, 68, 255);
            curseText = useObject.MyCurse.curseDisplayName;
        }
        else
        {
            curseText = "";
        }

        categorieText.text = vignetteText + curseText;


        objectFrom = useObject;
        SetUpUI();
    }

    public void SetUpUI()
    {
        objetText.text = vignette_objetText;
        mentalHealthText.text = vignette_mentalHealthText;
        healthText.text = vignette_healthText;
    }

    private void CheckIsPositionIsValid()
    {
        if (GridManager.instance.DoesVignetteIsValid(this) && vignetteTile.Count > 0)
        {
            OnGrid = true;
            //vignetteScene.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            OnGrid = false;
            //vignetteScene.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void ClearList()
    {
        vignetteTile.Clear();
        listOfAffectedObject.Clear();
        ListOfCaseEventObject.Clear();
        GridManager.instance.ListOfHoveredTile.Clear();

        foreach (var item in listOfAffectedObject)
        {
            if (GridManager.instance.ListOfHoveredTile.Contains(item))
                GridManager.instance.ListOfHoveredTile.Remove(item);
        }

        listOfAffectedObject.Clear();
    }

    #region Update Vignette

    public void ApplyTileEffect(VignetteCategories newCategorie)
    {
        bool isChange = false;
        if (currentCategorie != initCategorie)
            isChange = true;

        currentEffect = NewCategorie(newCategorie);
        currentCategorie = newCategorie;
        print(currentEffect);

        categorieText.text = m_VignetteName;
        curseText = "";
        if (objectFrom != null)
        {
            if (objectFrom.IsCurse)
            {
                SpriteIndicator.color = new Color32(104, 46, 68, 255);
                curseText = objectFrom.MyCurse.curseDisplayName;
            }
        }

        animation_Feedback.PlayTransformation();
        //transformationEffect.start();


        categorieText.text = m_VignetteName + curseText;
        SetUpUI();
    }

    public void ResetVignette()
    {
        SetUpUI();

        bool isChange = false;
        if (currentCategorie != initCategorie)
            isChange = true;

        currentCategorie = initCategorie;
        currentCategorie = initCategorie;

        curseText = "";
        if (objectFrom != null)
        {
            if (objectFrom.IsCurse)
            {
                SpriteIndicator.color = new Color32(104, 46, 68, 255);
                curseText = objectFrom.MyCurse.curseDisplayName;
            }
        }

        categorieText.text = m_VignetteName + curseText;

        if (isChange)
        {
            animation_Feedback.PlayTransformation();
            //transformationEffect.start();
        }
    }

    private Vignette_Behaviours NewCategorie(VignetteCategories categorie)
    {
        switch (categorie)
        {
            case VignetteCategories.NEUTRE:
                return new Vignette_Behaviours_Neutre();
                break;
            case VignetteCategories.EXPLORER:
                return new Vignette_Behaviours_Explorer();
                break;
            case VignetteCategories.PRENDRE:
                return new Vignette_Behaviours_Prendre();
                break;
            case VignetteCategories.COMBATTRE:
                return new Vignette_Behaviours_Combattre();
                break;
            case VignetteCategories.UTILISER:
                return new Vignette_Behaviours_Utiliser();
                break;
            case VignetteCategories.PIEGE:
                return new Vignette_Behaviours_Piege();
                break;
            case VignetteCategories.CURSE:
                return new Vignette_Behaviours_Curse();
                break;
            case VignetteCategories.PERTE_OBJET:
                return new Vignette_Behaviours_Perte_Objet();
                break;
            case VignetteCategories.VENT_GLACIAL:
                return new Vignette_Behaviours_Vent_Glacial();
                break;
            case VignetteCategories.SAVOIR_OCCULTE:
                return new Vignette_Behaviours_Savoir_Occulte();
                break;
            case VignetteCategories.SMALL_HEAL:
                return new Vignette_Behaviours_Small_Heal();
                break;
            case VignetteCategories.BIG_HEAL:
                return new Vignette_Behaviours_Big_Heal();
                break;
            case VignetteCategories.ECLAIRER:
                return new Vignette_Behaviours_Eclairer();
                break;
            case VignetteCategories.RESSURECTION:
                return new Vignette_Behaviours_Ressurection();
                break;
            case VignetteCategories.PLANIFICATION:
                return new Vignette_Behaviours_Planification();
                break;
            case VignetteCategories.SOIN_EQUIPE:
                return new Vignette_Behaviours_Soin_Equipe();
                break;
            case VignetteCategories.DEBROUILLARD:
                return new Vignette_Behaviours_Debrouillard();
                break;
            case VignetteCategories.SOUFFLER:
                return new Vignette_Behaviours_Souffler();
                break;
            case VignetteCategories.INSTANTANE:
                return new Vignette_Behaviours_Instantane();
                break;
            case VignetteCategories.RESSEMBLACE_ETRANGE:
                return new Vignette_Behaviours_Ressemblance_Etrange();
                break;
            case VignetteCategories.EXPLORER_RARE:
                return new Vignette_Behaviours_EXPLORER_RARE();
                break;
            case VignetteCategories.EXPLORER_MEDIC:
                return new Vignette_Behaviours_EXPLORER_Medic();
                break;
            case VignetteCategories.EXPLORER_OCCULT:
                return new Vignette_Behaviours_EXPLORER_occult();
                break;
            case VignetteCategories.RASOIR:
                break;
            case VignetteCategories.RASOIR_USE:
                break;
            case VignetteCategories.ARTEFACT:
                break;
            case VignetteCategories.FOOD_OLD:
                break;
            case VignetteCategories.CANNOTOPEN:
                break;
            default:
                return null;
                break;
        }
        return null;
    }

    #endregion

    public void SetUpVignette(VignetteCategories categorie)
    {
        currentCategorie = initCategorie = categorie;
        categorieText.text = vignetteText;
        SpriteIndicator.sprite = null;
        currentEffect = NewCategorie(categorie);
        //objectFrom = null;
        SetUpUI();
    }

    #region Check
    public bool CheckCaseCondition()
    {
        print("CheckCaseCondition");

        if (ListOfCaseEventObject.Count > 0)
        {
            print("check");
            foreach (var condition in ListOfCaseEventObject)
            {
                if (condition.DoLock)
                {
                    this.m_IsLock = true;
                    animation_Feedback.PlayLock();
                    //lockEffect.start();
                }


                if (condition.AnyVignette || currentCategorie == VignetteCategories.DEBROUILLARD)
                {
                    ApplyTileEffect(condition.CaseResult);
                    return true;
                }

                foreach (var objectNeeded in condition.ObjectsRequired)
                {
                    if (currentCategorie == objectNeeded)
                    {
                        ApplyTileEffect(condition.CaseResult);
                        return true;
                    }
                }
            }
            if (ListOfCaseEventObject[0].IsEchecResult)
                ApplyTileEffect(ListOfCaseEventObject[0].EchecResult);
        }

        return false;
    }

    private void CheckNeighbourg()
    {
        RaycastHit hit;
        TileElt_Behaviours checkedTile;
        int amountOfModifier = 0;


        for (int i = 0; i < physicsCheck.transform.childCount; i++)
        {
            GameObject tile = physicsCheck.transform.GetChild(i).gameObject;
            Physics.Raycast(tile.gameObject.transform.position, Vector3.forward, out hit, Mathf.Infinity, m_LayerDetection);
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.TryGetComponent<TileElt_Behaviours>(out checkedTile))
                {
                    if (checkedTile != null)
                    {
                        if ((!neighbourgCheck.Contains(checkedTile.Tileposition)))
                        {
                            neighbourgCheck.Add(checkedTile.Tileposition);
                        }
                    }
                }
            }
        }
    }

    public Vignette_Behaviours CheckNextMove()
    {
        if (OnGrid)
        {
            //GridManager.instance.ListOfMovement.Clear();
            alreadyCheck = false;
            nextMove = null;
            bool isDecal = false;
            TileElt_Behaviours tileEvent;

            foreach (var hoveredTile in neighbourgCheck)
            {
                for (int x = 0; x <= 1; x++)
                {
                    for (int y = 0; y <= 1; y++)
                    {

                        Vector2 tilePos = new Vector2((hoveredTile.x + x), (hoveredTile.y + y));

                        if (tilePos.y > GridManager.instance.GridSize.y)
                        {
                            tilePos.Set(tilePos.x + 1, 0);
                            isDecal = true;
                        }
                        
                        if (!vignetteTile.Contains(tilePos))
                        {
                            if (VectorMethods.ManhattanDistance(hoveredTile, tilePos, 1) && tilePos.x < GridManager.instance.GridSize.x && tilePos.y < GridManager.instance.GridSize.y)
                            {
                                if (tilePos.x < GridManager.instance.GridSize.x && tilePos.y < GridManager.instance.GridSize.y)
                                {
                                    GameObject tile = GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)][Mathf.RoundToInt(tilePos.y)];
                                    tileEvent = tile.GetComponent<TileElt_Behaviours>();

                                    if (tileEvent != null)
                                    {
                                        if (tileEvent != null && tileEvent.EventAssocier != null && tileEvent.EventAssocier != this)
                                        {
                                            if (!tileEvent.EventAssocier.alreadyCheck)
                                            {
                                                alreadyCheck = true;
                                                return tileEvent.EventAssocier;
                                            }
                                            if (hoveredTile == Vector2.zero)
                                            {
                                                alreadyCheck = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (isDecal)
                            {
                                if (tilePos.y >= GridManager.instance.GridSize.y)
                                {
                                    tilePos.Set(tilePos.x + 1, 0);
                                    isDecal = true;
                                }

                                if (tilePos.x < GridManager.instance.GridSize.x && tilePos.y < GridManager.instance.GridSize.y)
                                {
                                    GameObject tile = GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)][Mathf.RoundToInt(tilePos.y)];

                                    if (tile.TryGetComponent<TileElt_Behaviours>(out tileEvent))
                                    {
                                        if (tileEvent != null && tileEvent.EventAssocier != null && tileEvent.EventAssocier != this)
                                        {
                                            if (!tileEvent.EventAssocier.alreadyCheck)
                                            {
                                                alreadyCheck = true;

                                                return tileEvent.EventAssocier;
                                            }
                                            if (hoveredTile == Vector2.zero)
                                            {
                                                alreadyCheck = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            alreadyCheck = false;
            nextMove = null;
            bool isDecal = false;
        }
        return null;
    }

    public void GetNextMove()
    {
        Vignette_Behaviours check = CheckNextMove();
        NextMove = check != this ? check : null;
    }

    #endregion

    public void TakeEffect()
    {
        print("Take Effect off : " + InventoryManager.instance.PageInventory.Count + " Item");
        for (int i = 0; i < CanvasManager.instance.LevelInventoryPanel1.transform.childCount; i++)
        {
            Destroy(CanvasManager.instance.LevelInventoryPanel1.transform.GetChild(i).gameObject);
        }

        InventoryManager.instance.PageInventory.Clear();
        CanvasManager.instance.SetUpLevelIndicator();
        //GameObject.Find("Feedback_Stockage").GetComponent<Inventaire_Feedback>().PlayStockageFeedback();
    }

    #region Interfaces
    public void OnPointerUp(PointerEventData eventData)
    {
        ClearList();
        GridManager.instance.CheckTile();
        //GridManager.instance.SortList();

        RaycastHit[] hit;
        int amountOfModifier = 0;

        hit = Physics.BoxCastAll(transform.GetChild(0).position, transform.localScale / m_RadiusDetectionSize, Vector3.forward, Quaternion.identity, Mathf.Infinity, m_LayerDetection);
        if (hit.Length > 0 && hit.Length <= VignetteSize)
        {
            foreach (var item in hit)
            {
                Case_Behaviours caseBehaviours;

                if (item.collider.TryGetComponent<Case_Behaviours>(out caseBehaviours))
                {
                    if (caseBehaviours.CaseEffects != null)
                        ListOfCaseEventObject.Add(caseBehaviours.CaseEffects);
                }

                if (!GridManager.instance.ListOfHoveredTile.Contains(item.collider.gameObject))
                {
                    //vignetteTilePosition.Add(item.collider.gameObject.transform.position);
                    VignetteTile.Add(item.collider.gameObject.GetComponent<TileElt_Behaviours>().Tileposition);
                    VignetteTile.Sort((v1, v2) => (v1.x - v1.y).CompareTo((v2.x - v2.y)));
                    listOfAffectedObject.Add(item.collider.gameObject);
                    GridManager.instance.ListOfHoveredTile.Add(item.collider.gameObject);
                }

            }
            if (vignetteTile.Count < (VignetteShape.x * VignetteShape.y))
            {
                ClearList();
            }
            //dropVignetteOnGridEffect.start();
        }
        else
        {
            OnGrid = false;
            nextMove = null;
        }

        foreach (var item in FindObjectsOfType<Vignette_Behaviours>())
        {
            item.CheckIsPositionIsValid();
            item.alreadyCheck = false;
            item.nextMove = null;
            item.neighbourgCheck.Clear();
            item.CheckNeighbourg();

        }

        GridManager.instance.SortList();
        if(GridManager.instance.ListOfMovement.Count >= 1)
        {
            print(GridManager.instance.ListOfMovement.Count);
            foreach (var item in GridManager.instance.ListOfMovement)
            {
                Vignette_Behaviours check = item.EventAssocier;
                //print(item.gameObject.name + " je regarde ici et check = " + check);
                if (check != null)
                {
                    if (check.OnGrid && check.vignetteTile.Count > 0)
                    {
                        check.GetNextMove();
                    }
                }
            }
        }

        GridManager.instance.GetVignetteOrderByNeighbourg();

        GridManager.instance.CheckIfAllAreConnect();//IsMovementvalid();

        CheckCaseCondition();

        GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 0;
        GridManager.instance.SortList();
        LineRendererScript.instance.DrawLineRenderer();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ResetVignette();
        Vector3 data = Camera.main.ScreenToWorldPoint(eventData.position);
        data.z = transform.position.z;
        //AJouter la distance entre le pivot et le curseur;
        offset = transform.position - (Vector3)data;

        GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 100;

        //takeVignetteEffect.start();
        // SetUpUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hoverRender;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ResetRender;
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
        if (!m_IsLock)
            transform.position = rayPoint;

        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = dragRender;
    }
    #endregion

}
