using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CastingManager : MonoBehaviour
{
    public static CastingManager instance;
    
    string[] Names = { "René","Josiane","Michel","Albert","Lucie","Sylvie","Maurice","Mauricette","Nathan","Sonia","Simon","Adrien","Julien","Morgane","Killian","Thomas","Pierre","José","Nicolas","Brigitte","Vivienne","Jean"};
    public Color CharacterColors;
    public GameObject iconRenderer;
    Character[] allCharacters;
    GameObject[] allCharacterIcons;

    [Header("Face Icon Sprites")]
    public Sprite baseFaceIcon;
    public Sprite NeutralFace;

    [Header("Facial Features")]
    public Sprite[] TopSprites;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        CreateCharacter(6);
    }

    void Start()
    {
        
        
        Bd_Component.bd_instance.CreateNewRandomVignette(5);
        foreach (Vignette item in Bd_Component.bd_instance.VignetteSequence)
        {
            Debug.Log(item.Cadre_Object.name);
            SetCharacterToVignette(item);
        }
    
    }

    public Character getRandomCharacter()
    {
        int randIndex = Random.Range(0, allCharacters.Length);
        return allCharacters[randIndex];
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
        foreach (Character item in _vignetteToSet.inVignetteCharacter)
        {
            item.SetIconToVignette(_vignetteToSet);
        }
        
    }

    public void CreateCharacter(int CharacterNumber)
    {
        allCharacters = new Character[CharacterNumber];
        allCharacterIcons = new GameObject[CharacterNumber];
        for (int i = 0; i < CharacterNumber; i++)
        {
            allCharacters[i] = new Character(Role.None, EmotionJauge.Jauge_PeurColere);
           
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



public class Character
{
    //In Vignette Character
    public GameObject characterHolder;
    SpriteRenderer characterRenderer;
    
   
    // CharacterData
    public List<GameObject> characterFaceIcon;
    public TMP_Text nameDisplay;
    public string characterName;
    Color characterColor;
    Sprite faceFeature;

    // CharacterEmotion
    public EmotionJauge currentJauge;
    public Role currentRole = Role.None;
    public int jaugeNumber = 0;
    public SpriteRenderer EmotionSprite;

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
        faceFeature = CastingManager.instance.TopSprites[randomTop];
        

        characterName = CastingManager.instance.GetName();
        currentRole = character_Role;
        currentJauge = currentEmotion;

        
    }

   

    public void SetIconToVignette(Vignette _vignette)
    {
        GameObject newPoint = _vignette.GetRandomCompositionPoint();
        GameObject tempFace = CreateFace(newPoint);
        newPoint.SetActive(false);
        tempFace.transform.parent = _vignette.Cadre_Object.transform;
        characterFaceIcon.Add(tempFace);

    }
}
