using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileElt_Behaviours : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private Vector2 m_Tileposition;
    [SerializeField] Vignette_Behaviours eventAssocier;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AssociateEventToTile(Vignette_Behaviours BD_elt)
    {
        this.EventAssocier = BD_elt;
        if (!GridManager.instance.ListOfEvent.Contains(this))
            GridManager.instance.ListOfEvent.Add(this);
    }

    public void ApplyEffect(PlayerManager player)
    {
        string content = "";
        print(eventAssocier.name);



        if (eventAssocier.MyEvent.Happy_Sad > 0)
        {
            player.Update_Happyness_Sadness(eventAssocier.MyEvent.Happy_Sad);
        }
        else
        {
            player.Update_Happyness_Sadness(eventAssocier.MyEvent.Happy_Sad);
        }



        if (eventAssocier.MyEvent.Angry_Fear > 0)
        {
            player.Update_Angry_Fear(eventAssocier.MyEvent.Angry_Fear);
        }
        else
        {
            player.Update_Angry_Fear(eventAssocier.MyEvent.Angry_Fear);
        }



        if (eventAssocier.MyEvent.AmountOfVignetteToDraw > 0)
        {
            player.HandModifier(eventAssocier.MyEvent.AmountOfVignetteToDraw);
        }
        else
        {
            player.HandModifier(eventAssocier.MyEvent.AmountOfVignetteToDraw);
        }

    }



    #region Getter && Setter
    public Vector2 Tileposition { get => m_Tileposition; set => m_Tileposition = value; }
    public int Index { get => index; set => index = value; }
    public Vignette_Behaviours EventAssocier { get => eventAssocier; set => eventAssocier = value; }

    #endregion
}
