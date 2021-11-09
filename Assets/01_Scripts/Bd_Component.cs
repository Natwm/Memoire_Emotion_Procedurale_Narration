using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/* ORDER IN LAYER
 * Fond blanc : -5
 * Décors : Entre -4 et 0
 * Effets : 1
 * Personnages : Entre 1 et 5
 * Ui : 11
*/
public class Bd_Component : MonoBehaviour
{
    public static Bd_Component bd_instance;

    public GameObject[] InVignetteSpawn;

    public GameObject[] Gabarit_Composition;
    public GameObject[] Vignettes;
    public GameObject vignetteHolder;
    public Sprite[] Expression_Sprite;
    Vignette currentVignette;
    public int Pose;
    public Color character_Color;
    public int iterationNumber;
    Vector3 VignettePos = Vector3.zero;
    int VignetteIndex = 1;
    public List<Vignette> VignetteSequence;
    public float gouttiere;
    string[] cases = { "1x1","1x2","2x1","2x2" };
    private void Awake()
    {
        if (bd_instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GameManager");
        else
            bd_instance = this;
        VignetteSequence = new List<Vignette>();
    }

    private void Start()
    {

        for (int i = 0; i < iterationNumber; i++)
        {
           // CreateNewRandomVignette();
        }
       
    }

    public GameObject GetComp(int caseIndex)
    {
        string chemin = "Generation/Composition/" + cases[caseIndex];
        return Resources.Load<GameObject>(chemin);
    }

    public GameObject GetComp(Vector2 caseShape)
    {
        string chemin = "Generation/Composition/" + caseShape.x + "x" + caseShape.y;
        return Resources.Load<GameObject>(chemin);
    }

    public GameObject GetVignette(int caseIndex)
    {
        string chemin = "Generation/Vignette/" + "case_" + cases[caseIndex];
        return Resources.Load<GameObject>(chemin);
    }

    public GameObject GetVignette(Vector2 caseShape)
    {
        string chemin = "Generation/Vignette/" + "case_" + caseShape.x+"x"+caseShape.y;
        return Resources.Load<GameObject>(chemin);
    }

    public void SetVignetteToOject(GameObject ObjToSet)
    {
        string ObjName = ObjToSet.name;
        int vignetteIndexType = 0;
        for (int i = 0; i < cases.Length; i++)
        {
            if (ObjName.Contains(cases[i]))
            {
                vignetteIndexType = i;
            }
        }
        Vignette newVignette = new Vignette(cases[vignetteIndexType], GetVignette(vignetteIndexType), ObjToSet.transform, InVignetteSpawn, GetComp(vignetteIndexType));
        ObjToSet.GetComponent<Vignette_Behaviours>().vignetteInfo = newVignette.Vignette_Object;
        ObjToSet.GetComponent<Vignette_Behaviours>().assignedVignette = newVignette;
        VignetteSequence.Add(newVignette);
    }

    public void SetVignetteToObjectCreate(GameObject ObjToSet, Character[] charactersToSet)
    {
        string ObjName = ObjToSet.name;
        Vector2 shape = ObjToSet.GetComponent<Vignette_Behaviours>().VignetteShape;
        string shapeString = shape.x + "x" + shape.y;

        Vignette newVignette = new Vignette(shape, GetVignette(shape), ObjToSet.transform, InVignetteSpawn, GetComp(shape), charactersToSet);
        ObjToSet.GetComponent<Vignette_Behaviours>().vignetteInfo = newVignette.Vignette_Object;
        ObjToSet.GetComponent<Vignette_Behaviours>().assignedVignette = newVignette;

        CastingManager.instance.SetCharacterToVignette(newVignette, charactersToSet);

        VignetteSequence.Add(newVignette);
    }

    public void CreateNewRandomVignette(int numberOfVignette)
    {
        for (int i = 0; i < numberOfVignette; i++)
        {
        Pose = Random.Range(0, InVignetteSpawn.Length);
        int randomObjNumber = Random.Range(1, 3);
        int randomObj = Random.Range(0, InVignetteSpawn.Length);
        int randomInt = Random.Range(0, cases.Length);
        GameObject newHolder = new GameObject();
        newHolder.name = "Vignette_" + VignetteIndex;
        VignetteIndex++;
        
        
        Vignette tempVignette = new Vignette(cases[randomInt], GetVignette(randomInt), newHolder.transform, InVignetteSpawn, GetComp(randomInt));
            
        tempVignette.Vignette_Object.transform.localPosition = Vector3.zero;
        float Vignette_shift = 0;
        if (currentVignette != null)
        {
            Vignette_shift = tempVignette.Vignette_Object.GetComponent<MeshCollider>().bounds.extents.x + currentVignette.Vignette_Object.GetComponent<MeshCollider>().bounds.extents.x + gouttiere;
        }        
        VignettePos.x += Vignette_shift;
        newHolder.transform.position = VignettePos;
        VignetteSequence.Add(tempVignette);
        currentVignette = tempVignette;
        
        }
    }

    private void Update()
    {
       
    }

    public void SpawnVignette()
    {
       // Vignette newV = new Vignette(1, Vignettes[0], vignetteHolder.transform, InVignetteSpawn,Gabarit_Composition[0]);
    }

    public Sprite GetRandomSprite()
    {
        return Expression_Sprite[Random.Range(0, Expression_Sprite.Length)];
    } 

}

/* Distribution :
 * Créer casting de personnage
 * Créer Main de vignettes
 * Assigner des personnages aux vignettes
 * Créer les icons de faces dans les points de composition
 * 
*/

public class Vignette
{
    public int ObjectsNumber;
    public GameObject Vignette_Object;
    public GameObject Cadre_Object;
    SpriteRenderer Sprite_Vignette;
    public SpriteMask Mask_Vignette; 
    Bd_Object[] InVignette_Objects;
    public List<GameObject> inVignette_CharacterIcons;
    public Character[] inVignetteCharacter;
    public Character_Behaviours[] inVignetteCharacter_Behaviours;
    GameObject Gabarit_Composition;

