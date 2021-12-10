using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 MoveVector()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * MoveSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 NewPos = new Vector3(transform.position.x + MoveVector().x, transform.position.y + MoveVector().y,-10);
        transform.position = NewPos;
    }
}
