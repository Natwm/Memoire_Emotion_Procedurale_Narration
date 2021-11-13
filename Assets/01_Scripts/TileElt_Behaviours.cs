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
        print("okokokoko");
        eventAssocier.ApplyVignetteEffect();
        /*print(eventAssocier.name);

        player.Update_Happyness_Sadness(eventAssocier.MyEvent.CurrentHappy_Sad);

        player.Update_Angry_Fear(eventAssocier.MyEvent.CurrentAngry_Fear);

        player.HandModifier(eventAssocier.MyEvent.CurrentAmountOfVignetteToDraw);*/


    }



    #region Getter && Setter
    public Vector2 Tileposition { get => m_Tileposition; set => m_Tileposition = value; }
    public int Index { get => index; set => index = value; }
    public Vignette_Behaviours EventAssocier { get => eventAssocier; set => eventAssocier = value; }

    #endregion
}
