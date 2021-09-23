using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    DemoScript InputManager;
    Face_Manager Face;

    public float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        InputManager = FindObjectOfType<DemoScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float intensityModifier = 0.3f + 1 * InputManager.secondTriggerValue;
        Vector3 movementVector = new Vector3(InputManager.axisMovement.x * intensityModifier * MoveSpeed, 0 , InputManager.axisMovement.y * intensityModifier * MoveSpeed);
        print("movementVector "+movementVector);
        print("InputManager.axisMovement " + InputManager.axisMovement.y * intensityModifier * MoveSpeed);
        print(VectorMethods.CompareVector(movementVector, new Vector3(0.3f, 0.3f, 0.0f)));
       if(VectorMethods.CompareVector(movementVector, new Vector3(0.01f, 0f, 0.01f)))
            transform.Translate(movementVector);
    }
}
