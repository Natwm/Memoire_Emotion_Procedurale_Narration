using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Space]
    [Header("Param")]
    [SerializeField] private int m_AmountOfCrewMember = 2;
    [SerializeField] private Character m_CurrentCharacter;
    [SerializeField] private int amountOfObjToSendCamp = 8;

    [Space]
    [Header("Global Character")]
    [SerializeField] private List<Character> m_Crew;
    [SerializeField] private List<Character_SO> m_GlobalCrew_SO;

    [Space]
    [SerializeField] private List<Character> m_OrderCharacter;
    [SerializeField] private List<Character> m_WaitingCharacter;

    [Space]
    [Header("Prefabs")]
    [SerializeField] private GameObject characterPrefabs;

    [Space]
    [Header("Movement")]
    [SerializeField] private List<Vignette_Behaviours> visitedVignette;

    public Color32 curseColor = new Color32(104, 46, 68, 255);

    public List<Character> OrderCharacter { get => m_OrderCharacter; set => m_OrderCharacter = value; }
    public List<Character> WaitingCharacter { get => m_WaitingCharacter; set => m_WaitingCharacter = value; }
    public List<Character_SO> GlobalCrew_SO { get => m_GlobalCrew_SO; set => m_GlobalCrew_SO = value; }
    public Character CurrentCharacter { get => m_CurrentCharacter; set => m_CurrentCharacter = value; }
    public int AmountOfObjToSendCamp { get => amountOfObjToSendCamp; set => amountOfObjToSendCamp = value; }
    public List<Character> Crew { get => m_Crew; set => m_Crew = value; }
    public List<Vignette_Behaviours> VisitedVignette { get => visitedVignette; set => visitedVignette = value; }

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GameManager");
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateCharacterList(m_AmountOfCrewMember);
        Startpull();

        if(CurrentCharacter!= null)
        {
            RoomGenerator.instance.InitialiseGame();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Character_SO> CreateCharacterList(int _charaAmount = 1)
    {
        List<Character_SO> tempList = new List<Character_SO>();
        foreach (var item in GlobalCrew_SO)
        {
            tempList.Add(item);
        }


        for (int i = 0; i < _charaAmount; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            Character_SO tempCharacter = tempList[randomIndex];

            GameObject character =  Instantiate(characterPrefabs);
            Character dataPlayer = character.GetComponent<Character>();
            dataPlayer.setUpCharacter(tempCharacter);

            OrderCharacter.Add(dataPlayer);
            /*string musique;
            string hurt;*

            switch (i)
            {
                case 0:
                    musique = Character1Sound;
                    hurt = CharacterHurt1Sound;
                    break;
                case 1:
                    musique = Character2Sound;
                    hurt = CharacterHurt2Sound;
                    break;
                case 2:
                    musique = Character3Sound;
                    hurt = CharacterHurt3Sound;
                    break;
                case 3:
                    musique = Character4Sound;
                    hurt = CharacterHurt4Sound;
                    break;

                default:
                    musique = Character1Sound;
                    hurt = CharacterHurt1Sound;
                    break;
            }
            musique = Character1Sound;*/

            //NegociationManager.instance.CreateCharacterButton(character.GetComponent<Character>());

            Crew.Add(dataPlayer);
            tempList.RemoveAt(randomIndex);
        }
        CurrentCharacter = Crew[0];

        return tempList == null ? null : tempList;
    }

    public void Startpull()
    {
        GameManager.instance.OrderCharacter = new List<Character>();
        foreach (var item in Crew)
        {
            GameManager.instance.OrderCharacter.Add(item);
        }

        OrderCharacter.Remove(CurrentCharacter);
        WaitingCharacter.Add(CurrentCharacter);
    }

    public void NextCharacter()
    {
        int index = 0;
        bool can = true;
        foreach (var item in Crew)
        {
            if (item.InventoryObj.Count == 0)
            {
                index++;
                can = false;
            }
        }
        if (can)
            LaunchGame();
        else if (index < 4)
        {
            //RepartitionObject();
            NextCharacter();
        }
        else
        {
            LaunchGame();
        }
    }

    public void LaunchGame()
    {
        CurrentCharacter = OrderCharacter[0];

        if (CurrentCharacter.AssignedElement != null)
        {
            foreach (var item in CurrentCharacter.InventoryObj)
            {
                InventoryManager.instance.GlobalInventoryObj.Add(item);
            }

            WaitingCharacter.Add(CurrentCharacter);
            GameManager.instance.OrderCharacter.RemoveAt(0);
            /*CanvasManager.instance.SetUpCharacterInfo();
            CanvasManager.instance.SetUpGamePanel();*/

            /*if (muteFirstSound)
                PlayerManager.instance.CharacterContener.CharacterSelectedEffect.start();*/
            //CanvasManager.instance.SetUpWaitingCharacterInfo();

        }

    }

    public void ApplyCurseOnObject()
    {
        CanvasManager.instance.MoveButton.interactable = false;
        StartCoroutine(ApplyCurseOnEachObject());
    }

    public IEnumerator ApplyCurseOnEachObject()
    {
        Vignette_Behaviours[] allVignette = FindObjectsOfType<Vignette_Behaviours>();
        int index = -1;
        for (int i = 0; i < allVignette.Length; i++)
        {
            if (!allVignette[i].OnGrid)
            {
                index++;
                if (LevelManager.instance.PageInventory.Count > index)
                {
                    if (LevelManager.instance.PageInventory[index] != null)
                    {
                        int randomCurse = Random.Range(0, 3);

                        CurseBehaviours myCurse;

                        switch (randomCurse)
                        {
                            case 0:
                                myCurse = new Curse_ReduceLife();
                                break;
                            case 1:
                                myCurse = new Curse_Loose_A_LevelObject();
                                break;
                            case 2:
                                myCurse = new Curse_ReduceMental();
                                break;
                            /* case 3:
                                 break;
                             case 4:
                                 break;
                             case 5:
                                 break;*/
                            default:
                                myCurse = new Curse_ReduceLife();
                                break;
                        }
                        print(randomCurse);

                        LevelManager.instance.PageInventory[index].IsCurse = true;
                        LevelManager.instance.PageInventory[index].MyCurse = myCurse;
                        LevelManager.instance.PageInventory[index].gameObject.GetComponent<UnityEngine.UI.Image>().color = GameManager.instance.curseColor;

                    }
                }
                //SoundManager.instance.PlaySound_CurseObject();
                LevelManager.instance.HandOfVignette.Remove(allVignette[i]);
                Destroy(allVignette[i].gameObject);
                yield return new WaitForSeconds(0.5f);
            }

        }
        MoveToAnotherStep();
    }

    public void MoveToAnotherStep()
    {
        if (GridManager.instance.ListOfMovement.Count > 0)
            StartCoroutine(MoveToLocationByVignette());
        else
        {
            GridManager.instance.SortList();
            StartCoroutine(MoveToLocationByVignette());
        }
    }

    private IEnumerator MoveToLocationByVignette()
    {
        /*GameObject targetedVignette = GridManager.instance.ListOfMovement[0].EventAssocier.gameObject;
        Vector3 newPosition = GridManager.instance.ListOfMovement[0].EventAssocier.transform.position;
        GameObject tile = GridManager.instance.ListOfMovement[0].gameObject;*/

        Vignette_Behaviours targetedVignette = GridManager.instance.ListOfMovement[0].EventAssocier;
        Vector3 newPosition = GridManager.instance.ListOfMovement[0].transform.position;
        GameObject tile = GridManager.instance.ListOfMovement[0].gameObject;

        newPosition.Set(newPosition.x, newPosition.y, -5f);

        if (!VisitedVignette.Contains(targetedVignette))
        {
            VisitedVignette.Add(targetedVignette);
            this.transform.DOMove(newPosition, 1f);

            yield return new WaitForSeconds(.8f);

            StartCoroutine(CameraManager.instance.MoveCameraToTarget(newPosition));

            targetedVignette.ApplyVignetteEffect();

            //CHeck les cases en dessous

            GridManager.instance.ListOfMovement.RemoveAt(0);

            yield return new WaitForSeconds(2f);

            StartCoroutine(CameraManager.instance.LerpZoomFunction(CameraManager.instance.UnZoomValue, 1f));

            yield return new WaitForSeconds(.8f);

            if (GridManager.instance.ListOfMovement.Count > 0)
            {
                StartCoroutine(MoveToLocationByVignette());
            }
            else
            {

                yield return new WaitForSeconds(1.5f);
                EndPage();
            }

        }
        else
        {
            GridManager.instance.ListOfMovement.RemoveAt(0);
            if (GridManager.instance.ListOfMovement.Count > 0)
            {
                StartCoroutine(MoveToLocationByVignette());
            }
            else
            {
                yield return new WaitForSeconds(1.5f);
                EndPage();
            }
        }

    }

    public void GameOver()
    {

    }

    public void EndPage()
    {
        //SoundManager.instance.PlaySound_EndResolution();
        CanvasManager.instance.LevelInventoryPanel1.transform.parent.gameObject.SetActive(false);
        if (CurrentCharacter.Life <= 0)
        {
            print("nope Death");
            StopAllCoroutines();
        }

        RoomGenerator.instance.OnRoomCompletion();

        LineRendererScript.instance.DrawLineRenderer();
    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
