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
        this.EventAssocier = BD_elt;
        if(!GridManager.instance.ListOfEvent.Contains(this))
            GridManager.instance.ListOfEvent.Add(this);
    }

    public void ApplyEffect(PlayerManager player)
    {
        string content="";
        print(eventAssocier.name);
        if(GetComponent<IModifier>() != null)
        {
            GetComponent<IModifier>().CollectElement(eventAssocier.MyEvent);
        }

        if(eventAssocier.Value.HealthAffect == Carte_SO.Affect.USE)
        {

            if (eventAssocier.MyEvent.Health > 0)
            {
                player.GainHeath(eventAssocier.MyEvent.Health);
                content += " Player gain " + eventAssocier.MyEvent.Health + " life point \n";
            }
            else
            {
                player.LooseHeath(eventAssocier.Value.Health);
                content += " Player loose " + eventAssocier.MyEvent.Health + " life point \n";
            }
        }

        if (eventAssocier.Value.MovementAffect == Carte_SO.Affect.USE)
        {
            if (eventAssocier.Value.Movement > 0)
            {
                player.GainMovement(eventAssocier.Value.Movement);
                content += " Player gain " + eventAssocier.MyEvent.Movement + " stamina point";
            }
            else
            {
                player.LooseMovement(eventAssocier.Value.Movement);
                content += " Player loose " + eventAssocier.MyEvent.Movement + " stamina point";
            }
        }

        if(eventAssocier.Value.VignetteAffect == Carte_SO.Affect.USE)
        {
            if (eventAssocier.MyEvent.Vignette > 0)
            {
                player.HandModifier(eventAssocier.MyEvent.Vignette);
            }
            else
            {
                player.HandModifier(eventAssocier.MyEvent.Vignette);
            }
        }
        CanvasManager.instance.NewLogEntry(content);
    }

    

    #region Getter && Setter
    public Vector2 Tileposition { get => m_Tileposition; set => m_Tileposition = value; }
    public int Index { get => index; set => index = value; }
    public Bd_Elt_Behaviours EventAssocier { get => eventAssocier; set => eventAssocier = value; }

    #endregion
}
