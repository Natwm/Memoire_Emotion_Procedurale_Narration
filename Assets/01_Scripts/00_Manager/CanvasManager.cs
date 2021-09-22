using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : CanvasManager");
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
