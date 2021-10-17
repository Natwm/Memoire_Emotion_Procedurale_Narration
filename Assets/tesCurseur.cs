using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class tesCurseur : MonoBehaviour
{
    Mouse mymouse;
    public Vector2 mouve;

    // Start is called before the first frame update
    void Start()
    {
        mymouse = InputSystem.GetDevice<Mouse>();
        print(mymouse.position.ReadValue());
    }

    public void pomme(InputAction.CallbackContext ctx)
    {
        print("^pmùùe");
        mouve = ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        /*print("Position screen = "+mymouse.position.ReadValue());
        print("Position world = " + Camera.main.ScreenToWorldPoint(mymouse.position.ReadValue()));
        print(Camera.main.ScreenToWorldPoint(mymouse.position.ReadValue()));*/
    }
}
