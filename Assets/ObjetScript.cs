using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetScript : MonoBehaviour
{

    public SpriteRenderer sprite;
    public enum Status
    {
        HAUT,
        BAS
    }

    public Status typeOfObject;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
