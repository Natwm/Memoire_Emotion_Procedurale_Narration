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

    [SerializeField] private Carte_SO value;

    public Carte_SO Value { get => value; set => this.value = value; }

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

    

    #region Interface
    public void OnPointerClick(PointerEventData eventData)
    {
        print("Stop");

        RaycastHit[] hit;
        hit = Physics.SphereCastAll(transform.position, m_RadiusDetection, Vector3.back, Mathf.Infinity, m_LayerDetection);

        if(hit.Length > 0 && hit[0].collider != null)
        {
            print(hit.Length);
            foreach (var item in hit)
            {
                TileElt_Behaviours tile = item.collider.GetComponent<TileElt_Behaviours>();
                tile.AssociateEventToTile(this);

                switch (value.HealthEffect)
                {
                    case Carte_SO.Status.BONUS:
                        item.collider.GetComponent<MeshRenderer>().material.color = Color.blue ;
                        break;
                    case Carte_SO.Status.MALUS:
                        item.collider.GetComponent<MeshRenderer>().material.color = Color.red;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_RadiusDetection);
    }
}
