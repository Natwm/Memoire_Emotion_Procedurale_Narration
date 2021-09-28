using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_bd : MonoBehaviour
{

    public GameObject[] exitNodes;
    public bool twoExit;
    public bool used;
    
    // Start is called before the first frame update
    void Start()
    {
        exitNodes = new GameObject[transform.childCount];
        for (int i = 0; i < exitNodes.Length; i++)
        {
            exitNodes[i] = transform.GetChild(i).gameObject;
        }
        if (transform.childCount > 1)
        {
            twoExit = true;
        }
    }

    public GameObject GetNext()
    {
        GameObject newCase = null;
        for (int i = 0; i < exitNodes.Length; i++)
        {
            GameObject exitPoint = exitNodes[i];
            RaycastHit hit;
            if (Physics.Raycast(exitPoint.transform.position, exitPoint.transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.GetComponent<Case_bd>())
                {
                    Debug.DrawRay(exitPoint.transform.position, exitPoint.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log(hit.collider.gameObject.name);
                    newCase = hit.collider.gameObject;
                    return newCase;
                }
            }
        }
        return newCase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
