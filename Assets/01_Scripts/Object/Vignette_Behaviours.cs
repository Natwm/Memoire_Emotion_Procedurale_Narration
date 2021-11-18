using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Vignette_Behaviours : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
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
        SAVOIR_OCCULTE        
    }

    #region param

    private Vector3 offset;

    [SerializeField] private VignetteCategories categorie;
    [SerializeField] private TMPro.TMP_Text categorieText;

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
    [SerializeField] public GameObject vignetteInfo;
    public Vignette assignedVignette;
    [SerializeField] private SpriteRenderer SpriteIndicator;

    [Header("List")]
    [SerializeField] private List<Vector2> vignetteTilePosition = new List<Vector2>();
    [SerializeField] private List<Vector2> vignetteTile = new List<Vector2>();
    [SerializeField] private List<GameObject> listOfAffectedObject = new List<GameObject>();
    [SerializeField] private List<CaseContener_SO> listOfCaseEventObject = new List<CaseContener_SO>();

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
    [SerializeField] private Vignette_Behaviours previousMove;

    [SerializeField] private List<Vignette_Behaviours> testt = new List<Vignette_Behaviours>();

    public List<Vector2> neighbourgCheck = new List<Vector2>();

    [Space]
    [Header("Physics")]
    [SerializeField] private GameObject physicsCheck;
    #endregion

    public bool a = false;
    // Start is called before the first frame update
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;

        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);

        print(trigger);

        m_IsVignetteShowUp = false;

        // vignetteInfo = transform.GetChild(0).gameObject;
        //vignetteInfo.SetActive(true);

        vignetteScene = transform.GetChild(1).gameObject;
        //vignetteScene.SetActive(false);

        vignetteImage = vignetteScene.transform.GetChild(0).gameObject;
        vignetteImage.SetActive(false);
        //vignetteImage.GetComponent<SpriteRenderer>().DOFade(0, 0f);

        myEvent = GetComponent<EventContener>();
        SetUpCard();

        Categorie = GetRandomEnum();
        categorieText.text = GetEnumName();
        SpriteIndicator.sprite = GetSprite();
        //SpriteIndicator.color = Color.black;

        //vignetteInfo.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = ;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ApplyVignetteEffect()
    {
        // fait effet 

        // action spécifique
        switch (Categorie)
        {
            case VignetteCategories.NEUTRE:
                NeutralEffect();
                break;
            case VignetteCategories.EXPLORER:
                ExploreEffect();
                break;
            case VignetteCategories.PRENDRE:
                TakeEffect();
                break;
            case VignetteCategories.COMBATTRE:
                FightEffect();
                break;
            case VignetteCategories.UTILISER:
                UseEffect();
                break;
            case VignetteCategories.PIEGE:
                FallEffect();
                break;
            case VignetteCategories.CURSE:
                CurseEffect();
                break;
            case VignetteCategories.PERTE_OBJET:
                LooseObjectEffect();
                break;
            case VignetteCategories.VENT_GLACIAL:
                Vent_GlacialEffect();
                break;
            case VignetteCategories.SAVOIR_OCCULTE:
                Savoir_OcculteEffect();
                break;
            default:
                break;
        }
    }

    

    #region VIgnette Effect

    public void NeutralEffect()
    {
        print("neutre");
    }

    public void ExploreEffect()
    {
        print("ExploreEffect");

        int randomIndex = UnityEngine.Random.Range(0, LevelManager.instance.UnlockableObject.Count);

        LevelManager.instance.PageInventory.Add(LevelManager.instance.UnlockableObject[randomIndex]);
    }

    public void TakeEffect()
    {
        print("Take Effect off : " + LevelManager.instance.PageInventory.Count + " Item");
        foreach (var item in LevelManager.instance.PageInventory)
        {
            CreationManager.instance.GlobalInventory.Add(item);
        }

        LevelManager.instance.PageInventory = new List<Object_SO>();
    }

    public void FightEffect()
    {
        print("FightEffect");
        PlayerManager.instance.GetDamage(1);
    }

    public void UseEffect()
    {
        print("UseEffect"); // check si il y a condition
        CheckCaseCondition();
    }

    public void FallEffect()
    {
        print("FallEffect");
        PlayerManager.instance.GetDamage(1);
    }
    public void CurseEffect()
    {
        print("CurseEffect");

        PlayerManager.instance.GetDamage(2);
    }

    public void LooseObjectEffect()
    {
        print("LooseObjectEffect");
        if (PlayerManager.instance.Inventory.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, PlayerManager.instance.Inventory.Count);
            PlayerManager.instance.Inventory.RemoveAt(index);
        }
    }

    public void Vent_GlacialEffect()
    {
        print("Vent_GlacialEffect");
        LevelManager.instance.PageInventory = new List<Object_SO>();
    }

    public void Savoir_OcculteEffect()
    {
        print("Savoir_OcculteEffect");
        for (int i = 0; i < 2; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, LevelManager.instance.UnlockableObject.Count);

            LevelManager.instance.PageInventory.Add(LevelManager.instance.UnlockableObject[randomIndex]);
        }
    }

    #endregion

    public bool CheckCaseCondition()
    {
        print("CheckCaseCondition");
        
        if (ListOfCaseEventObject.Count > 0)
        {
            print("check");
            foreach (var condition in ListOfCaseEventObject)
            {
                foreach (var objectNeeded in condition.ObjectsRequired)
                {
                    if (PlayerManager.instance.Inventory.Contains(objectNeeded))
                    {
                        PlayerManager.instance.Inventory.Remove(objectNeeded);

                        foreach (var item in condition.CaseResult)
                        {
                            LevelManager.instance.PageInventory.Add(item);
                        }
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void CheckNeighbourg()
    {
        RaycastHit hit;
        IModifier myModifier;
        TileElt_Behaviours checkedTile;
        int amountOfModifier = 0;


        for (int i = 0; i < physicsCheck.transform.childCount; i++)
        {
            GameObject tile = physicsCheck.transform.GetChild(i).gameObject;
            Physics.Raycast(tile.gameObject.transform.position, Vector3.forward, out hit, Mathf.Infinity, m_LayerDetection);
            if(hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                if(hitObject.TryGetComponent<TileElt_Behaviours>(out checkedTile))
                {
                    if (checkedTile != null)
                    {
                        
                        if ((!neighbourgCheck.Contains(checkedTile.Tileposition)))
                        {
                            
                            neighbourgCheck.Add(checkedTile.Tileposition);
                            /*vignetteTilePosition.Add(hitObject.transform.position);
                            VignetteTile.Add(checkedTile.Tileposition);
                            listOfAffectedObject.Add(hitObject);
                            GridManager.instance.ListOfOveredTile.Add(hitObject);*/
                        }
                    }
                }
            }
            //Gizmos.DrawWireCube(tile.transform.position, transform.localScale / raycastSize);
        }
    }

    public Vignette_Behaviours CheckNextMove()
    {
        if (OnGrid)
        {
            GridManager.instance.Test.Clear();
            a = false;
            previousMove = nextMove = null;
            bool isDecal = false;
            TileElt_Behaviours tileEvent;

            /*print("La tile qui vérifie est : " + this.gameObject.name);
            print("les voisin de " + this.gameObject.name + "sont = " + neighbourgCheck.Count);
            print(this.gameObject.name + " est de taille = " + vignetteShape);*/
            foreach (var hoveredTile in neighbourgCheck)
            {
                //print("La tile qui vérifie est : " + this.gameObject.name + "____________________________________________________________");
                //print("La tile qui vérifie est : " + this.gameObject.name +"Tile check is = "+hoveredTile);
                for (int x = 0; x <= 1 ; x++)
                {
                    for (int y = 0; y <= 1 ; y++)
                    {
                       // print("La tile qui vérifie est : " + this.gameObject.name + "_______________"+" " + x+" "+y);

                        //print(this.gameObject.name + "   Position du curseur = "+x + " " + y);
                        Vector2 tilePos = new Vector2((hoveredTile.x + x), (hoveredTile.y + y));
                        //print("tilePos  = " + tilePos + "  !vignetteTile.Contains(tilePos) = " + !vignetteTile.Contains(tilePos));

                        if (tilePos.y >= GridManager.instance.GridSize.y)
                        {
                            //print("La tile : " + this.gameObject.name+"  est décalé");
                            tilePos.Set(tilePos.x + 1, 0);
                            isDecal = true;
                        }
                        //print(this.gameObject.name + "   Check = " + tilePos);
                        //print(this.gameObject.name + "   est ce que vignettetile le contiens = " + vignetteTile.Contains(tilePos));

                        if (!vignetteTile.Contains(tilePos))
                        {
                            if (VectorMethods.ManhattanDistance(hoveredTile, tilePos, 1) && tilePos.x < GridManager.instance.GridSize.x && tilePos.y < GridManager.instance.GridSize.y)
                            {
                                /*print("Game hoveredTile = " + hoveredTile + "  tilePos  = " + tilePos + "  !vignetteTile.Contains(tilePos) = " + !vignetteTile.Contains(tilePos));
                                print("La tile qui vérifie est : " + this.gameObject.name +" et possède la bonne distance de  " + tilePos);
                                print("La tile qui vérifie est : " + this.gameObject.name + " check si elle est au bonne endroit  " + (tilePos.x < GridManager.instance.GridSize.y && tilePos.y < GridManager.instance.GridSize.y));
                               */
                                if (tilePos.x < GridManager.instance.GridSize.y && tilePos.y < GridManager.instance.GridSize.y)
                                {
                                    GameObject tile = GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)][Mathf.RoundToInt(tilePos.y)];
                                    //print("La tile qui vérifie est : " + this.gameObject.name + " La taile que l'on check is " + tile.name);
                                    tileEvent = tile.GetComponent<TileElt_Behaviours>();
                                   // print("la tile : " + this.gameObject.name + " vérifie la position : " + Mathf.RoundToInt(tilePos.x) + " " + Mathf.RoundToInt(tilePos.y) + " et levent est : " + tileEvent);
                                   // print("La tile qui vérifie est : " + this.gameObject.name + " La taile que l'on check is " + tile.name + " , est ce quel possède l'event :" + tileEvent +" est il null ? " + tileEvent != null);


                                    if (tileEvent != null)
                                    {
                                        //print("La tile  est : " + tile.gameObject.name + " et possède TileElt_Behaviours  " + tileEvent);

                                        if (tileEvent != null && tileEvent.EventAssocier != null && tileEvent.EventAssocier != this)
                                        {
                                            //print(tileEvent.EventAssocier.name + "   la valeur de schec est : " + tileEvent.EventAssocier.a);
                                            if (!tileEvent.EventAssocier.a)
                                            {
                                                a = true;
                                                tileEvent.EventAssocier.previousMove = this;
                                                //print("Je suis  "+this.gameObject.name +" et J'ai detec un voisin qui est  : " + tileEvent.EventAssocier.gameObject.name);
                                                return tileEvent.EventAssocier;
                                            }
                                            if (hoveredTile == Vector2.zero)
                                            {
                                                a = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (isDecal)
                            {
                                /*print("isDecal " + isDecal);
                                print("tilePos  = " + tilePos + "  !vignetteTile.Contains(tilePos) = " + !vignetteTile.Contains(tilePos));
                                */
                                if (tilePos.y >= GridManager.instance.GridSize.y)
                                {
                                    tilePos.Set(tilePos.x + 1, 0);
                                    isDecal = true;
                                }

                                if (tilePos.x < GridManager.instance.GridSize.y && tilePos.y < GridManager.instance.GridSize.y)
                                {
                                    GameObject tile = GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)][Mathf.RoundToInt(tilePos.y)];

                                    if (tile.TryGetComponent<TileElt_Behaviours>(out tileEvent))
                                    {
                                        if (tileEvent != null && tileEvent.EventAssocier != null && tileEvent.EventAssocier != this)
                                        {
                                            if (!tileEvent.EventAssocier.a)
                                            {
                                                a = true;
                                                tileEvent.EventAssocier.previousMove = this;

                                                return tileEvent.EventAssocier;
                                            }
                                            if (hoveredTile == Vector2.zero)
                                            {
                                                a = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //print("_______ __________ __________ ______ ______ ___");

                }
                //print("__________________________________________");
            }
        }
        else
        {
            a = false;
            previousMove = nextMove = null;
            bool isDecal = false;
        }

        return null;

    }

    public Vignette_Behaviours CheckNextMove(int a = 0)
    {
        if (OnGrid)
        {
            previousMove = nextMove = null;

            bool isNewLine = false;
            foreach (var hoveredTile in VignetteTile)
            {
                //print("aaaa" + " Shape = (" + (vignetteShape.x+1)+","+ (vignetteShape.y+1));
                for (int x = -1; x < VignetteShape.x + 1; x++)
                {
                    for (int y = 0; y < VignetteShape.y + 1; y++)
                    {

                        Vector2 tilePos = new Vector2((hoveredTile.x + x), (hoveredTile.y + y));

                        if (!vignetteTile.Contains(tilePos))
                        {

                            /*if (tilePos.x >= 4 && tilePos.y < 4)
                            {
                                tilePos.Set(0, tilePos.y + 1);
                                isNewLine = true;
                            }*/
                            //print(tilePos);

                            if (tilePos.y >= GridManager.instance.GridSize.y && tilePos.x < GridManager.instance.GridSize.y)
                            {
                                tilePos.Set(tilePos.x + 1, 0);
                                isNewLine = true;
                            }
                            //print("Check by : " + this.name +" at tilePos : " + tilePos);

                            //print(" pomme = " + "tilePos  " + tilePos);
                            if (VectorMethods.ManhattanDistance(hoveredTile, tilePos, 1) || isNewLine)
                            {
                                print(x + " " + y);
                                Debug.Break();
                                //print(" pomme2 = " + "tilePos  " + tilePos);
                                try
                                {
                                    GameObject tile = GridManager.instance.ListOfTile2D[Mathf.RoundToInt(tilePos.x)][Mathf.RoundToInt(tilePos.y)];
                                    TileElt_Behaviours tileEvent;

                                    if (tile.TryGetComponent<TileElt_Behaviours>(out tileEvent))
                                    {
                                        if (tileEvent.EventAssocier != this && tileEvent.EventAssocier != null)
                                        {
                                            print(this.name + "  next = " + tileEvent.EventAssocier);
                                            /*if (!GridManager.instance.Test.Contains(tileEvent.EventAssocier))
                                            {*/
                                            print("!GridManager.instance.Test.Contains(tileEvent.EventAssocier) = " + !GridManager.instance.Test.Contains(tileEvent.EventAssocier));
                                            if (tileEvent.EventAssocier.nextMove == null)
                                            {
                                                tileEvent.EventAssocier.previousMove = this;
                                                return tileEvent.EventAssocier;
                                            }

                                            //}
                                        }
                                    }
                                }
                                catch { }
                            }
                            /*else if (isNewLine)
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
                                            if (!GridManager.instance.Test.Contains(tileEvent.EventAssocier))
                                            {
                                                tileEvent.EventAssocier.previousMove = this;
                                                return tileEvent.EventAssocier;
                                            }
                                        }
                                    }
                                }
                            }*/
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
        Vignette_Behaviours check = CheckNextMove();
        NextMove = check != this ? check : null;
        if (NextMove != null)
        {
            print("Next move is :    " + NextMove.gameObject);
        }
        else
        {
            print("NextMove est sensé être  =" + check );
            print("il est null car : check != this =" + (check != this));
        }

    }

    public void SetUpCard(int happySad_Value = 0, int angryFear_Value = 0, int amountofVignetteToDraw = 0, bool isKey = false, Sprite vignetteRender = null)
    {
        myEvent.SetUp(happySad_Value, angryFear_Value, amountofVignetteToDraw, isKey);

        cardImage.sprite = vignetteRender;
        //SetUpUI();
    }

    public void SetUpCard(IModifier modifier)
    {
        modifier.CollectElement(myEvent);
        //SetUpUI();
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
        //outObject.GetComponent<SpriteRenderer>().DOFade(0, speed);
        outObject.SetActive(false);
        //inObject.GetComponent<SpriteRenderer>().DOFade(1, speed);
        inObject.SetActive(true);
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
        vignetteTilePosition.Clear();
        vignetteTile.Clear();
        listOfCaseEventObject.Clear();

        foreach (var item in listOfAffectedObject)
        {
            if (GridManager.instance.ListOfOveredTile.Contains(item))
                GridManager.instance.ListOfOveredTile.Remove(item);
        }

        listOfAffectedObject.Clear();
    }

    private VignetteCategories GetRandomEnum()
    {
        int value = UnityEngine.Random.Range(0,10);

        switch (value)
        {
            case 0:
                return VignetteCategories.NEUTRE;
                break;
            case 1:
                return VignetteCategories.EXPLORER;
                break;
            case 2:
                return VignetteCategories.PRENDRE;
                break;
            case 3:
                return VignetteCategories.COMBATTRE;
                break;
            case 4:
                return VignetteCategories.UTILISER;
                break;
            case 5:
                return VignetteCategories.PIEGE;
                break;
            case 6:
                return VignetteCategories.CURSE;
                break;
            case 7:
                return VignetteCategories.PERTE_OBJET;
                break;
            case 8:
                return VignetteCategories.VENT_GLACIAL;
                break;
            case 9:
                return VignetteCategories.SAVOIR_OCCULTE;
                break;
            default:
                return VignetteCategories.NEUTRE;
                break;
        }
    }

    private string GetEnumName()
    {
        switch (categorie)
        {
            case VignetteCategories.NEUTRE:
                return "NEUTRE";
                break;
            case VignetteCategories.EXPLORER:
                return "EXPLORER";
                break;
            case VignetteCategories.PRENDRE:
                return "PRENDRE";
                break;
            case VignetteCategories.COMBATTRE:
                return "COMBATTRE";
                break;
            case VignetteCategories.UTILISER:
                return "UTILISER";
                break;
            case VignetteCategories.PIEGE:
                return "PIEGE";
                break;
            case VignetteCategories.CURSE:
                return "CURSE";
                break;
            case VignetteCategories.PERTE_OBJET:
                return "PERTE_OBJET";
                break;
            case VignetteCategories.VENT_GLACIAL:
                return "VENT_GLACIAL";
                break;
            case VignetteCategories.SAVOIR_OCCULTE:
                return "SAVOIR_OCCULTE";
                break;
            default:
                return "NEUTRE";
                break;
        }
    }

    private Sprite GetSprite()
    {
        return CreationManager.instance.GetVignetteSprite(this);
    }

    #region Interface
    public void OnPointerUp(PointerEventData eventData)
    {
        ClearList();

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
                Case_Behaviours caseBehaviours;
                if (item.collider.TryGetComponent<IModifier>(out myModifier))
                {
                    SetUpCard(myModifier);
                    amountOfModifier++;
                }

                if (item.collider.TryGetComponent<Case_Behaviours>(out caseBehaviours))
                {
                    if(caseBehaviours.CaseEffects != null)
                        ListOfCaseEventObject.Add(caseBehaviours.CaseEffects);
                }

                if (!GridManager.instance.ListOfOveredTile.Contains(item.collider.gameObject))
                {
                    vignetteTilePosition.Add(item.collider.gameObject.transform.position);
                    VignetteTile.Add(item.collider.gameObject.GetComponent<TileElt_Behaviours>().Tileposition);
                    VignetteTile.Sort((v1, v2) => (v1.x - v1.y).CompareTo((v2.x - v2.y)));
                    listOfAffectedObject.Add(item.collider.gameObject);
                    GridManager.instance.ListOfOveredTile.Add(item.collider.gameObject);
                }

            }
            if (vignetteTile.Count < (VignetteShape.x * VignetteShape.y))
            {
                ClearList();
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


        foreach (var item in FindObjectsOfType<Vignette_Behaviours>())
        {
            item.CheckIsPositionIsValid();
            item.a = false;
            item.previousMove = nextMove = null;
            item.neighbourgCheck.Clear();
            item.CheckNeighbourg();
            
        }

        GridManager.instance.SortList();

        //GridManager.instance.Test.Clear();
        foreach (var item in GridManager.instance.ListOfMovement)
        {
            Vignette_Behaviours check = item.EventAssocier;
            if (check.OnGrid && check.vignetteTile.Count > 0)
            {
                check.GetNextMove();
            }

        }

        GameManager.instance.IsMovementvalid();
        GridManager.instance.GetVignetteOrderByNeighbourg();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 data = Camera.main.ScreenToWorldPoint(eventData.position);
        data.z = transform.position.z;
        //AJouter la distance entre le pivot et le curseur;
        offset = transform.position - (Vector3)data;

        myEvent.ResetEvent();
        // SetUpUI();
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
            // ShowVignetteElt(vignetteImage, vignetteInfo, .2f);
        }
        else
        {
            m_IsVignetteShowUp = false;
            /*vignetteScene.SetActive(false);
            vignetteInfo.SetActive(true);*/

            //  ShowVignetteElt(vignetteInfo, vignetteImage, .2f);
        }
    }

    #endregion



    private void OnDrawGizmos()
    {
        for (int i = 0; i < physicsCheck.transform.childCount; i++)
        {
            GameObject tile = physicsCheck.transform.GetChild(i).gameObject;
            Gizmos.DrawWireCube(tile.transform.position, transform.localScale / raycastSize);
        }
        //Gizmos.DrawWireCube(transform.GetChild(0).position, transform.localScale / raycastSize);
    }


    #region Getter & Setter
    public List<GameObject> ListOfAffectedObject { get => listOfAffectedObject; set => listOfAffectedObject = value; }
    public EventContener MyEvent { get => myEvent; set => myEvent = value; }
    public Vignette_Behaviours NextMove { get => nextMove; set => nextMove = value; }
    public List<Vector2> VignetteTile { get => vignetteTile; set => vignetteTile = value; }
    public Vector2 VignetteShape { get => vignetteShape; set => vignetteShape = value; }
    public bool OnGrid { get => onGrid; set => onGrid = value; }
    public List<CaseContener_SO> ListOfCaseEventObject { get => listOfCaseEventObject; set => listOfCaseEventObject = value; }
    public VignetteCategories Categorie { get => categorie; set => categorie = value; }
    #endregion
}
