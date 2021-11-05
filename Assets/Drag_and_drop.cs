using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag_and_drop : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler, IPointerUpHandler
{
    public Canvas canva;
    public CanvasGroup canvasGroupe;
    public RectTransform rectTransform;

    public Vector2 shape;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroupe = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canva = FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("begindrag");
        canvasGroupe.alpha = 0.8f;
        canvasGroupe.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        rectTransform.anchoredPosition += eventData.delta / canva.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("enddrag");
        canvasGroupe.alpha = 1f;
        canvasGroupe.blocksRaycasts = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //canvasGroupe.blocksRaycasts = false;
    }
}
