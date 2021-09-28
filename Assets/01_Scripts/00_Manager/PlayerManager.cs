using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header ("Health")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int health;

    [Space]
    [Header("Stamina")]
    [SerializeField] private int maxStamina = 5;
    [SerializeField] private int stamina;

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

        CanvasManager.instance.UpdateInformationText(health, stamina);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameOver()
    {
        print("GameOver");
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
        LooseMovement(1);
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

    void EndMovement()
    {
        if (health < 0) {
            print("nope Death");
            DebugManager.instance.ReloadScene();
        }
        else
        {
            GridManager.instance.ClearScene();
        }
    }

    #region LifeEvent
    public void GainHeath( int point)
    {
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

    #region Getter && Setter
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Health { get => health; set => health = value; }
    #endregion
}
