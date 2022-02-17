using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    public List<UsableObject_SO> _RarePullOfObject;
    public List<UsableObject_SO> _HealPullOfObject;
    public List<UsableObject_SO> _BasisPullOfObject;
    public List<UsableObject_SO> _OccultsPullOfObject;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("");
        else
            instance = this;
    }
}
