using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static LineRendererScript instance;
    Material LineMat;
    LineRenderer myLine;
    public float scrollSpeed;
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
        LineMat = myLine.material;
    }

    Vector2 offset;
    public void Update()
    {
        offset.x += Time.time*scrollSpeed;
        LineMat.SetTextureOffset("_MainTex",offset);
    }

    public void DrawLineRenderer()
    {
        myLine.positionCount = 0;
        int index = -1;
        
        myLine.positionCount = GridManager.instance.ListOfMovement.Count;

        foreach (var item in GridManager.instance.ListOfMovement)
        {
            if (item.EventAssocier.OnGrid)
            {
                index++;
                myLine.SetPosition(index, item.transform.position);
            }
        }

        if (GridManager.instance.ListOfMovement.Count > 0 && myLine.GetPosition(myLine.positionCount-1) == Vector3.zero)
            myLine.positionCount = myLine.positionCount - 1;
    }
}
