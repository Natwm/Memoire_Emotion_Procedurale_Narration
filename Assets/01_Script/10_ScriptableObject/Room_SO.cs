using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Palette", menuName = "New Scriptable Object/New Palette")]
public class Palette_SO : ScriptableObject
{
    [SerializeField] private Color[] m_colorPalette;
    public Color[] colorPalette { get => m_colorPalette; set => m_colorPalette = value; }
}

[System.Serializable]
public class RoomExit
{
    [SerializeField] private Room_SO m_ExitFromRoom;

    [TextArea(5, 5)]
    [SerializeField] private string m_ExitText;

    public Room_SO Exit_To_Room { get => m_ExitFromRoom; set => m_ExitFromRoom = value; }
    public string Exit_Text { get => m_ExitText; set => m_ExitText = value; }

}

[CreateAssetMenu(fileName = "New Room", menuName = "New Scriptable Object/New Room")]
public class Room_SO : ScriptableObject
{
    [Header("Param")]
    [SerializeField] private string m_RoomName;
    [SerializeField] private Vector2Int m_Roomsize;
    [SerializeField] private List<CaseContener_SO> m_possibleObjects;
    [SerializeField] private List<CaseContener_SO> m_garanteedObjects;
    [SerializeField] private int m_Object_Distribution;
    [SerializeField] private CustomEffect m_Effect_Of_Room;
    [SerializeField] private Palette_SO m_ColorPalette;
    [SerializeField] private Sprite m_RoomPicture;

    public enum CustomEffect
    {
        NONE,
        DARK,
        NEGO,
        HUB

    }

    [Space]
    [Header("Exit")]
    [SerializeField] private RoomExit[] m_PossibleExits;

    [Space]
    [Header("Narration")]
    [TextArea(10, 50)]
    [SerializeField] string m_IntroText;

    [TextArea(10, 50)]
    [SerializeField] string m_OutroText;

    #region Getter && Setter
    public Sprite RoomPicture { get => m_RoomPicture; set => m_RoomPicture = value; }
    public Palette_SO ColorPalette { get => m_ColorPalette; set => m_ColorPalette = value; }
    public string RoomName { get => m_RoomName; set => m_RoomName = value; }
    public Vector2Int Room_Size { get => m_Roomsize; set => m_Roomsize = value; }
    public List<CaseContener_SO> PossibleTiles { get => m_possibleObjects; set => m_possibleObjects = value; }
    public List<CaseContener_SO> GaranteedTiles { get => m_garanteedObjects; set => m_garanteedObjects = value; }
    public string Room_IntroText { get => m_IntroText; set => m_IntroText = value; }
    public string Room_OutroText { get => m_OutroText; set => m_OutroText = value; }
    public int ObjectDistribution { get => m_Object_Distribution; set => m_Object_Distribution = value; }
    public RoomExit[] PossibleExits { get => m_PossibleExits; set => m_PossibleExits = value; }
    public CustomEffect Effect_Of_Room { get => m_Effect_Of_Room; set => m_Effect_Of_Room = value; }

    #endregion

    public void SetColorPalette()
    {
        //Vignette_Renderer.instance.currentColorPalette = ColorPalette.colorPalette;
    }

    public void ApplyEffect()
    {
        switch (Effect_Of_Room)
        {
            case CustomEffect.NONE:
                /*SoundManager.instance.LoopEffect.setParameterByName("Resolution", 1);
                SoundManager.instance.LoopEffect.setParameterByName("Negotiation", 0);*/
                break;

            case CustomEffect.DARK:
                {
                    /*SoundManager.instance.LoopEffect.setParameterByName("Resolution", 1);
                    SoundManager.instance.LoopEffect.setParameterByName("Negotiation", 0);*/
                    foreach (GameObject item in GridManager.instance.ListOfTile)
                    {
                        /*if (item.GetComponent<Case_Behaviours>().CaseEffects != null)
                        {
                            item.transform.GetChild(0).gameObject.SetActive(false);
                        }*/

                    }

                    break;
                }
            case CustomEffect.NEGO:
                {
                    /*SoundManager.instance.LoopEffect.setParameterByName("Resolution", 0);
                    SoundManager.instance.LoopEffect.setParameterByName("Negotiation", 1);*/

                    NegociationManager.instance.ResetNegociationTime();
                    CanvasManager.instance.SetUpCreationPanel();

                    if(LevelManager.instance.PageInventory.Count > 0)
                    {
                        foreach (var item in LevelManager.instance.PageInventory)
                        {
                            Destroy(item.gameObject);
                        }
                    }

                    LevelManager.instance.PageInventory = new List<UsableObject>();

                    GameManager.instance.OrderCharacter.Clear();
                    GameManager.instance.WaitingCharacter.Clear();
                    break;
                }
            case CustomEffect.HUB:
                {

                    break;
                }
        }
    }

}
