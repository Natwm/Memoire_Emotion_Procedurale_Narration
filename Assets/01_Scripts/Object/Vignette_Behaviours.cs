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
        RESSEMBLACE_ETRANGE
    }

    #region param

    private Vector3 offset;

    [SerializeField] private VignetteCategories currentCategorie;
    [SerializeField] private VignetteCategories initCategorie;

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
    [SerializeField] private TMPro.TMP_Text objetText;
    [SerializeField] private TMPro.TMP_Text healthText;
    [SerializeField] private TMPro.TMP_Text mentalHealthText;

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

    [Space]
    [Header("Object")]
    [SerializeField] private UsableObject objectFrom;

    [Space]
    [Header("Sound Fmod Action")]
    FMOD.Studio.EventInstance takeVignetteEffect;
    [FMODUnity.EventRef] [SerializeField] private string takeVignetteSound;

    FMOD.Studio.EventInstance dropVignetteEffect;
    [FMODUnity.EventRef] [SerializeField] private string dropVignetteSound;

    FMOD.Studio.EventInstance transformationVignetteEffect;
    [FMODUnity.EventRef] [SerializeField] private string transformationVignetteSound;

    [Space]
    [Header("Sound Fmod Resolution")]
    FMOD.Studio.EventInstance reveleEffect;
    [FMODUnity.EventRef] [SerializeField] private string reveleSound;

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

        m_IsVignetteShowUp = false;

        vignetteScene = transform.GetChild(1).gameObject;

        vignetteImage = vignetteScene.transform.GetChild(0).gameObject;
        vignetteImage.SetActive(false);

        myEvent = GetComponent<EventContener>();
        //SetUpCard();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {

        }
    }

    public void ApplyVignetteEffect()
    {

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
            case VignetteCategories.SMALL_HEAL:
                SmallHealEffect();
                break;
            case VignetteCategories.BIG_HEAL:
                FullHealEffect();
                break;
            case VignetteCategories.SOUFFLER:
                SoufflerEffect();
                break;
            default:
                break;
        }

        if (objectFrom != null)
        {
            if (objectFrom.IsCurse)
            {
                objectFrom.MyCurse.ApplyCurse();
            }
        }
    }

    #region SETUP
    public void SetUpVignette(VignetteCategories categorie)
    {
        Categorie = initCategorie = categorie;
        categorieText.text = GetEnumName();
        SpriteIndicator.sprite = null;
        SetUpUI();
    }


    public void SetUpVignette(VignetteCategories categorie, Object_SO useObject)
    {
        Categorie = initCategorie = categorie;
        categorieText.text = GetEnumName();
        print(useObject);
        SpriteIndicator.sprite = useObject.Sprite;
        objectFrom = null;
        SetUpUI();
    }


    string vignetteText;
    string curseText;
    public void SetUpVignette(VignetteCategories categorie, UsableObject useObject)
    {
        Categorie = initCategorie = categorie;
        vignetteText = GetEnumName();
        
        SpriteIndicator.sprite = useObject.Data.Sprite;

        if (useObject.IsCurse)
        {
            SpriteIndicator.color = new Color(104,46,68,255);
            curseText = GetCurseName(useObject);
        }
        else
        {
            curseText = "";
        }

        categorieText.text = vignetteText + curseText;


        objectFrom = useObject;
        SetUpUI();
    }
    #endregion

    #region VIgnette Effect

    public void NeutralEffect()
    {
        print("neutre");
    }

    public void ExploreEffect()
    {
        print("ExploreEffect");

        int randomIndex = UnityEngine.Random.Range(0, LevelManager.instance.UnlockableObject.Count);
        Object_SO newItem = LevelManager.instance.UnlockableObject[randomIndex];

        
        GameObject item = CanvasManager.instance.NewItemInLevelInventory(newItem);
        item.GetComponent<UsableObject>().Data = newItem;

        LevelManager.instance.PageInventory.Add(item.GetComponent<UsableObject>());
    }

    public void TakeEffect()
    {
        print("Take Effect off : " + LevelManager.instance.PageInventory.Count + " Item");
        foreach (var item in LevelManager.instance.PageInventory)
        {
            //CreationManager.instance.GlobalInventory.Add(item);
            item.transform.parent = CreationManager.instance.pulledObject.transform;
            item.gameObject.SetActive(false);
        }
        //CanvasManager.instance.ClearLevelInventory();
        LevelManager.instance.PageInventory = new List<UsableObject>();
    }

    public void FightEffect()
    {
        print("FightEffect");
        PlayerManager.instance.GetDamage(2);
    }

    public void UseEffect()
    {
        print("UseEffect"); // check si il y a condition
        //CheckCaseCondition();
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
            CanvasManager.instance.RemoveObjInPlayerInventory(index);
        }
    }

    public void Vent_GlacialEffect()
    {
        print("Vent_GlacialEffect");
        CanvasManager.instance.ClearLevelInventory();
        LevelManager.instance.PageInventory = new List<UsableObject>();
    }

    public void Savoir_OcculteEffect()
    {
        print("Savoir_OcculteEffect");
        for (int i = 0; i < 2; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, LevelManager.instance.UnlockableObject.Count);

            LevelManager.instance.PageInventory.Add(new UsableObject(LevelManager.instance.UnlockableObject[randomIndex]));
        }

        //draw 2 vignette negatif et 1 explorer.
    }
    public void SoufflerEffect()
    {
        PlayerManager.instance.HealMentalPlayer(1);
    }

    public void SmallHealEffect()
    {
        print("SmallHealEffect");
        PlayerManager.instance.HealPlayer(1);
    }

    public void FullHealEffect()
    {
        print("FullHealEffect");
        PlayerManager.instance.HealPlayer(100);
    }

    public void EclairerEffect()
    {
        print("Trouve deux objet alea");
        for (int i = 0; i < 2; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, LevelManager.instance.UnlockableObject.Count);

            LevelManager.instance.PageInventory.Add(new UsableObject(LevelManager.instance.UnlockableObject[randomIndex]));
        }
    }

    public void CameraEffect()
    {
        print("Camera Effect");
    }

    #endregion

    #region Check
    public bool CheckCaseCondition()
    {
        print("CheckCaseCondition");

        if (ListOfCaseEventObject.Count > 0)
        {
            print("check");
            foreach (var condition in ListOfCaseEventObject)
            {
                if (condition.AnyVignette)
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
            //
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
                for (int x = 0; x <= 1; x++)
                {
                    for (int y = 0; y <= 1; y++)
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
            //print("Next move is :    " + NextMove.gameObject);
        }
        else
        {
            //print("NextMove est sensé être  =" + check);
            //print("il est null car : check != this =" + (check != this));
        }

    }
    #endregion

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
        switch (Categorie)
        {
            case VignetteCategories.NEUTRE:
                SetUpTextVignette(0,0,0);
                break;
            case VignetteCategories.EXPLORER:
                SetUpTextVignette(1,0,0);
                break;
            case VignetteCategories.PRENDRE:
                SetUpTextVignette("All");
                break;
            case VignetteCategories.COMBATTRE:
                SetUpTextVignette(0,0,-2);
                break;
            case VignetteCategories.UTILISER:
                SetUpTextVignette("");
                break;
            case VignetteCategories.PIEGE:
                SetUpTextVignette(0,0,-1);
                break;
            case VignetteCategories.CURSE:
                SetUpTextVignette(0,-1,0);
                break;
            case VignetteCategories.PERTE_OBJET:
                SetUpTextVignette(-1);
                break;
            case VignetteCategories.VENT_GLACIAL:
                SetUpTextVignette("NONE");
                break;
            case VignetteCategories.SMALL_HEAL:
                SetUpTextVignette(0,0,1);
                break;
            case VignetteCategories.BIG_HEAL:
                SetUpTextVignette(0, 0, 10);
                break;
            case VignetteCategories.SOUFFLER:
                SetUpTextVignette(0,1,0);
                break;
            /*case VignetteCategories.RESSURECTION:
                SetUpTextVignette("");
                break;
            case VignetteCategories.PLANIFICATION:
                SetUpTextVignette("");
                break;
            case VignetteCategories.SOIN_EQUIPE:
                SetUpTextVignette();
                break;
            case VignetteCategories.DEBROUILLARD:
                SetUpTextVignette();
                break;
            case VignetteCategories.INSTANTANE:
                SetUpTextVignette();
                break;
            case VignetteCategories.RESSEMBLACE_ETRANGE:
                SetUpTextVignette();
                break;*/
            default:
                break;
        }

    }

    void SetUpTextVignette(int nbObjet = 0, int nbMetalHeath = 0, int nbHeath = 0)
    {
        objetText.text = nbObjet.ToString();
        mentalHealthText.text = nbMetalHeath.ToString();
        healthText.text = nbHeath.ToString();
    }
    void SetUpTextVignette(string nbObjet = "", string nbMetalHeath = "", string nbHeath = "")
    {
        objetText.text = nbObjet;
        mentalHealthText.text = nbMetalHeath;
        healthText.text = nbHeath;
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

    public static VignetteCategories GetRandomEnum()
    {
        int value = UnityEngine.Random.Range(0, 10);

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
            case 10:
                return VignetteCategories.SMALL_HEAL;
                break;
            case 11:
                return VignetteCategories.ECLAIRER;
                break;
            case 12:
                return VignetteCategories.RESSURECTION;
                break;
            case 13:
                return VignetteCategories.PLANIFICATION;
                break;
            case 14:
                return VignetteCategories.SOIN_EQUIPE;
                break;
            case 15:
                return VignetteCategories.DEBROUILLARD;
                break;
            case 16:
                return VignetteCategories.SOUFFLER;
                break;
            case 17:
                return VignetteCategories.INSTANTANE;
                break;
            case 18:
                return VignetteCategories.RESSEMBLACE_ETRANGE;
                break;
            default:
                return VignetteCategories.NEUTRE;
                break;
        }
    }

    public static VignetteCategories GetRandomNegatifEnum()
    {
        int value = UnityEngine.Random.Range(0, 4);

        switch (value)
        {
            case 0:
                return VignetteCategories.COMBATTRE;
                break;
            case 1:
                return VignetteCategories.PIEGE;
                break;
            case 2:
                return VignetteCategories.CURSE;
                break;
            case 3:
                return VignetteCategories.PERTE_OBJET;
                break;

            default:
                return VignetteCategories.NEUTRE;
                break;
        }
    }

    private string GetEnumName()
    {
        switch (currentCategorie)
        {
            case VignetteCategories.NEUTRE:
                return "<br>Neutre";
                break;
            case VignetteCategories.EXPLORER:
                return "<color=#B5935A>+1<sprite=1 color=#B5935A></color=#B5935A><br><size=100%>Explorer";
                break;
            case VignetteCategories.PRENDRE:
                return "<color=#B5935A><sprite=3 color=#B5935A></color=#B5935A><br>Prendre";
                break;
            case VignetteCategories.COMBATTRE:
                return "<color=#B5935A>-2<sprite=0 color=#B5935A></color=#B5935A><br>Combattre";
                break;
            case VignetteCategories.UTILISER:
                return "<br>Utiliser";
                break;
            case VignetteCategories.PIEGE:
                return "<color=#B5935A>-1<sprite=0 color=#B5935A></color=#B5935A><br>Piège";
                break;
            case VignetteCategories.CURSE:
                return "<color=#B5935A>-1<sprite=2 color=#B5935A></color=#B5935A><br>Malédiction";
                break;
            case VignetteCategories.PERTE_OBJET:
                return "<color=#B5935A>-1<sprite=1 color=#B5935A></color=#B5935A><br>Perte d'objet";
                break;
            case VignetteCategories.VENT_GLACIAL:
                return "VENT_GLACIAL";
                break;
            case VignetteCategories.SAVOIR_OCCULTE:
                return "<color=#B5935A>-1<sprite=2 color=#B5935A><br>+1<sprite=1 color=#B5935A></color=#B5935A><br>Savoir Occulte";
                break;
            case VignetteCategories.SMALL_HEAL:
                return "<color=#B5935A>+1<sprite=0 color=#B5935A></color=#B5935A><br>Soin Léger";
                break;
            case VignetteCategories.BIG_HEAL:
                return "<color=#B5935A>+1<sprite=0 color=#B5935A></color=#B5935A><br>Soin Léger";
                break;
            case VignetteCategories.ECLAIRER:
                return "<color=#B5935A>+2<sprite=1 color=#B5935A></color=#B5935A><br><size=100%>Éclairer";
                break;
            case VignetteCategories.RESSURECTION:
                return "<color=#B5935A><sprite=4 color=#B5935A></color=#B5935A><br><size=90%>Résurrection";
                break;
            case VignetteCategories.PLANIFICATION:
                return "<color=#B5935A>+30<sprite=5 color=#B5935A></color=#B5935A><br><size=90%>Planification";
                break;
            case VignetteCategories.SOIN_EQUIPE:
                return "<color=#B5935A>(Tous) +1<sprite=0 color=#B5935A></color=#B5935A><br><size=90%>Soin de l'équipe";
                break;
            case VignetteCategories.DEBROUILLARD:
                return "<color=#B5935A>Utilisable sur toute les cases</color=#B5935A><br><size=90%>Débrouillard";
                break;
            case VignetteCategories.SOUFFLER:
                return "<color=#B5935A>+ 1<sprite=2 color=#B5935A></color=#B5935A><br><size=100%>Bref Répit";
                break;
            case VignetteCategories.INSTANTANE:
                return "INSTANTANE";
                break;
            case VignetteCategories.RESSEMBLACE_ETRANGE:
                return "RESSEMBLACE_ETRANGE";
                break;
            default:
                return "<br> Neutre";
                break;
        }
    }

    private string GetCurseName(UsableObject useObject)
    {
        switch (useObject.MyCurse.CurseName)
        {
            case "Reduce Life":
                return "<br><color=#682e44>-1<sprite=0 color=#682e44></color>";
                break;
            case "Reduce Mental":
                return "<br><color=#682e44>-1<sprite=2 color=#682e44></color>";
                break;
            case "Loose an object":
                return "<br><color=#682e44>-1<sprite=1 color=#682e44></color>";
                break;
            default:
                return "";
                break;
        }
    }

    private Sprite GetSprite()
    {
        return CreationManager.instance.GetVignetteSprite(this);
    }

    #region Update Vignette

    public void ApplyTileEffect(VignetteCategories newCategorie)
    {
        currentCategorie = newCategorie;
        categorieText.text = GetEnumName();
        SetUpUI();
    }

    public void ResetVignette()
    {
        currentCategorie = initCategorie;
        categorieText.text = GetEnumName();
        SetUpUI();
    }

    #endregion

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
                    if (caseBehaviours.CaseEffects != null)
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
            //vignetteScene.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
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

        
        GridManager.instance.GetVignetteOrderByNeighbourg();

        GameManager.instance.CheckIfAllAreConnect();//IsMovementvalid();

        CheckCaseCondition();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ResetVignette();
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
    public VignetteCategories Categorie { get => currentCategorie; set => currentCategorie = value; }
    public UsableObject ObjectFrom { get => objectFrom; set => objectFrom = value; }
    #endregion
}
