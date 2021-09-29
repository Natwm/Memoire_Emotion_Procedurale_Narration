using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bd_Elt_Behaviours : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private float m_RadiusDetection;
    [SerializeField] private LayerMask m_LayerDetection;
    [SerializeField] private bool m_IsLook;

    [SerializeField] private List<GameObject> listOfAffectedObject = new List<GameObject>();

    [SerializeField] private SpriteRenderer cardImage;

    [SerializeField] private Carte_SO value;

    public Vector3 offset;

    [Space]
    [Header ("Text Status")]
    [SerializeField] private TMPro.TMP_Text staminaText;
    [SerializeField] private TMPro.TMP_Text healthText;

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

        rayPoint += offset;

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

        if (value.HealthAffect == Carte_SO.Affect.USE)
        {
            if(value.HealthEffect== Carte_SO.Status.BONUS)
            {
                healthText.text = "+" + value.Health.ToString();
                healthText.color = Color.black;
            }
            else
            {
                healthText.text = "-" + value.Health.ToString();
            }
        }
        else
        {
            healthText.text = "0";
        }

        if (value.MovementAffect == Carte_SO.Affect.USE)
        {
            if (value.MovementEffect == Carte_SO.Status.BONUS)
            {
                staminaText.text = "+" + value.Movement.ToString();
                staminaText.color = Color.black;
            }
            else
            {
                staminaText.text = "-" + value.Movement.ToString();
            }
        }
        else
        {
            staminaText.text = "0";
        }
    }
    

    #region Interface
    public void OnPointerUp(PointerEventData eventData)
    {
        print("Stop");
        GridManager.instance.CheckTile();
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_RadiusDetection);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 data = Camera.main.ScreenToWorldPoint(eventData.position);
        data.z = transform.position.z;
        //AJouter la distance entre le pivot et le curseur;
        offset = transform.position - (Vector3)data;
    }
}
