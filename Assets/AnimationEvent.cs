using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void OnTransitionReached()
    {
        RoomGenerator.instance.CurrentRoom = RoomGenerator.instance.GoToRoom.Exit_To_Room;
        
        RoomGenerator.instance.InitialiseRoomToUi(RoomGenerator.instance.CurrentRoom);
        RoomGenerator.instance.SetIntro();

    }
}
