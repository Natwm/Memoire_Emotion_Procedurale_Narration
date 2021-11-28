using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Curse 
{
    [SerializeField] private string curseName;

    public void ApplyCurse()
    {
        Debug.Log("Yes");
    }
}
