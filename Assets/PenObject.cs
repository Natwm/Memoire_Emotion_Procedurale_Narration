using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PenObject : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{

    [SerializeField] private Sprite neutre;
    [SerializeField] private Sprite hover;
    [SerializeField] private Sprite presed;
    [SerializeField] private Sprite disable;

    [Space]

    [SerializeField] private Image render;

    bool pressed = false;

    [Space]
    [Header("SOUND")]
    protected FMOD.Studio.EventInstance hoverEffect;
    [FMODUnity.EventRef] [SerializeField] private string hoverSound;
    protected FMOD.Studio.EventInstance selectEffect;
    [FMODUnity.EventRef] [SerializeField] private string selectSound;

    public Image Render { get => render; set => render = value; }
    public Sprite Neutre { get => neutre; set => neutre = value; }

    private void Start()
    {
        Render = GetComponent<Image>();
    }

    public void InitButton()
    {
        if (GetComponent<Button>().interactable)
            render.sprite = neutre;
        else
        {
            render.sprite = disable;
        }
    }

    public void DisableInteraction()
    {
        render.sprite = disable;
    }

    public void EnableInteraction()
    {
        print("OBJECT : " + gameObject.name);
        render.sprite = neutre;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
        {
            foreach (var item in FindObjectsOfType<PenObject>())
            {
                item.Render.sprite = item.neutre;
            }
            if (Render.sprite != presed && !pressed)
            {
                Render.sprite = presed;
                pressed = true;
            }
            else
            {
                Render.sprite = neutre;
                pressed = false;
            }
        }
        
            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(GetComponent<Button>().interactable)
            Render.sprite = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
        {
            if (Render.sprite != presed)
                Render.sprite = Neutre;
        }
        
    }

    public void SelectedButton()
    {
        if(GetComponent<UnityEngine.UI.Image>().color != Color.red)
            GetComponent<UnityEngine.UI.Image>().color = Color.red;
        else
            GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }
}
