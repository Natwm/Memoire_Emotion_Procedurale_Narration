using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CurseBehaviours : MonoBehaviour
{
    public string curseDisplayName;
    public abstract void ApplyCurse();

    public abstract string GetCurseName();
}

[System.Serializable]
public class Curse_ReduceLife : CurseBehaviours
{
    public override void ApplyCurse()
    {
    }

    public override string GetCurseName()
    {
        throw new System.NotImplementedException();
    }
}

[System.Serializable]
public class Curse_Loose_A_LevelObject : CurseBehaviours
{
    public override void ApplyCurse()
    {
    }
    public override string GetCurseName()
    {
        throw new System.NotImplementedException();
    }
}

[System.Serializable]
public class Curse_ReduceMental : CurseBehaviours
{
    public override void ApplyCurse()
    {
    }
    public override string GetCurseName()
    {
        throw new System.NotImplementedException();
    }
}