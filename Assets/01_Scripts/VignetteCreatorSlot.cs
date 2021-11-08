using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VignetteCreatorSlot : MonoBehaviour, IDropHandler
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop" + "   =================================== " + eventData.pointerDrag);

        if (eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.GetComponent<VignetteShapeCreation>().can)
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Enter : "+collision.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Exit : " + collision.name);
    }
}
