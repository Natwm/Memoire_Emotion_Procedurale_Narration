using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public static PlayerManager instance;
    [SerializeField] private Character_SO characterData;
    private Character_Button characterContener;

    [Space]
    [Header("Health")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int health ;

    [Space]
    [Header("Endurance")]
    [SerializeField] private int m_Endurance;
    [Min(1)]
    [SerializeField] private int m_MaxEndurance = 1;

    [Space]
    [Header("Mental Health")]
    [SerializeField] private int m_MentalHealth;
    [Min(1)]
    [SerializeField] private int m_MaxMentalHealth = 1;

    [Space]
    [Header("Inventory")]
    [SerializeField] private int m_InventorySize;
    [Min(1)]
    [SerializeField] private int m_MaxInventorySize = 1;

    [Space]

    [SerializeField] private List<Object_SO> m_Inventory;
    [SerializeField] private List<UsableObject> m_InventoryObj;


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
    [SerializeField] private List<Vignette_Behaviours> visitedVignette;



    //fairesantémental.
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
        m_InventorySize = MaxInventorySize;
        health = MaxHealth;
        m_Endurance = m_MaxEndurance;

        amountOfCardToDraw = minCardToDraw;

        visitedVignette = new List<Vignette_Behaviours>();

        //CanvasManager.instance.UpdateInformationText(player_Happy_SadValue, player_Angry_FearValue, amountOfCardToDraw);

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

    public void SetUpCharacter(Character_SO characterData)
    {
        maxHealth = characterData.MaxHealth;
        health = characterData.Health;

        MaxEndurance = characterData.MaxEndurance;
        Endurance = characterData.Endurance;

        MaxInventorySize = characterData.MaxInventorySize;
        InventorySize = characterData.InventorySize;

        Inventory = new List<Object_SO>();
        foreach (var item in characterData.Inventory)
        {
            Inventory.Add(item);
        }
        

        CharacterData = characterData;
    }
    public void SetUpCharacter(Character_Button characterData)
    {
        maxHealth = characterData.MaxLife;
        health = characterData.Life;

        MaxEndurance = characterData.MaxEndurance;
        Endurance = characterData.Endurance;

        MaxInventorySize = characterData.MaxInventorySize;
        InventorySize = characterData.InventorySize;

        Inventory = new List<Object_SO>();
        foreach (var item in characterData.Inventory)
        {
            Inventory.Add(item);
        }

        foreach (var item in characterData.InventoryObj)
        {
            InventoryObj.Add(item);
        }

        CharacterData = characterData.AssignedElement;
        characterContener = characterData;
    }
    public void SetUpCharacter()
    {
        maxHealth = characterContener.MaxLife;
        health = characterContener.Life;

        MaxEndurance = characterContener.MaxEndurance;
        Endurance = characterContener.Endurance;

        MaxInventorySize = characterContener.MaxInventorySize;
        InventorySize = characterContener.InventorySize;

        Inventory = new List<Object_SO>();
        foreach (var item in characterContener.Inventory)
        {
            Inventory.Add(item);
        }

        CharacterData = characterContener.AssignedElement;

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

        if(GridManager.instance.ListOfMovement.Count > 0)
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
        /*GameObject targetedVignette = GridManager.instance.ListOfMovement[0].EventAssocier.gameObject;
        Vector3 newPosition = GridManager.instance.ListOfMovement[0].EventAssocier.transform.position;
        GameObject tile = GridManager.instance.ListOfMovement[0].gameObject;*/

        Vignette_Behaviours targetedVignette = GridManager.instance.Test[0];
        Vector3 newPosition = GridManager.instance.Test[0].transform.position;
        GameObject tile = GridManager.instance.Test[0].gameObject;

        newPosition.Set(newPosition.x, newPosition.y, -5f);

        if (!visitedVignette.Contains(targetedVignette))
        {
            visitedVignette.Add(targetedVignette);
            this.transform.DOMove(newPosition, 1f);

            CanvasManager.instance.NewLogEntry("");
            
            yield return new WaitForSeconds(.8f);

            StartCoroutine(CameraManager.instance.MoveCameraToTarget(newPosition));
            
            targetedVignette.ApplyVignetteEffect();

            //CHeck les cases en dessous

            GridManager.instance.Test.RemoveAt(0);

            yield return new WaitForSeconds(2f);

            StartCoroutine(CameraManager.instance.LerpZoomFunction(CameraManager.instance.UnZoomValue,1f));

            yield return new WaitForSeconds(.8f);

            if (GridManager.instance.Test.Count > 0 )
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
            GridManager.instance.Test.RemoveAt(0);
            if (GridManager.instance.Test.Count > 0)
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
        if (health <= 0) {
            print("nope Death");
            //DebugManager.instance.ReloadScene();
            StopAllCoroutines();
            CanvasManager.instance.PlayerLooseTheGame(CharacterData);
        }
        else
        {
            //GridManager.instance.ClearScene();
            CanvasManager.instance.PlayerWinTheGame(CharacterData);
        }
        UpdatePlayerContener();
    }

    #endregion

    void UpdatePlayerContener()
    {
        characterContener.Life = health;
        characterContener.Endurance = Endurance;
        characterContener.Inventory.Clear();

        foreach (var item in Inventory)
        {
            characterContener.Inventory.Add(item);
        }
    }    

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

    private void DrawVignette(int amountOfCard = 1)
    {
        print("okdqd");
        LevelManager.instance.SpawnObject(amountOfCard);
    }

    public void DrawVignette()
    {
        LevelManager.instance.SpawnObject(1);
    }

    private void DiscardVignette()
    {
        int selectedCard = Random.Range(0, HandOfVignette.Count);
        Vignette_Behaviours vignetteToDelete = HandOfVignette[selectedCard];
        HandOfVignette.Remove(vignetteToDelete);
        Destroy(vignetteToDelete.gameObject);

    }

    #endregion

    public void HealPlayer(int amountOfHeal)
    {
        health += amountOfHeal;
        if (health > maxHealth)
            health = maxHealth;
    }

    #region Interfaces
    public void GetDamage(int amountOfDamage)
    {
        health -= amountOfDamage;
        characterContener.PlayDamageMusique();
        if (IsDead())
            Death();
    }

    public void Death()
    {
        StopAllCoroutines();
        CreationManager.instance.GlobalCrew.Remove(CharacterData);
        CreationManager.instance.listOfCharacter.Remove(CharacterContener);
        Destroy(characterContener.gameObject);
        CanvasManager.instance.PlayerLooseTheGame(CharacterData);
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    #endregion

    public void ClearVignette()
    {
        foreach (var item in HandOfVignette)
        {
            Destroy(item.gameObject);
        }
        HandOfVignette.Clear();
    }

    #region Getter && Setter

    public bool HaveKey { get => haveKey; set => haveKey = value; }
    public List<Vignette_Behaviours> HandOfVignette { get => handOfVignette; set => handOfVignette = value; }
    public int MinCardToDraw { get => minCardToDraw; set => minCardToDraw = value; }
    public int AmountOfCardToDraw { get => amountOfCardToDraw; set => amountOfCardToDraw = value; }
    public Character_SO CharacterData { get => characterData; set => characterData = value; }
    public int Endurance { get => m_Endurance; set => m_Endurance = value; }
    public int MaxEndurance { get => m_MaxEndurance; set => m_MaxEndurance = value; }
    public int InventorySize { get => m_InventorySize; set => m_InventorySize = value; }
    public int MaxInventorySize { get => m_MaxInventorySize; set => m_MaxInventorySize = value; }
    public List<Object_SO> Inventory { get => m_Inventory; set => m_Inventory = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Health { get => health; set => health = value; }
    public Character_Button CharacterContener { get => characterContener; set => characterContener = value; }
    public int MentalHealth { get => m_MentalHealth; set => m_MentalHealth = value; }
    public int MaxMentalHealth { get => m_MaxMentalHealth; set => m_MaxMentalHealth = value; }
    public List<UsableObject> InventoryObj { get => m_InventoryObj; set => m_InventoryObj = value; }
    #endregion
}
