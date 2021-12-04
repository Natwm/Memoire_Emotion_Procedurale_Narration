using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "New Scriptable Object/New Room")]
public class Room_So : ScriptableObject
{
    [Header("Param")]
    [SerializeField] private string m_RoomName;
    [SerializeField] private Vector2Int m_Roomsize;
    [SerializeField] private List<CaseContener_SO> m_containedObjects;

    [Space]
    [Header("Narration")]
    [TextArea(10,50)]
    [SerializeField] string m_IntroText;

    [TextArea(10, 50)]
    [SerializeField] string m_OutroText;

    #region Getter && Setter

    public string RoomName { get => m_RoomName; set => m_RoomName = value; }
    public Vector2Int Room_Size { get => m_Roomsize; set => m_Roomsize = value; }
    public List<CaseContener_SO> PossibleTiles { get => m_containedObjects; set => m_containedObjects = value; }
    public string Room_IntroText { get => m_IntroText; set => m_IntroText = value; }
    public string Room_OutroText { get => m_OutroText; set => m_OutroText = value; }

    #endregion

}
