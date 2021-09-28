using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bd_Elt_Behaviours : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float m_RadiusDetection;
    [SerializeField] private LayerMask m_LayerDetection;
    [SerializeField] private bool m_IsLook;

    [SerializeField] private List<GameObject> listOfAffectedObject = new List<GameObject>();

    [SerializeField] private SpriteRenderer cardImage;

    [SerializeField] private Carte_SO value;

    public Carte_SO Value { get => value; set => this.value = value; }
    public List<GameObject> ListOfAffectedObject { get => listOfAffectedObject; set => listOfAffectedObject = value; }

    // Start is called before the first frame update
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.Drag;

        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    private void OnDragDelegate(PointerEventData data)
    {
        //Create a ray going from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(data.position);
        //Calculate the distance between the Camera and the GameObject, and go this distance along the ray
        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
        rayPoint.Set(rayPoint.x, rayPoint.y, -2f);
        //Move the GameObject when you drag it
        if(!m_IsLook)
            transform.position = rayPoint;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetUpCard()
    {
        cardImage.sprite = value.CardSprite;
    }
    

    #region Interface
    public void OnPointerClick(PointerEventData eventData)
    {
        print("Stop");
        GridManager.instance.CheckTile();
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_RadiusDetection);
    }
}
