using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CastingManager : MonoBehaviour
{
    public static CastingManager instance;
    
    string[] Names = { "René","Josiane","Michel","Albert","Lucie","Sylvie","Maurice","Mauricette","Nathan","Sonia","Simon","Adrien","Julien","Morgane","Killian","Thomas","Pierre","José","Nicolas","Brigitte","Vivienne","Jean"};
    public Color[] CharacterColors;
    public GameObject iconRenderer;
    public GameObject iconRendererUI;
    public int CharacterJaugesMaximumAmount=1;
    [SerializeReference]
    Character[] allcharacters;
    Character PlayerCharacter;
    GameObject[] allCharacterIcons;

    [Header("Face Icon Sprites")]
    public Sprite baseFaceIcon;
    public Sprite NeutralFace;
    public Sprite[] EmotionFaces;

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
       // CastColors = GetCharacterColorDistribution(6);
        CreateCharacter(6);
        PlayerCharacter = new Character(Role.None, EmotionJauge.Jauge_PeurColere);
        
        GetJaugeDistribution(6);
        
    }

    void Start()
    {
        /*CreationManager.instance.CharacterList = new List<Character>(allcharacters);
        CreationManager.instance.CharacterList.Add(PlayerCharacter);*/

        //Bd_Component.bd_instance.CreateNewRandomVignette(5);

        //CanvasManager.instance.InitialiseCharactersPanel();
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

    public Character getRandomCharacter(Character[] checkList)
    {
        int randIndex = Random.Range(0, checkList.Length);
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

    public Character[] getCharacterDistribution(int characterAmount, Character[] allCast)
    {
        print("popo"+characterAmount);
        Character[] tempCharacterDistribution = new Character[characterAmount];

        for (int i = 0; i < characterAmount; i++)
        {
            tempCharacterDistribution[i] = getRandomUniqueCharacter(tempCharacterDistribution,allCast);
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

    public Character getRandomUniqueCharacter(Character[] currentCast, Character[] allCast)
    {
        Character newChara = getRandomCharacter();
        foreach (Character item in currentCast)
        {
            if (item == newChara )
            {
                return getRandomUniqueCharacter(currentCast, allCast);
            }
        }
        if(CreationManager.instance.PageCharacterList.Contains(newChara))
            return newChara;
        else
            return getRandomUniqueCharacter(currentCast, allCast);
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
        Debug.Log(CharacterColors.Length);
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

    public void SetCharacterToVignette(Vignette _vignetteToSet, Character[] allCast)
    {
        print("popopo " + _vignetteToSet.ObjectsNumber);
        
        _vignetteToSet.inVignetteCharacter = getCharacterDistribution(_vignetteToSet.ObjectsNumber, allCast);
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

    public void GetJaugeDistribution(int _charaAmount)
    {
        int half = _charaAmount / 2;
        //Modulo == 0 > Nombre Pair == Half
        for (int i = 0; i < half; i++)
        {
            AllCharacters[i].currentJauge = EmotionJauge.Jauge_PeurColere;
        }
        for (int i = half; i < AllCharacters.Length; i++)
        {
            AllCharacters[i].currentJauge = EmotionJauge.Jauge_TristesseJoie;
        }
        if (_charaAmount % 2 != 0)
        {
            int Rand = Random.Range(0, 2);
            if (Rand == 0)
            {
                AllCharacters[2 * half + 1].currentJauge = EmotionJauge.Jauge_PeurColere;

            }
            else
            {
                AllCharacters[2 * half + 1].currentJauge = EmotionJauge.Jauge_TristesseJoie;

            }
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


//On a notre liste de personnages.
//Il faut créer une liste de personnages aléatoire, et 

[System.Serializable]
public class Character
{
    [Header("Character Data")]
    public Color characterColor;
    public string characterName;
    public EmotionJauge currentJauge;
    public int jaugeNumber = 0;
    int JaugeCap;

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
    public Relation currentRelation;

    //BaseCharacter
    public Character(Role character_Role, EmotionJauge currentEmotion)
    {
        int randomTop = Random.Range(0, CastingManager.instance.TopSprites.Length);
        int pileOuFace = Random.Range(0, 2);
        characterFaceIcon = new List<GameObject>();
        faceFeature = CastingManager.instance.TopSprites[randomTop];

        characterColor = Color.black;

        characterName = CastingManager.instance.GetName();
        currentRole = character_Role;
        currentJauge = currentEmotion;
        JaugeCap = CastingManager.instance.CharacterJaugesMaximumAmount;
    }

    // CharacterEmotion

    public GameObject CreateFace(GameObject compPoint)
    {
        
        //Base
        GameObject newFaceIcon = GameObject.Instantiate(CastingManager.instance.iconRenderer);
        characterFaceIcon.Add(newFaceIcon);
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

        Emotion = GetCharacterEmotion(0);


        //EmotionObject
        

        return newFaceIcon;
    }

    public GameObject CreateFaceUI(GameObject compPoint)
    {
        //Base
        GameObject newFaceIcon = GameObject.Instantiate(CastingManager.instance.iconRendererUI);
        characterFaceIcon.Add(newFaceIcon);
        newFaceIcon.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = CastingManager.instance.baseFaceIcon;
        //newFaceIcon.GetComponent<Image>().sortingOrder = 1;

        newFaceIcon.transform.parent = compPoint.transform;
        //newFaceIcon.transform.localPosition = Vector3.zero;
        nameDisplay = newFaceIcon.transform.GetChild(2).GetComponent<TMP_Text>();
        nameDisplay.text = characterName;


        //Facial Features
        GameObject Feature = newFaceIcon.transform.GetChild(1).gameObject;
        Feature.transform.GetComponent<Image>().sprite = faceFeature;
        Feature.transform.parent = newFaceIcon.transform;
        Feature.transform.localPosition = Vector3.zero;
        //Feature.GetComponent<Image>().sortingOrder = 2;

        Emotion = GetCharacterEmotion(0);


        //EmotionObject


        return newFaceIcon;
    }


    public void UpdateAllCharacterFaceIcon()
    {
        foreach (GameObject item in characterFaceIcon)
        {
            item.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = GetCharacterEmotion(jaugeNumber);
        }
    }

    public void UpdateCharacterFaceIcon(GameObject _icon,int _amount)
    {
        _icon.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = GetCharacterEmotion(_amount);
    }

    

    public void IncreaseCharacterJauge()
    {
        if (jaugeNumber < JaugeCap)
        {
            jaugeNumber++;
        }
    }
    
    public void DecreaseCharacterJauge()
    {
        if (jaugeNumber > -JaugeCap)
        {
            jaugeNumber--;
        }
    }

    

    public Sprite GetCharacterEmotion(int _jaugeAmount)
    {

        if (_jaugeAmount == 0)
        {
            return CastingManager.instance.NeutralFace;
        }
        switch (currentJauge)
        {
            case EmotionJauge.Jauge_PeurColere:
                {
                if (_jaugeAmount>0)
                {


                        return CastingManager.instance.EmotionFaces[1];

                }
                else
                {

                        return CastingManager.instance.EmotionFaces[0];
                }
                }
            case EmotionJauge.Jauge_TristesseJoie:
                {
                if (_jaugeAmount > 0)
                {

                        return CastingManager.instance.EmotionFaces[3];
                }
                else
                {

                        return CastingManager.instance.EmotionFaces[2];
                }
                
                }
        }
        return CastingManager.instance.NeutralFace;
        
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
        Debug.Log("ahah");
        GameObject newPoint = _vignette.GetRandomCompositionPoint();
        GameObject tempFace = CreateFace(newPoint);
        SetObjectColor(true, tempFace);
        newPoint.SetActive(false);
        tempFace.transform.parent = _vignette.Cadre_Object.transform;
        _vignette.inVignette_CharacterIcons.Add(tempFace);

    }
}

public class Relation
{
    Character[] involvedCharacters;
    EmotionJauge assignedJauge;
    int Amount;
    bool relationWithAvatar;

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
        if (charaCounter == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
/*
    public class MainCharacter : Character
        {   
            public MainCharacter(Role _role,EmotionJauge _jauges)
            {
                
            }
        }
        */
    public Relation(Vignette _initialVignette,EmotionJauge _relationEmotion)
    {
        involvedCharacters = new Character[2];
        int charaVignetteAmount = _initialVignette.ObjectsNumber;
        //Déterminer le type de la relation
        switch (charaVignetteAmount)
        {
            case (1):
                {
                    //Un seul personnage dans la vignette
                    relationWithAvatar = true;
                    involvedCharacters[0] = _initialVignette.inVignetteCharacter[0];
                    break;
                }
            case (2):
                {
                    //Deux personnages dans la vignette
                    relationWithAvatar = false;
                    involvedCharacters[0] = _initialVignette.inVignetteCharacter[0];
                    involvedCharacters[1] = _initialVignette.inVignetteCharacter[1];
                    break;
                }
            case (3):
                {
                    //Plus de deux personnages dans la vignette
                    relationWithAvatar = false;
                    involvedCharacters[0] = _initialVignette.inVignetteCharacter[0];
                    involvedCharacters[1] = _initialVignette.inVignetteCharacter[Random.Range(1, 3)];
                    break;
                }

        }
        assignedJauge = _relationEmotion;
        foreach (Character chara in involvedCharacters )
        {
            chara.currentRelation = this;
        }
    }

    // Store Characters
    // Jauge To Modify
    // Amount Of Relation
    // SendToCharacter
}
