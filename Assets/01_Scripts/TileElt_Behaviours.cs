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

        if(eventAssocier.Value.Happy_SadAffect == Carte_SO.Affect.USE)
        {

            if (eventAssocier.MyEvent.Happy_Sad > 0)
            {
                player.Update_Happyness_Sadness(eventAssocier.MyEvent.Happy_Sad);
            }
            else
            {
                player.Update_Happyness_Sadness(eventAssocier.MyEvent.Happy_Sad);
            }
        }

        if (eventAssocier.Value.Angry_FearAffect == Carte_SO.Affect.USE)
        {
            if (eventAssocier.Value.Angry_Fear > 0)
            {
                player.Update_Angry_Fear(eventAssocier.MyEvent.Angry_Fear);
            }
            else
            {
                player.Update_Angry_Fear(eventAssocier.MyEvent.Angry_Fear);
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
    }

    

    #region Getter && Setter
    public Vector2 Tileposition { get => m_Tileposition; set => m_Tileposition = value; }
    public int Index { get => index; set => index = value; }
    public Bd_Elt_Behaviours EventAssocier { get => eventAssocier; set => eventAssocier = value; }

    #endregion
}
