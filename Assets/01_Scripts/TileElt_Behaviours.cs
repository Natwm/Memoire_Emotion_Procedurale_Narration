using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileElt_Behaviours : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private Vector2 m_Tileposition;
    [SerializeField] Bd_Elt_Behaviours eventAssocier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssociateEventToTile(Bd_Elt_Behaviours BD_elt)
    {
        print(BD_elt.name);
        this.EventAssocier = BD_elt;
        print(this.name + "  " + this.EventAssocier.name);
        print(this.EventAssocier.Value.HealthEffect);
        print(this.EventAssocier.Value.Health);

        GridManager.instance.ListOfEvent.Add(this);
    }

    public void ApplyEffect(PlayerManager player)
    {
        print(eventAssocier.name);
       /* if(eventAssocier != null)
        {*/
            switch (eventAssocier.Value.HealthEffect)
            {
                case Carte_SO.Status.BONUS:
                    player.GainHeath(eventAssocier.Value.Health);
                    break;

                case Carte_SO.Status.MALUS:
                    player.LooseHeath(eventAssocier.Value.Health);
                    break;

                default:
                    break;
           // }
        }
    }

    

    #region Getter && Setter
    public Vector2 Tileposition { get => m_Tileposition; set => m_Tileposition = value; }
    public int Index { get => index; set => index = value; }
    public Bd_Elt_Behaviours EventAssocier { get => eventAssocier; set => eventAssocier = value; }

    #endregion
}
