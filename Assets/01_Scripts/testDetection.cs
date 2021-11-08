using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class testDetection : MonoBehaviour, IPointerUpHandler
{

    [Header("RayCastSize")]
    [SerializeField] private float radiusSize = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusSize);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print(Physics2D.CircleCastAll(transform.position, radiusSize, Vector2.zero).Length);
    }
}
