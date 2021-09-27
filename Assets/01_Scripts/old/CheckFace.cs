using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFace : MonoBehaviour
{

    /*Timer m_EndTimerCheck;
    public float m_EndTimerCheckTime = 2f;*/

    CharacterController m_Model;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckIstheSame()
    {
        print("CheckIstheSame");
        foreach (var item in FindObjectsOfType<CharacterController>())
        {

        }
    }

    void IsTheSameAsTheModel(CharacterController toCompare)
    {

    }

}
