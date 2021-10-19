using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Codex", menuName = "New Scriptable Object/New Condition Branch")]
public class BranchingCondition : ScriptableObject
{
    [Header("Detail")]
    [SerializeField] private string branchName;
    [SerializeField] private int branchSize;

    [Header("Enum condition")]
    [SerializeField] private EmotionJauge emotionCondition;
    [SerializeField] private Role roleCondition;
    [SerializeField] private int jaugeValueCondition;

    [Header("Next Move")]
    [SerializeField] private int nextCheck;
    [SerializeField] private List<BranchingCondition> nextMove;

    public EmotionJauge EmotionCondition { get => emotionCondition; set => emotionCondition = value; }
    public Role RoleCondition { get => roleCondition; set => roleCondition = value; }
    public int JaugeValueCondition { get => jaugeValueCondition; set => jaugeValueCondition = value; }
    public int NextCheck { get => nextCheck; set => nextCheck = value; }
    public List<BranchingCondition> NextMove { get => nextMove; set => nextMove = value; }
    public string BranchName { get => branchName; set => branchName = value; }
    public int BranchSize { get => branchSize; set => branchSize = value; }
}
