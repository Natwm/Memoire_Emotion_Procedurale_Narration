using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomExitButton : MonoBehaviour
{
    public RoomExit RoomToExitTo;
    public void OnButtonClick()
    {
        RoomGenerator.instance.RoomToExitTo = RoomToExitTo;
    }
}
