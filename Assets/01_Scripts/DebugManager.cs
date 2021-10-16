using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    public static DebugManager instance;
    [SerializeField] private bool useDebug;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : DebugManager");
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

        if (useDebug)
            DebugInput();
    }

    void DebugInput()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (Input.GetKeyDown(KeyCode.S))
            SpawnNewObject();*/
    }

    public void SpawnNewObject()
    {
        LevelManager.instance.SpawnObject();
    }

    public void ReloadScene()
    {
        
    }
}
