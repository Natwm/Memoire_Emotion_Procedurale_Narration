using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Copie : MonoBehaviour
{

    public static GameManager_Copie instance;

    public Timer m_EndTimerCheck;
    public float m_EndTimerCheckTime = 20f;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GameManager");
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
      //  m_EndTimerCheck = new Timer(m_EndTimerCheckTime,CanvasManager.instance.EndTimer);
        //m_EndTimerCheck.ResetPlay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
