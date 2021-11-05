using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationManager : MonoBehaviour
{
    public enum m_PenStatus
    {
        NONE,
        FREEZE,
        WANT,
        DONT_WANT,
        DRAW
    }


    public static CreationManager instance;
    public List<Character> CharacterList;
    public List<Character> PageCharacterList;
    public GameObject ListHolder;
    public GameObject baseButton;
    public List<GameObject> ButtonList;

    [SerializeField] private m_PenStatus m_Pen;

    public m_PenStatus Pen { get => m_Pen; set => m_Pen = value; }

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : CreationManager");
        else
            instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        ButtonList = new List<GameObject>();
    }

    public List<Character> CreateList(int _charaAmount=1)
    {
        List<Character> tempList = new List<Character>();
        
        for (int i = 0; i < _charaAmount; i++)
        {
            Character tempCharacter = CastingManager.instance.getRandomUniqueCharacter(tempList.ToArray());
            tempList.Add(tempCharacter);
            GameObject tempButton = Instantiate(baseButton, ListHolder.transform);
            tempButton.GetComponent<characterCreation>().assignedElement = tempCharacter;
            ButtonList.Add(tempButton);

        }
        return tempList == null?null:tempList;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            PageCharacterList = CreateList(4);
        }
    }

    

    public void ChangePen(string usedPen)
    {
        switch (usedPen)
        {
            case "freeze":
                if (Pen != m_PenStatus.FREEZE)
                    Pen = m_PenStatus.FREEZE;
                else
                    Pen = m_PenStatus.NONE;
                break;

            case "want":
                if (Pen != m_PenStatus.WANT)
                    Pen = m_PenStatus.WANT;
                else
                    Pen = m_PenStatus.NONE;
                break;

            case "dont":
                if (Pen != m_PenStatus.DONT_WANT)
                    Pen = m_PenStatus.DONT_WANT;
                else
                    Pen = m_PenStatus.NONE;
                break;

            case "none":
                    Pen = m_PenStatus.NONE;
                break;

            case "draw":
                if (Pen != m_PenStatus.DRAW)
                    Pen = m_PenStatus.DRAW;
                else
                    Pen = m_PenStatus.NONE;
                break;

            default:
                break;
        }
    }

}
