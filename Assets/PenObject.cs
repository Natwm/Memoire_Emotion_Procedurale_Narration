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

    public Image Render { get => render; set => render = value; }
    public Sprite Neutre { get => neutre; set => neutre = value; }

    private void Start()
    {
        Render = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (var item in FindObjectsOfType<PenObject>())
        {
            item.Render.sprite = item.neutre;
        }
        if(Render.sprite != presed && !pressed)
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Render.sprite = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(Render.sprite != presed)
            Render.sprite = Neutre;
    }

    public void SelectedButton()
    {
        if(GetComponent<UnityEngine.UI.Image>().color != Color.red)
            GetComponent<UnityEngine.UI.Image>().color = Color.red;
        else
            GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }
}
