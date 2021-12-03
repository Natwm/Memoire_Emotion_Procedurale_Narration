using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineRendererScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static lineRendererScript instance;

    LineRenderer myLine;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : CreationManager");
        else
            instance = this;

    }

    void Start()
    {
        myLine = GetComponent<LineRenderer>();
    }


    public void DrawLineRenderer()
    {
        myLine.positionCount = 0;
        int index = -1;
        foreach (var item in GridManager.instance.ListOfMovement)
        {
            if (item.EventAssocier.OnGrid)
            {
                index++;
                myLine.positionCount = GridManager.instance.ListOfMovement.Count;
                myLine.SetPosition(index, item.transform.position);
            }
            
        }
    }
}
