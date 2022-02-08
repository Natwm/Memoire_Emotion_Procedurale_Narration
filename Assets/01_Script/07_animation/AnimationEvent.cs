using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void OnTransitionReached()
    {
        RoomGenerator.instance.CurrentRoom = RoomGenerator.instance.GoToRoom.Exit_To_Room;
        CanvasManager.instance.InitialiseRoomToUi(RoomGenerator.instance.CurrentRoom);
        if (RoomGenerator.instance.checkCompletion(RoomGenerator.instance.CurrentRoom))
        {
            Debug.Log("ALREADY COMPLETE...SETTING OUTRO");
            CanvasManager.instance.SetOutro();
            CanvasManager.instance.Intro.SetActive(false);
        }
        else
        {
            Debug.Log("NOT COMPLETE...SETTING INTRO");
            CanvasManager.instance.SetIntro();
            CanvasManager.instance.Outro.SetActive(false);
        }
        
        

    }
}
