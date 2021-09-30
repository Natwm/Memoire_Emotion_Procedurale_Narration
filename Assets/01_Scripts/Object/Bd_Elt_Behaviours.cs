using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bd_Elt_Behaviours : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    #region param
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

    [Space]
    [Header("event")]
    [SerializeField] private EventContener myEvent;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.Drag;

        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);

        myEvent = GetComponent<EventContener>();
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
        MyEvent.SetUpEvent(value);

        SetUpUI();
    }

    public void SetUpCard(IModifier modifier)
    {
        modifier.CollectElement(myEvent);
        print("SetUpCard");
        SetUpUI();
    }


    private void SetUpUI()
    {
        if(myEvent.Health >= 0)
        {
            healthText.text = value.HealthEffect == Carte_SO.Status.BONUS? "+" + MyEvent.Health.ToString() : "-"+ MyEvent.Health.ToString();
            healthText.color = value.HealthEffect == Carte_SO.Status.BONUS ? Color.red : Color.black;
        }

        else
        {
            healthText.text = MyEvent.Health > 0 ?"-" + MyEvent.Health.ToString(): MyEvent.Health.ToString();
        }

        if (myEvent.Movement >= 0)
        {
            staminaText.text = value.MovementEffect == Carte_SO.Status.BONUS ? "+" + MyEvent.Movement.ToString() : "-" + MyEvent.Movement.ToString();
            staminaText.color = value.MovementEffect == Carte_SO.Status.BONUS ? Color.red : Color.black;
        }

        else
        {
            staminaText.text = MyEvent.Movement > 0 ? "-" + MyEvent.Movement.ToString() : MyEvent.Movement.ToString();
        }


        if (value.MovementAffect == Carte_SO.Affect.USE)
        {
            if (value.MovementEffect == Carte_SO.Status.BONUS)
            {
                staminaText.text = "+" + MyEvent.Movement.ToString();
                staminaText.color = Color.black;
            }
            else
            {
                staminaText.text = "-" + MyEvent.Movement.ToString();
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
        GridManager.instance.CheckTile();
        
        RaycastHit[] hit;
        int amountOfModifier = 0;

        hit = Physics.BoxCastAll(transform.GetChild(0).position, transform.localScale / 60f, Vector3.forward, Quaternion.identity, Mathf.Infinity, m_LayerDetection);
        print(hit.Length);
        if(hit.Length > 0)
        {
            foreach (var item in hit)
            {
                print(item.collider.name);
                IModifier myModifier;
                if(item.collider.TryGetComponent<IModifier>(out myModifier))
                {
                    SetUpCard(myModifier);
                    amountOfModifier++;
                }
            }
        }

        if (!(amountOfModifier > 0))
        {
            SetUpCard();
        }
        
    
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 data = Camera.main.ScreenToWorldPoint(eventData.position);
        data.z = transform.position.z;
        //AJouter la distance entre le pivot et le curseur;
        offset = transform.position - (Vector3)data;

        SetUpCard();
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.GetChild(0).position, transform.localScale / 60);
    }


    #region Getter & Setter
    public Carte_SO Value { get => value; set => this.value = value; }
    public List<GameObject> ListOfAffectedObject { get => listOfAffectedObject; set => listOfAffectedObject = value; }
    public EventContener MyEvent { get => myEvent; set => myEvent = value; }
    #endregion
}