    public Vignette(string vignetteType,GameObject _vignetteType,Transform _parent,GameObject[] _obj,GameObject _gabarit)
    {
        inVignette_CharacterIcons = new List<GameObject>();
        // INITIALISATION VIGNETTE
        GameObject tempVignette = GameObject.Instantiate(_vignetteType);
        tempVignette.transform.parent = _parent;
        tempVignette.transform.localPosition = Vector3.zero;
        Vignette_Object = tempVignette;
        Cadre_Object = tempVignette.transform.GetChild(0).gameObject;
        GameObject tempGab = GameObject.Instantiate(_gabarit, Cadre_Object.transform);
        Gabarit_Composition = tempGab;
        Gabarit_Composition.transform.localPosition = Vector3.zero;
        Gabarit_Composition.transform.localScale = Vector3.one;
        Cadre_Object.transform.localPosition = Vector3.zero;
        switch (vignetteType)
        {
            case ("1x1"):
                {
                    ObjectsNumber = 1;
                    break;
                }
            case ("1x2"):
                {
                    ObjectsNumber = 2;
                    break;
                }
            case ("2x1"):
                {
                    ObjectsNumber = 2;
                    break;
                }
            case ("2x2"):
                {
                    ObjectsNumber = 3;
                    break;
                }

        }
        Sprite_Vignette = Cadre_Object.GetComponent<SpriteRenderer>();
        Mask_Vignette = Cadre_Object.GetComponent<SpriteMask>();
        inVignetteCharacter = new Character[ObjectsNumber - 1];
    }

    public Vignette(Vector2 vignetteType, GameObject _vignetteType, Transform _parent, GameObject[] _obj, GameObject _gabarit, Character[] selectedCharacter)
    {
        inVignette_CharacterIcons = new List<GameObject>();
        // INITIALISATION VIGNETTE
        GameObject tempVignette = GameObject.Instantiate(_vignetteType);
        tempVignette.transform.parent = _parent;
        tempVignette.transform.localPosition = Vector3.zero;
        Vignette_Object = tempVignette;
        Cadre_Object = tempVignette.transform.GetChild(0).gameObject;
        GameObject tempGab = GameObject.Instantiate(_gabarit, Cadre_Object.transform);
        Gabarit_Composition = tempGab;
        Gabarit_Composition.transform.localPosition = Vector3.zero;
        Gabarit_Composition.transform.localScale = Vector3.one;
        Cadre_Object.transform.localPosition = Vector3.zero;

        string vignetteShape = vignetteType.x + "x" + vignetteType.y;

        switch (vignetteShape)
        {
            case ("1x1"):
                {
                    ObjectsNumber = 1;
                    break;
                }
            case ("1x2"):
                {
                    ObjectsNumber = 2;
                    break;
                }
            case ("2x1"):
                {
                    ObjectsNumber = 2;
                    break;
                }
            case ("2x2"):
                {
                    ObjectsNumber = 3;
                    break;
                }

        }
        Sprite_Vignette = Cadre_Object.GetComponent<SpriteRenderer>();
        Mask_Vignette = Cadre_Object.GetComponent<SpriteMask>();
        inVignetteCharacter = selectedCharacter;
    }
    public Vignette(Vector2 vignetteType, GameObject _vignetteType, Transform _parent, GameObject[] _obj, GameObject _gabarit, Character_Behaviours[] selectedCharacter)
    {
        inVignette_CharacterIcons = new List<GameObject>();
        // INITIALISATION VIGNETTE
        GameObject tempVignette = GameObject.Instantiate(_vignetteType);
        tempVignette.transform.parent = _parent;
        tempVignette.transform.localPosition = Vector3.zero;
        Vignette_Object = tempVignette;
        Cadre_Object = tempVignette.transform.GetChild(0).gameObject;
        GameObject tempGab = GameObject.Instantiate(_gabarit, Cadre_Object.transform);
        Gabarit_Composition = tempGab;
        Gabarit_Composition.transform.localPosition = Vector3.zero;
        Gabarit_Composition.transform.localScale = Vector3.one;
        Cadre_Object.transform.localPosition = Vector3.zero;

        string vignetteShape = vignetteType.x + "x" + vignetteType.y;

        switch (vignetteShape)
        {
            case ("1x1"):
                {
                    ObjectsNumber = 1;
                    break;
                }
            case ("1x2"):
                {
                    ObjectsNumber = 2;
                    break;
                }
            case ("2x1"):
                {
                    ObjectsNumber = 2;
                    break;
                }
            case ("2x2"):
                {
                    ObjectsNumber = 3;
                    break;
                }

        }
        Sprite_Vignette = Cadre_Object.GetComponent<SpriteRenderer>();
        Mask_Vignette = Cadre_Object.GetComponent<SpriteMask>();
        inVignetteCharacter_Behaviours = selectedCharacter;
    }

