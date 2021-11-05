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
    public List<GameObject> ButtonList = new List<GameObject>();

    public Vector2 shape;

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
    }

    private void CreateButton(Character tempCharacter)
    {
        GameObject tempButton = Instantiate(baseButton, ListHolder.transform);
        
        tempButton.GetComponent<characterCreation>().assignedElement = tempCharacter;
        tempCharacter.CreateFaceUI(tempButton);
        ButtonList.Add(tempButton);

        tempButton.GetComponent<characterCreation>().assignedElement = tempCharacter;

    }

    public List<Character> CreateList(int _charaAmount=1)
    {
        List<Character> tempList = new List<Character>();
        
        for (int i = 0; i < _charaAmount; i++)
        {
            Character tempCharacter = CastingManager.instance.getRandomUniqueCharacter(tempList.ToArray());
            
            tempList.Add(tempCharacter);

            CreateButton(tempCharacter);

            //CharacterList.Remove(tempCharacter);
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
        if (Input.GetKeyUp(KeyCode.A))
        {
            CreateVignette();
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

    public void CreateVignette()
    {
        List<characterCreation> listOfCharacterWanted = new List<characterCreation>();
        List<characterCreation> listOfCharacterDontWanted = new List<characterCreation>();
        List<characterCreation> listOfCharacterFreeze = new List<characterCreation>();
        List<characterCreation> listOfCharacterNone = new List<characterCreation>();
        List<characterCreation> listOfSpawnable = new List<characterCreation>();


        for (int i = 0; i < ListHolder.transform.childCount; i++)
        {
            characterCreation character = ListHolder.transform.GetChild(i).GetComponent<characterCreation>();
            //print("<Color=green>"+character.assignedElement.characterName + "  " + character.Status+"</Color>");
            switch (character.Status)
            {
                case characterCreation.CharacterStatus.FREEZE:
                    listOfCharacterFreeze.Add(character);
                    break;
                case characterCreation.CharacterStatus.WANT:
                    listOfCharacterWanted.Add(character);
                    break;
                case characterCreation.CharacterStatus.DONT_WANT:
                    listOfCharacterDontWanted.Add(character);
                    break;
                case characterCreation.CharacterStatus.NONE:
                    listOfCharacterNone.Add(character);
                    break;
                default:
                    break;
            }
        }

        listOfSpawnable.AddRange(listOfCharacterWanted);
        listOfSpawnable.AddRange(listOfCharacterNone);

        List<Character> test = new List<Character>();

        foreach (var item in listOfSpawnable)
        {
            test.Add(item.assignedElement);
        }

        GameObject card = Instantiate(GetVignetteShape(shape));

        //print(card.name);
//        print(Bd_Component.bd_instance.name);
        Bd_Component.bd_instance.SetVignetteToObjectCreate(card,test.ToArray());
        
        //Vignette tempVignette = new Vignette(shape.ToString(), GetVignette(shape), null, null, GetComp(shape));

        //PrintList(listOfCharacterFreeze, listOfCharacterDontWanted, listOfCharacterWanted, listOfCharacterNone);
    }

    public GameObject GetVignette(Vector2 caseShape)
    {
        string chemin = "Generation/Vignette/" + "case_" + caseShape.ToString();
        return Resources.Load<GameObject>(chemin);
    }

    public GameObject GetShape(Vector2 caseShape)
    {
        string chemin = "Generation/Vignette/" + "case_" + caseShape.ToString();
        return Resources.Load<GameObject>(chemin);
    }

    public GameObject GetVignetteShape(Vector2 caseShape)
    {
        string chemin = "Generation/Tile/" + "case_" + caseShape.x+"x"+caseShape.y;
        return Resources.Load<GameObject>(chemin);
    }

    public GameObject GetComp(Vector2 caseShape)
    {
        string chemin = "Generation/Composition/" + caseShape.ToString();
        return Resources.Load<GameObject>(chemin);
    }

    private void PrintList(List<characterCreation> listOfCharacterFreeze, List<characterCreation> listOfCharacterDontWanted, List<characterCreation> listOfCharacterWanted, List<characterCreation> listOfCharacterNone)
    {
        print(" ------------------------------------------------");
        print("ImageShape is : " + shape);

        print("My list est : listOfCharacterFreeze :");
        foreach (var item in listOfCharacterFreeze)
        {
            print(item.assignedElement.characterName);
        }

        print("My list est : listOfCharacterDontWanted :");
        foreach (var item in listOfCharacterDontWanted)
        {
            print(item.assignedElement.characterName);
        }

        print("My list est : listOfCharacterWanted :");
        foreach (var item in listOfCharacterWanted)
        {
            print(item.assignedElement.characterName);
        }

        print("My list est : listOfCharacterNone :");
        foreach (var item in listOfCharacterNone)
        {
            print(item.assignedElement.characterName);
        }
        print(" ------------------------------------------------");
    }

}
