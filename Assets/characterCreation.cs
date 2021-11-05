using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterCreation : MonoBehaviour
{

    private Button myButton;
    public enum CharacterStatus
    {
        FREEZE,
        WANT,
        DONT_WANT,
        NONE
    }

    [SerializeField] private CharacterStatus m_Status;

    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AffectByPen()
    {
        
        switch (CreationManager.instance.Pen)
        {
            case CreationManager.m_PenStatus.NONE:
                myButton.image.color = Color.white;
                m_Status = CharacterStatus.NONE;
                break;

            case CreationManager.m_PenStatus.FREEZE:
                if(m_Status != CharacterStatus.FREEZE)
                {
                    myButton.image.color = Color.cyan;
                    m_Status = CharacterStatus.FREEZE;
                }
                else
                {
                    myButton.image.color = Color.white;
                    m_Status = CharacterStatus.NONE;
                }
                break;

            case CreationManager.m_PenStatus.WANT:
                if (m_Status != CharacterStatus.WANT)
                {
                    myButton.image.color = Color.green;
                    m_Status = CharacterStatus.WANT;
                }
                else
                {
                    myButton.image.color = Color.white;
                    m_Status = CharacterStatus.NONE;
                }
                break;

            case CreationManager.m_PenStatus.DONT_WANT:
                if (m_Status != CharacterStatus.DONT_WANT)
                {
                    myButton.image.color = Color.red;
                    m_Status = CharacterStatus.DONT_WANT;
                }
                else
                {
                    myButton.image.color = Color.white;
                    m_Status = CharacterStatus.NONE;
                }
                break;

            default:
                break;
        }
    }
}
