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
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int health;

    [Space]
    [Header("Stamina")]
    [SerializeField] private int maxStamina = 5;
    [SerializeField] private int stamina;

    [Space]
    [Header("Key")]
    [SerializeField] private bool haveKey = false;

    [Space]
    [Header("Hand")]
    [SerializeField] private List<Bd_Elt_Behaviours> handOfVignette;
    [SerializeField] private int minCardToDraw = 4;
    [SerializeField] private int amountOfCardToDraw;

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
        stamina = maxStamina;
        amountOfCardToDraw = minCardToDraw;

        visitedVignette = new List<GameObject>();

        CanvasManager.instance.UpdateInformationText(health, stamina, amountOfCardToDraw);

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
            StartCoroutine(MoveToLocation());
        else
        {
            GridManager.instance.SortList();
            StartCoroutine(MoveToLocation());
        }
    }

    private IEnumerator MoveToLocation()
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

        if(GridManager.instance.ListOfMovement.Count > 0 && health>0)
        {
            StartCoroutine(MoveToLocation());
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            EndMovement();
        }
    }

    private IEnumerator MoveToLocation2()
    {
        print("ok");//marche pas a regarder.

        GameObject targetedVignette = GridManager.instance.ListOfMovement[0].EventAssocier.gameObject;
        Vector3 newPosition = GridManager.instance.ListOfMovement[0].transform.position;
        GameObject tile = GridManager.instance.ListOfMovement[0].gameObject;

        newPosition.Set(newPosition.x, newPosition.y, -5f);
        Debug.Break();
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

        }
        GridManager.instance.ListOfMovement.RemoveAt(0);

        yield return new WaitForSeconds(1.5f);

        if (GridManager.instance.ListOfMovement.Count > 0 && health > 0)
        {
            StartCoroutine(MoveToLocation2());
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            EndMovement();
        }
    }

    void EndMovement()
    {
        if (health < 0) {
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

    #region LifeEvent
    public void GainHeath( int point)
    {
        print("GainHeath " + point);
        health += point;
        print("Heal by "+ point +" point");
        CanvasManager.instance.UpdateLifePoint(health);
    }

    public void LooseHeath (int point)
    {
        health -= point;
        print("damage deal by " + point + " point");
        if (health <= 0)
            GameOver();

        CanvasManager.instance.UpdateLifePoint(health);
    }
    #endregion

    #region MovementEvent
    public void GainMovement(int point)
    {
        stamina += point;
        print("stamina gain by " + point + " point");
        CanvasManager.instance.UpdateStaminaPoint(stamina);
    }

    public void LooseMovement(int point)
    {
        stamina -= point;
        print("stamina loose by " + point + " point");
        if (stamina < 0)
            GameOver();
        CanvasManager.instance.UpdateStaminaPoint(stamina);
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

    private void DiscardVignette()
    {
        int selectedCard = Random.Range(0, HandOfVignette.Count);
        Bd_Elt_Behaviours vignetteToDelete = HandOfVignette[selectedCard];
        HandOfVignette.Remove(vignetteToDelete);
        Destroy(vignetteToDelete.gameObject);

    }

    #endregion

    #region Getter && Setter
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Health { get => health; set => health = value; }
    public bool HaveKey { get => haveKey; set => haveKey = value; }
    public List<Bd_Elt_Behaviours> HandOfVignette { get => handOfVignette; set => handOfVignette = value; }
    public int MinCardToDraw { get => minCardToDraw; set => minCardToDraw = value; }
    public int AmountOfCardToDraw { get => amountOfCardToDraw; set => amountOfCardToDraw = value; }
    #endregion
}
