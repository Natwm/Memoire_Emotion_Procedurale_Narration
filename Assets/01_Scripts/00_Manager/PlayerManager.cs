using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int health;



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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToAnotherStep()
    {
        StartCoroutine(MoveToLocation());
    }

    private IEnumerator MoveToLocation()
    {
        Vector3 newPosition = GridManager.instance.ListOfEvent[0].transform.position;
        GameObject tile = GridManager.instance.ListOfTile[0];

        newPosition.Set(newPosition.x, newPosition.y, -2f);
        this.transform.DOMove(newPosition, 1f);
        GridManager.instance.ListOfEvent.RemoveAt(0);
        yield return new WaitForSeconds(.8f);

        TileElt_Behaviours cardEvent;
        if(tile.TryGetComponent<TileElt_Behaviours>(out cardEvent))
            cardEvent.ApplyEffect(this);

    }

    public void GainHeath( int point)
    {
        health += point;
        print("Heal by "+ point +" point");
    }

    public void LooseHeath (int point)
    {
        health -= point;
        print("damage deal by " + point + " point");
        if (health <= 0)
            print("Death"); 
    }

    #region Getter && Setter
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Health { get => health; set => health = value; }
    #endregion
}
