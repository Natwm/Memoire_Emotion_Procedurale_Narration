using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{
    public Sprite closed_door;
    public Sprite open_door;
    SpriteRenderer render;
    GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        key = GameObject.Find("Indicateur_key");
    }

   public void SetDoor(bool door_open)
    {
        if (door_open)
        {
            render.sprite = open_door;
            key.SetActive(true);
        }
        else
        {
            render.sprite = closed_door;
            key.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
