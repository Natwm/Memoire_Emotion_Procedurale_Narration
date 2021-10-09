using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    Keyboard kb;

    [Header ("Health")]
    [SerializeField] private int maxHappyness = 5;
    [SerializeField] private int minSadness = - 5;
    [SerializeField] private int Player_Happy_SadValue;

    [Space]
    [Header("Stamina")]
    [SerializeField] private int maxAngry = 5;
    [SerializeField] private int minFear = -5;
    [SerializeField] private int Player_Angry_FearValue;

    [Space]
    [Header("Key")]
    [SerializeField] private bool haveKey = false;

    [Space]
    [Header("Hand")]
    [SerializeField] private List<Bd_Elt_Behaviours> handOfVignette;
    [SerializeField] private int minCardToDraw = 4;
    [SerializeField] private int amountOfCardToDraw;

    [Space]
    [SerializeField] private int drawCoast;

    [Space]
    [Header("Movement")]
    [SerializeField] private List<GameObject> visitedVignette;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : PlayerManager");
        else
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        Player_Angry_FearValue = maxAngry;
        amountOfCardToDraw = minCardToDraw;

        visitedVignette = new List<GameObject>();

        CanvasManager.instance.UpdateInformationText(Player_Happy_SadValue, Player_Angry_FearValue, amountOfCardToDraw);

        kb = InputSystem.GetDevice<Keyboard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp()
    {
        amountOfCardToDraw = minCardToDraw;
        CanvasManager.instance.UpdateVignetteToDraw(amountOfCardToDraw);
    }

    void GameOver()
    {
        EndMovement();
    }

    public void MoveToAnotherStep()
    {
        print(GridManager.instance.ListOfMovement.Count);
        if(GridManager.instance.ListOfMovement.Count >0)
            StartCoroutine(MoveToLocationByVignette());
        else
        {
            GridManager.instance.SortList();
            StartCoroutine(MoveToLocationByVignette());
        }
    }

    private IEnumerator MoveToLocationByCase()
    {
        Vector3 newPosition = GridManager.instance.ListOfMovement[0].transform.position;
        GameObject tile = GridManager.instance.ListOfMovement[0].gameObject;

        newPosition.Set(newPosition.x, newPosition.y, -5f);
        this.transform.DOMove(newPosition, 1f);
        GridManager.instance.ListOfMovement.RemoveAt(0);

        CanvasManager.instance.NewLogEntry("");

        yield return new WaitForSeconds(.8f);
        //LooseMovement(1);

        if (tile.GetComponent<TileElt_Behaviours>()!= null)
        {
            TileElt_Behaviours cardEvent = tile.GetComponent<TileElt_Behaviours>();
            tile.GetComponent<TileElt_Behaviours>().ApplyEffect(this);
        }

        yield return new WaitForSeconds(1.5f);

        if(GridManager.instance.ListOfMovement.Count > 0 && Player_Happy_SadValue>0)
        {
            StartCoroutine(MoveToLocationByCase());
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            EndMovement();
        }
    }

    private IEnumerator MoveToLocationByVignette()
    {
        print("ok");//marche pas a regarder.

        GameObject targetedVignette = GridManager.instance.ListOfMovement[0].EventAssocier.gameObject;
        Vector3 newPosition = GridManager.instance.ListOfMovement[0].EventAssocier.transform.position;
        GameObject tile = GridManager.instance.ListOfMovement[0].gameObject;

        newPosition.Set(newPosition.x, newPosition.y, -5f);

        if (!visitedVignette.Contains(targetedVignette))
        {
            visitedVignette.Add(targetedVignette);
            this.transform.DOMove(newPosition, 1f);

            CanvasManager.instance.NewLogEntry("");
            
            yield return new WaitForSeconds(.8f);

            if (tile.GetComponent<TileElt_Behaviours>() != null)
            {
                TileElt_Behaviours cardEvent = tile.GetComponent<TileElt_Behaviours>();
                tile.GetComponent<TileElt_Behaviours>().ApplyEffect(this);
            }

            GridManager.instance.ListOfMovement.RemoveAt(0);

            yield return new WaitForSeconds(1.5f);

            if (GridManager.instance.ListOfMovement.Count > 0 && Player_Happy_SadValue > 0)
            {
                StartCoroutine(MoveToLocationByVignette());
            }
            else
            {
                yield return new WaitForSeconds(1.5f);
                EndMovement();
            }
        }
        else
        {
            GridManager.instance.ListOfMovement.RemoveAt(0);
            if (GridManager.instance.ListOfMovement.Count > 0 && Player_Happy_SadValue > 0)
            {
                StartCoroutine(MoveToLocationByVignette());
            }
            else
            {
                yield return new WaitForSeconds(1.5f);
                EndMovement();
            }
        }
        
    }

    void EndMovement()
    {
        if (Player_Happy_SadValue == maxHappyness || Player_Happy_SadValue == minSadness || Player_Angry_FearValue == maxAngry || Player_Angry_FearValue == minFear) {
            print("nope Death");
            //DebugManager.instance.ReloadScene();
            StopAllCoroutines();
            CanvasManager.instance.PlayerLooseTheGame();
        }
        else
        {
            //GridManager.instance.ClearScene();
            CanvasManager.instance.PlayerWinTheGame();

        }
    }

    #region Happyness_Sadness Event
    public void Update_Happyness_Sadness( int point)
    {
        print("GainHeath " + point);
        Player_Happy_SadValue += point;
        print("Heal by "+ point +" point");
        CanvasManager.instance.Update_Happy_Sadness_Status(Player_Happy_SadValue);

        if (Player_Happy_SadValue <= 0)
            GameOver();
    }
    #endregion

    #region Angry_Fear Event
    public void Update_Angry_Fear(int point)
    {
        Player_Angry_FearValue += point;
        print("stamina gain by " + point + " point");
        CanvasManager.instance.Update_Angry_Fear_Status(Player_Angry_FearValue);
    }

    public void LooseMovement(int point)
    {
        Player_Angry_FearValue -= point;
        print("stamina loose by " + point + " point");
        if (Player_Angry_FearValue < 0)
            GameOver();
        CanvasManager.instance.Update_Angry_Fear_Status(Player_Angry_FearValue);
    }
    #endregion

    #region Key Logique

    public void PickUpKey()
    {
        HaveKey = true;
    }

    #endregion

    #region Hand

    public void HandModifier(int amountOfCard)
    {
        AmountOfCardToDraw += amountOfCard; ;
        CanvasManager.instance.UpdateVignetteToDraw(AmountOfCardToDraw);
    }

    private void DrawVignette(int amountOfCard)
    {
        print("okdqd");
        LevelManager.instance.SpawnObject(1);
    }

    public void DrawVignetteByStamina()
    {
        if(Player_Angry_FearValue - drawCoast >= 0)
        {
            Player_Angry_FearValue -= drawCoast;
            LevelManager.instance.SpawnObject(1);
            CanvasManager.instance.Update_Angry_Fear_Status(Player_Angry_FearValue);
        }
        
    }

    private void DiscardVignette()
    {
        int selectedCard = Random.Range(0, HandOfVignette.Count);
        Bd_Elt_Behaviours vignetteToDelete = HandOfVignette[selectedCard];
        HandOfVignette.Remove(vignetteToDelete);
        Destroy(vignetteToDelete.gameObject);

    }

    #endregion

    #region Getter && Setter
    public int MaxHealth { get => maxHappyness; set => maxHappyness = value; }
    public int Health { get => Player_Happy_SadValue; set => Player_Happy_SadValue = value; }
    public bool HaveKey { get => haveKey; set => haveKey = value; }
    public List<Bd_Elt_Behaviours> HandOfVignette { get => handOfVignette; set => handOfVignette = value; }
    public int MinCardToDraw { get => minCardToDraw; set => minCardToDraw = value; }
    public int AmountOfCardToDraw { get => amountOfCardToDraw; set => amountOfCardToDraw = value; }
    #endregion
}