    public void CharacterFeedback()
    {
        foreach (GameObject item in inVignette_CharacterIcons)
        {
            item.transform.DOScale(10f,0.1f);
            item.transform.DOScale(3f, 0.5f);
        }
    }

    public void UpdateCharactersState(int TJ_amount,int PC_amount,Character _whatCharacter)
    {
        // Determiner la jauge mise en action
        switch (_whatCharacter.currentJauge)
        {
            case EmotionJauge.Jauge_PeurColere:
                {
                    foreach (GameObject item in inVignette_CharacterIcons)
                    {
                        _whatCharacter.UpdateCharacterFaceIcon(item,PC_amount);
                        //item.transform.DOPunchScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f, 0, 0);
                        
                    }
                break;
                }
            case EmotionJauge.Jauge_TristesseJoie:
                {
                    foreach (GameObject item in inVignette_CharacterIcons)
                    {
                        _whatCharacter.UpdateCharacterFaceIcon(item, TJ_amount);
                        
                        
                    }
                break;
                }
        }
      
    }

    public GameObject GetRandomCompositionPoint()
    {
        int randomPoint = Random.Range(0, Gabarit_Composition.transform.childCount);
        
        GameObject newPos = Gabarit_Composition.transform.GetChild(randomPoint).gameObject;
        
        if (newPos.activeInHierarchy)
        {
            return newPos;
        }
        else
        {
            return GetRandomCompositionPoint();
        }
        

    }

  
}

public enum ObjectType
{
    CHARACTER, INANIMATE_OBJECT
}

public class Bd_Object
{
    GameObject Holder;
    Vignette Parent_Vignette;
    SpriteRenderer ObjectLine_Renderer;
    GameObject Bg_Holder;
    SpriteRenderer ObjectBackground_Renderer;

    
    void SetToVignette(Vignette _vignette,GameObject _obj,Vector3 _objPos,Color _color)
    {
        //References
        Parent_Vignette = _vignette;
        GameObject temp_Object = GameObject.Instantiate(_obj, Parent_Vignette.Cadre_Object.transform);
        Holder = temp_Object;
        //POSITION DE L'OBJET
        Holder.transform.localPosition =_objPos;
        Holder.transform.localScale = new Vector3(1f, 1f, 1f);
        Parent_Vignette = _vignette;

        //Masking & Sprite Management
        ObjectLine_Renderer = Holder.transform.GetChild(1).GetComponent<SpriteRenderer>();
        ObjectLine_Renderer.sortingLayerName = Parent_Vignette.Mask_Vignette.sortingLayerName;
        ObjectLine_Renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        ObjectLine_Renderer.sortingOrder = 1;
        ObjectLine_Renderer.color = _color;

        //Background Masking
        Bg_Holder = Holder.transform.GetChild(2).gameObject;
        ObjectBackground_Renderer = Bg_Holder.GetComponent<SpriteRenderer>();
        ObjectBackground_Renderer.sortingLayerName = ObjectLine_Renderer.sortingLayerName;
        ObjectBackground_Renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        ObjectBackground_Renderer.sortingOrder = 0;
    }
    //CONSTRUCTEUR OBJET INANIMÉ
    public Bd_Object(Vignette _Vignette,GameObject _baseBd_Object,Vector3 _pos,Color _color)
    {
        SetToVignette(_Vignette, _baseBd_Object,_pos,_color);
    }

    GameObject Expression_Pivot;
    SpriteRenderer Expression_Renderer;
    Sprite currentExpression;

    //CONSTRUCTEUR PERSONNAGE
    public Bd_Object(Vignette _Vignette, GameObject _baseBd_Object,Color _characterColor,Sprite _expression,Vector3 _pos) 
    {
        
        SetToVignette(_Vignette, _baseBd_Object,_pos,_characterColor);

        //CharacterExpression
        Expression_Pivot = Holder.transform.GetChild(0).gameObject;
        currentExpression = _expression;
        Expression_Renderer = Expression_Pivot.GetComponent<SpriteRenderer>();
        Expression_Renderer.sprite = _expression;
        Expression_Renderer.color = Color.black;
        Expression_Renderer.sortingLayerName = ObjectLine_Renderer.sortingLayerName;
        Expression_Renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

}
