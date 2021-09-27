using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_Copie : MonoBehaviour
{
    public static SoundManager_Copie instance;
    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : SoundManager");
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
