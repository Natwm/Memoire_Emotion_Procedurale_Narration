using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager_Copie : MonoBehaviour
{
    public static CanvasManager_Copie instance;

    public TMP_Text text;
    public TMP_Text endText;

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
        string Value = "";
        if (!GameManager_Copie.instance.m_EndTimerCheck.IsFinished())
        {
            if((GameManager_Copie.instance.m_EndTimerCheckTime - GameManager_Copie.instance.m_EndTimerCheck.Time).ToString().Length > 3)
            {
                Value = (GameManager_Copie.instance.m_EndTimerCheckTime - GameManager_Copie.instance.m_EndTimerCheck.Time).ToString().Substring(0, 2);
            }else if ((GameManager_Copie.instance.m_EndTimerCheckTime - GameManager_Copie.instance.m_EndTimerCheck.Time).ToString().Length > 2)
                Value = (GameManager_Copie.instance.m_EndTimerCheckTime - GameManager_Copie.instance.m_EndTimerCheck.Time).ToString();
        }
            text.text = "TIMER : " + Value;
    }

    public void EndTimer()
    {
        endText.gameObject.SetActive(true);
    }

}
