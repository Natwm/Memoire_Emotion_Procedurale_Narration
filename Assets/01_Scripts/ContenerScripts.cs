using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContenerScripts : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    void Start()
    {
        CreationManager.instance.shape = Vector2.one * -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        print("ondrop");
        
        CreationManager.instance.shape = Vector2.one *-1;

        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            CreationManager.instance.shape = eventData.pointerDrag.GetComponent<Drag_and_drop>().shape;
        }
    }
}
