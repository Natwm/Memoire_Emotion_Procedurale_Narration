using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void OnTransitionReached()
    {
        RoomGenerator.instance.CurrentRoom = RoomGenerator.instance.GoToRoom.Exit_To_Room;
        RoomGenerator.instance.InitialiseRoomToUi(RoomGenerator.instance.CurrentRoom);
        if (RoomGenerator.instance.checkCompletion(RoomGenerator.instance.CurrentRoom))
        {
            Debug.Log("ALREADY COMPLETE...SETTING OUTRO");
            RoomGenerator.instance.SetOutro();
            RoomGenerator.instance.Intro.SetActive(false);
        }
        else
        {
            Debug.Log("NOT COMPLETE...SETTING INTRO");
            RoomGenerator.instance.SetIntro();
            RoomGenerator.instance.Outro.SetActive(false);
        }
        
        

    }
}
