using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableObject : MonoBehaviour
{

    [SerializeField] private UsableObject_SO data;


    [SerializeField] private int amountOfUse;

    [Space]
    [Header("Curse")]
    [SerializeField] private bool isCurse;
    [SerializeField] private CurseBehaviours m_MyCurse;

    [SerializeField] private Character owner;
    [SerializeField] private GameObject buttonDisplay;

    public UsableObject_SO Data { get => data; set => data = value; }

    public bool IsCurse { get => isCurse; set => isCurse = value; }
    public CurseBehaviours MyCurse { get => m_MyCurse; set => m_MyCurse = value; }
    public int AmountOfUse { get => amountOfUse; set => amountOfUse = value; }
    public Character Owner { get => owner; set => owner = value; }
    public GameObject ButtonDisplay { get => buttonDisplay; set => buttonDisplay = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
