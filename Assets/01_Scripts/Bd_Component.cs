﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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
    List<Vignette> VignetteSequence;
    public float gouttiere;
    string[] cases = { "1x1","1x2","2x1","2x2" };
    private void Awake()
    {
        if (bd_instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GameManager");
        else
            bd_instance = this;
    }
    Keyboard kb;
    private void Start()
    {
        kb = InputSystem.GetDevice<Keyboard>();
        for (int i = 0; i < iterationNumber; i++)
        {
            CreateNewRandomVignette();
        }
    }

    public GameObject GetComp(int caseIndex)
    {
        string chemin = "Generation/Composition/" + cases[caseIndex];
        return Resources.Load<GameObject>(chemin);
    }

    public GameObject GetVignette(int caseIndex)
    {
        string chemin = "Generation/Vignette/" + "case_" + cases[caseIndex];
        return Resources.Load<GameObject>(chemin);
    }

    public void CreateNewRandomVignette()
    {
        Pose = Random.Range(0, InVignetteSpawn.Length);
        int randomObjNumber = Random.Range(1, 3);
        int randomObj = Random.Range(0, InVignetteSpawn.Length);
        int randomInt = Random.Range(0, cases.Length);
        GameObject newHolder = new GameObject();
        newHolder.name = "Vignette_" + VignetteIndex;
        VignetteIndex++;
        
        
        Vignette tempVignette = new Vignette(randomObjNumber, GetVignette(randomInt), newHolder.transform, InVignetteSpawn, GetComp(randomInt));
        tempVignette.Vignette_Object.transform.localPosition = Vector3.zero;
        float Vignette_shift = 0;
        if (currentVignette != null)
        {
            Vignette_shift = tempVignette.Vignette_Object.GetComponent<MeshCollider>().bounds.extents.x + currentVignette.Vignette_Object.GetComponent<MeshCollider>().bounds.extents.x + gouttiere;
        }        
        VignettePos.x += Vignette_shift;
        newHolder.transform.position = VignettePos;
        currentVignette = tempVignette;
    }

    private void Update()
    {
        if (kb.jKey.wasReleasedThisFrame)
        {
            //CreateNewRandomVignette();
        }
    }

    public void SpawnVignette()
    {
        Vignette newV = new Vignette(1, Vignettes[0], vignetteHolder.transform, InVignetteSpawn,Gabarit_Composition[0]);
    }

    public Sprite GetRandomSprite()
    {
        return Expression_Sprite[Random.Range(0, Expression_Sprite.Length)];
    } 

}

public class Vignette
{
    int ObjectsNumber;
    public GameObject Vignette_Object;
    public GameObject Cadre_Object;
    SpriteRenderer Sprite_Vignette;
    public SpriteMask Mask_Vignette; 
    Bd_Object[] InVignette_Objects;
    GameObject Gabarit_Composition;

    public Vignette(int _objectInVignette,GameObject _vignetteType,Transform _parent,GameObject[] _obj,GameObject _gabarit)
    {
        // INITIALISATION VIGNETTE
        GameObject tempVignette = GameObject.Instantiate(_vignetteType,_parent);
        Vignette_Object = tempVignette;
        Cadre_Object = tempVignette.transform.GetChild(0).gameObject;
        GameObject tempGab = GameObject.Instantiate(_gabarit, Cadre_Object.transform);
        Gabarit_Composition = tempGab;
        Gabarit_Composition.transform.localPosition = Vector3.zero;
        Gabarit_Composition.transform.localScale = Vector3.one;
        Cadre_Object.transform.localPosition = Vector3.zero;
        Sprite_Vignette = Cadre_Object.GetComponent<SpriteRenderer>();
        Mask_Vignette = Cadre_Object.GetComponent<SpriteMask>();


        // INITIALISATION OBJETS
        InVignette_Objects = new Bd_Object[_objectInVignette];
        for (int i = 0; i < InVignette_Objects.Length; i++)
        {

            InVignette_Objects[i] = new Bd_Object(this,_obj[Bd_Component.bd_instance.Pose],Bd_Component.bd_instance.character_Color,Bd_Component.bd_instance.GetRandomSprite(),GetRandomCompositionPoint());
        }
        GameObject.Destroy(Gabarit_Composition);
    }

    public Vector3 GetRandomCompositionPoint()
    {
        int randomPoint = Random.Range(0, Gabarit_Composition.transform.childCount);
        
        Vector3 newPos = Gabarit_Composition.transform.GetChild(randomPoint).transform.localPosition;
        GameObject.Destroy(Gabarit_Composition.transform.GetChild(randomPoint).gameObject);
        return newPos;
    }

    /* Vignette Size
     * Vignette Composition
     * 
     * Get Character
     * Get Emotion
     */
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