using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Space]
    [Header("Health")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int health ;

    [Header ("Happyness - Sadness")]
    [SerializeField] private int maxHappyness = 5;
    [SerializeField] private int minSadness = - 5;
    [SerializeField] private int player_Happy_SadValue;

    [Space]
    [Header("Angry - Fear")]
    [SerializeField] private int maxAngry = 5;
    [SerializeField] private int minFear = -5;
    [SerializeField] private int player_Angry_FearValue;

    [Space]
    [Header("Key")]
    [SerializeField] private bool haveKey = false;

    [Space]
    [Header("Hand")]
    [SerializeField] private List<Vignette_Behaviours> handOfVignette;
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
        player_Happy_SadValue = 0;
        player_Angry_FearValue = 0;
        Health = maxHealth;

        amountOfCardToDraw = minCardToDraw;

        visitedVignette = new List<GameObject>();

        CanvasManager.instance.UpdateInformationText(player_Happy_SadValue, player_Angry_FearValue, amountOfCardToDraw);

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

    public void ResetPlayerPosition()
    {
        transform.DOMove(new Vector2(-2, -2), 0.1f);
    }

    #region Movement

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

        if(GridManager.instance.ListOfMovement.Count > 0 && player_Happy_SadValue>0)
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

            if (GridManager.instance.ListOfMovement.Count > 0 )
            {
                StartCoroutine(MoveToLocationByVignette());
            }
            else
            {

                yield return new WaitForSeconds(1.5f);
                EndMovement();
            }
            /*if (GridManager.instance.ListOfMovement.Count > 0 && (player_Happy_SadValue != maxHappyness || player_Happy_SadValue != minSadness) && (player_Angry_FearValue != maxAngry || player_Angry_FearValue != minFear))
            {
                StartCoroutine(MoveToLocationByVignette());
            }
            else
            {

                yield return new WaitForSeconds(1.5f);
                EndMovement();
            }*/
        }
        else
        {
            GridManager.instance.ListOfMovement.RemoveAt(0);
            if (GridManager.instance.ListOfMovement.Count > 0 && player_Happy_SadValue > 0)
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
        if (player_Happy_SadValue >= maxHappyness || player_Happy_SadValue <= minSadness || player_Angry_FearValue >= maxAngry || player_Angry_FearValue <= minFear) {
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

    #endregion

    #region Happyness_Sadness Event
    public void Update_Happyness_Sadness( int point)
    {
        print("GainHeath " + point);
        player_Happy_SadValue += point;
        print("Heal by "+ point +" point");

        if (player_Happy_SadValue > MaxHappyness)
            player_Happy_SadValue = MaxHappyness;
        if (player_Happy_SadValue < minSadness)
            player_Happy_SadValue = minSadness;

        CanvasManager.instance.Update_Happy_Sadness_Status(player_Happy_SadValue);
        
        /*if (player_Happy_SadValue == maxHappyness || player_Happy_SadValue == minSadness)
            GameOver();*/
    }
    #endregion

    #region Angry_Fear Event
    public void Update_Angry_Fear(int point)
    {
        player_Angry_FearValue += point;
        if(player_Angry_FearValue>maxAngry )
            player_Angry_FearValue = maxAngry;
        if(player_Angry_FearValue < minFear)
            player_Angry_FearValue = minFear;

        print("stamina gain by " + point + " point");
        CanvasManager.instance.Update_Angry_Fear_Status(player_Angry_FearValue);
    }

    public void LooseMovement(int point)
    {
        player_Angry_FearValue -= point;
        print("stamina loose by " + point + " point");
        if (player_Angry_FearValue == maxAngry || player_Angry_FearValue == minFear)
            GameOver();
        CanvasManager.instance.Update_Angry_Fear_Status(player_Angry_FearValue);
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
        if(player_Angry_FearValue - drawCoast >= 0)
        {
            player_Angry_FearValue -= drawCoast;
            LevelManager.instance.SpawnObject(1);
            CanvasManager.instance.Update_Angry_Fear_Status(player_Angry_FearValue);
        }
        
    }

    private void DiscardVignette()
    {
        int selectedCard = Random.Range(0, HandOfVignette.Count);
        Vignette_Behaviours vignetteToDelete = HandOfVignette[selectedCard];
        HandOfVignette.Remove(vignetteToDelete);
        Destroy(vignetteToDelete.gameObject);

    }

    #endregion

    #region Getter && Setter
    public int MaxHappyness { get => maxHappyness; set => maxHappyness = value; }
    public int Player_Happy_SadValue { get => player_Happy_SadValue; set => player_Happy_SadValue = value; }
    public bool HaveKey { get => haveKey; set => haveKey = value; }
    public List<Vignette_Behaviours> HandOfVignette { get => handOfVignette; set => handOfVignette = value; }
    public int MinCardToDraw { get => minCardToDraw; set => minCardToDraw = value; }
    public int AmountOfCardToDraw { get => amountOfCardToDraw; set => amountOfCardToDraw = value; }
    public int Health { get => health; set => health = value; }
    #endregion
}
