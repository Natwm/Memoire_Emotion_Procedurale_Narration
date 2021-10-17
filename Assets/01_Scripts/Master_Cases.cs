using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master_Cases : MonoBehaviour
{
    public Case_bd[] allCases;
    public int index = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    void DebugBd()
    {
        allCases[index].GetNext();
        if (index <= allCases.Length)
        {
            index++;
        }
        else
        {
            index = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
