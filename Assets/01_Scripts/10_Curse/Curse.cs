using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Curse 
{
    [SerializeField] private string curseName;

    public string CurseName { get => curseName; set => curseName = value; }

    public abstract void ApplyCurse();
}

[System.Serializable]
public class Curse_ReduceLife : Curse
{
    public Curse_ReduceLife()
    {
        CurseName = "Reduce Life";
        Debug.Log(CurseName);
    }

    public override void ApplyCurse()
    {
        PlayerManager.instance.GetDamage(1);
    }
}

[System.Serializable]
public class Curse_ReduceMental : Curse
{
    public Curse_ReduceMental()
    {
        CurseName = "Reduce Mental";
        Debug.Log(CurseName);
    }

    public override void ApplyCurse()
    {
        PlayerManager.instance.ReduceMentalPlayer(1);
    }
}

[System.Serializable]
public class Curse_Loose_A_LevelObject : Curse
{
    public Curse_Loose_A_LevelObject()
    {
        CurseName = "Loose an object";
        Debug.Log(CurseName);
    }

    public override void ApplyCurse()
    {
        if (LevelManager.instance.PageInventory.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, PlayerManager.instance.Inventory.Count);
            LevelManager.instance.PageInventory.RemoveAt(index);
            CanvasManager.instance.RemoveObjInLevelInventory(index);
        }
    }
}
