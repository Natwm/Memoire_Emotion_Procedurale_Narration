using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CastingManager : MonoBehaviour
{
    public static CastingManager instance;
    
    string[] Names = { "René","Josiane","Michel","Albert","Lucie","Sylvie","Maurice","Mauricette","Nathan","Sonia","Simon","Adrien","Julien","Morgane","Killian","Thomas","Pierre","José","Nicolas","Brigitte","Vivienne","Jean"};
    public Color[] CharacterColors;
    public GameObject iconRenderer;
    [SerializeReference]
    Character[] allcharacters;
    GameObject[] allCharacterIcons;

    [Header("Face Icon Sprites")]
    public Sprite baseFaceIcon;
    public Sprite NeutralFace;

    [Header("Facial Features")]
    public Sprite[] TopSprites;
    
    
    public Character[] AllCharacters { get => allcharacters; set => allcharacters = value; }
    public Color[] CastColors;
    public int ColorIndex = 0;
// Start is called before the first frame update

private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        CastColors = new Color[6];
        CastColors = GetCharacterColorDistribution(6);
        CreateCharacter(6);
        
        
    }

    void Start()
    {


        //Bd_Component.bd_instance.CreateNewRandomVignette(5);

        CanvasManager.instance.InitialiseCharactersPanel();
    }

    public void SetCharactersToHand()
    {
        foreach (Vignette item in Bd_Component.bd_instance.VignetteSequence)
        {
            SetCharacterToVignette(item);
        }
    }

    public Character getRandomCharacter()
    {
        int randIndex = Random.Range(0, AllCharacters.Length);
        return AllCharacters[randIndex];
    }

    public Character[] getCharacterDistribution(int characterAmount)
    {
        Character[] tempCharacterDistribution = new Character[characterAmount];
        for (int i = 0; i < characterAmount; i++)
        {
            tempCharacterDistribution[i] = getRandomUniqueCharacter(tempCharacterDistribution);
        }
        return tempCharacterDistribution;
    }

    public Character getRandomUniqueCharacter(Character[] currentCast)
    {
        Character newChara = getRandomCharacter();
        foreach (Character item in currentCast)
        {
            if (item == newChara)
            {
                return getRandomUniqueCharacter(currentCast);
            }    
        }
        return newChara;
    }

    public Color[] GetCharacterColorDistribution(int characterAmount)
    {
        Color[] colorDistribution = new Color[characterAmount];
        for (int i = 0; i < characterAmount; i++)
        {
            colorDistribution[i] = getRandomUniqueCharacterColor(colorDistribution);
        }
        return colorDistribution;
    }

    public Color getRandomUniqueCharacterColor(Color[] currentColors)
    {
        Color newColor = GetRandomCharacterColor();
        foreach (Color item in currentColors)
        {
            if (item == newColor)
            {
                return getRandomUniqueCharacterColor(currentColors);
            }
        }
        return newColor;
    }

    public Color GetRandomCharacterColor()
    {
        int randIndex = Random.Range(0, CharacterColors.Length);
        return CharacterColors[randIndex];
    }

    public Color SetCharacterColor()
    {

        ColorIndex++;
        return CastColors[ColorIndex - 1];
    }

    /* Get a Random Character in the Cast.
     * Foreach Character in Array 
     *      > Is The Character Already in Cast
     *      > True
     *      > Get Another Character > Do Comparaison Again
     *      > 
     * Is The character already in
     * Store it in array
     */

    public void SetCharacterToVignette(Vignette _vignetteToSet)
    {

        _vignetteToSet.inVignetteCharacter = getCharacterDistribution(_vignetteToSet.ObjectsNumber);
        for (int i = 0; i < _vignetteToSet.ObjectsNumber; i++)
        {
            _vignetteToSet.inVignetteCharacter[i].SetIconToVignette(_vignetteToSet);
        }
        /*
        foreach (Character item in _vignetteToSet.inVignetteCharacter)
        {
            item.SetIconToVignette(_vignetteToSet);
        }*/
        
    }

    public void CreateCharacter(int CharacterNumber)
    {
        AllCharacters = new Character[CharacterNumber];
        allCharacterIcons = new GameObject[CharacterNumber];
        for (int i = 0; i < CharacterNumber; i++)
        {
            AllCharacters[i] = new Character(Role.None, EmotionJauge.Jauge_PeurColere);
           
        }
    }



    public string GetName()
    {
        return Names[Random.Range(0, Names.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum EmotionJauge
{
    Jauge_PeurColere, Jauge_TristesseJoie
}

public enum Role
{
    Antagoniste,Allie,Mentor,Traitre,None
}


[System.Serializable]
public class Character
{
    [Header("Character Data")]
    public Color characterColor;
    public string characterName;
    public EmotionJauge currentJauge;
    public int jaugeNumber = 0;

    public Role currentRole = Role.None;
    [Space]
    [Header("Character Runtime Visuals")]
    public Sprite faceFeature;
    public SpriteRenderer EmotionSprite;
    SpriteRenderer characterRenderer;
    Sprite Emotion;
    //In Vignette Character

    [Space]
    [Header("Character References")]
    public List<GameObject> characterFaceIcon;
    public TMP_Text nameDisplay;
    //public GameObject characterHolder;
    // CharacterData







    // CharacterEmotion

    
    
    
    

    public GameObject CreateFace(GameObject compPoint)
    {
        characterFaceIcon = new List<GameObject>();
        //Base
        GameObject newFaceIcon = GameObject.Instantiate(CastingManager.instance.iconRenderer);
        newFaceIcon.GetComponent<SpriteRenderer>().sprite = CastingManager.instance.baseFaceIcon;
        newFaceIcon.GetComponent<SpriteRenderer>().sortingOrder = 1;

        newFaceIcon.transform.parent = compPoint.transform;
        newFaceIcon.transform.localPosition = Vector3.zero;
        nameDisplay = newFaceIcon.transform.GetChild(0).GetComponent<TMP_Text>();
        nameDisplay.text = characterName;


        //Facial Features
        GameObject Feature = newFaceIcon.transform.GetChild(1).gameObject;
        Feature.GetComponent<SpriteRenderer>().sprite = faceFeature;
        Feature.transform.parent = newFaceIcon.transform;
        Feature.transform.localPosition = Vector3.zero;
        Feature.GetComponent<SpriteRenderer>().sortingOrder = 2;
        

        //EmotionObject
        return newFaceIcon;
    }

    public Character(Role character_Role,EmotionJauge currentEmotion)
    {
        int randomTop = Random.Range(0, CastingManager.instance.TopSprites.Length);
        int pileOuFace = Random.Range(0, 2);
        
        faceFeature = CastingManager.instance.TopSprites[randomTop];
        
        characterColor = CastingManager.instance.SetCharacterColor();

        characterName = CastingManager.instance.GetName();
        currentRole = character_Role;
        currentJauge = currentEmotion;

        
    }

   
    public void SetObjectColor(bool isIcon,GameObject objToSet)
    {
        if (isIcon)
        {
            objToSet.GetComponent<SpriteRenderer>().color = characterColor;
            //objToSet.transform.GetChild(0).GetComponent<TMP_Text>().color = characterColor;
            objToSet.transform.GetChild(2).GetComponent<SpriteRenderer>().color = characterColor;
        }
    }

    public void SetIconToVignette(Vignette _vignette)
    {
        GameObject newPoint = _vignette.GetRandomCompositionPoint();
        GameObject tempFace = CreateFace(newPoint);
        SetObjectColor(true, tempFace);
        newPoint.SetActive(false);
        tempFace.transform.parent = _vignette.Cadre_Object.transform;
        characterFaceIcon.Add(tempFace);

    }
}

public class Relation
{
    Character[] involvedCharacters;
    EmotionJauge assignedJauge;
    int Amount;

    bool CheckForCharacter(Vignette _vignetteToCheck)
    {
        int charaCounter = 0;
        foreach (Character charaInVignette in _vignetteToCheck.inVignetteCharacter)
        {
            foreach (Character charaInRelation in involvedCharacters)
            {
                if (charaInRelation == charaInVignette)
                {
                    charaCounter++;
                }
            }
        }
        if (charaCounter ==2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    // Store Characters
    // Jauge To Modify
    // Amount Of Relation

}
