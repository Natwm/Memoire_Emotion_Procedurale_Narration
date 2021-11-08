using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterCreation : MonoBehaviour
{

    private Button myButton;
    public Character assignedElement;
    public enum CharacterStatus
    {
        FREEZE,
        WANT,
        DONT_WANT,
        NONE
    }

    [SerializeField] private CharacterStatus m_Status;

    public CharacterStatus Status { get => m_Status; set => m_Status = value; }

    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(delegate
        {
            print(assignedElement.characterName + " est : " + Status);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* public void AffectByPen()
    {
        
        switch (CreationManager.instance.Pen)
        {
            case CreationManager.m_PenStatus.NONE:
                myButton.image.color = Color.white;
                Status = CharacterStatus.NONE;
                break;

            case CreationManager.m_PenStatus.FREEZE:
                if(Status != CharacterStatus.FREEZE)
                {
                    myButton.image.color = Color.cyan;
                    Status = CharacterStatus.FREEZE;
                }
                else
                {
                    myButton.image.color = Color.white;
                    Status = CharacterStatus.NONE;
                }
                break;

            case CreationManager.m_PenStatus.WANT:
                if (Status != CharacterStatus.WANT)
                {
                    myButton.image.color = Color.green;
                    Status = CharacterStatus.WANT;
                }
                else
                {
                    myButton.image.color = Color.white;
                    Status = CharacterStatus.NONE;
                }
                break;

            case CreationManager.m_PenStatus.DONT_WANT:
                if (Status != CharacterStatus.DONT_WANT)d
                }
                else
                {
                    myButton.image.color = Color.white;
                    Status = CharacterStatus.NONE;
                }
                break;

            default:
                break;
        }
    }*/
}
