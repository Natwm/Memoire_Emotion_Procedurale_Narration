using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GameManager");
        else
            instance = this;
    }

    public void IsMovementvalid()
    {
        int value = 0;

        EventGenerator eventgenerator = FindObjectOfType<EventGenerator>();
        GameObject entryGO = null;
        GameObject exitGO = null;

        if (eventgenerator.EntryTile.GetComponent<TileElt_Behaviours>().EventAssocier !=null)
            entryGO = eventgenerator.EntryTile.GetComponent<TileElt_Behaviours>().EventAssocier.gameObject;

        if (eventgenerator.ExitTile.GetComponent<TileElt_Behaviours>().EventAssocier != null)
            exitGO = eventgenerator.ExitTile.GetComponent<TileElt_Behaviours>().EventAssocier.gameObject;

        GameObject keyGO = eventgenerator.occupiedTiles[eventgenerator.occupiedTiles.Count-1];

        bool isEntryConnected = eventgenerator.EntryTile.GetComponent<TileElt_Behaviours>().EventAssocier != null;
        bool isExitConnected = eventgenerator.ExitTile.GetComponent<TileElt_Behaviours>().EventAssocier != null;

  //      print("  keyGO = " + keyGO.gameObject);
//        print("  exitGO = " + exitGO.gameObject);

    //    print(" EntryTile  " + eventgenerator.EntryTile.GetComponent<TileElt_Behaviours>().gameObject);
      //  print(" ExitTile   " + eventgenerator.ExitTile.GetComponent<TileElt_Behaviours>().gameObject);

        if (isEntryConnected && isExitConnected)
        {
            GridManager.instance.SortList();
            
            if(entryGO != null && exitGO != null)
            {

                for (int i = 0; i < GridManager.instance.ListOfMovement.Count ; i++)
                {
                    Vignette_Behaviours stepBD = GridManager.instance.ListOfMovement[i].EventAssocier;
                    print(i + "  stepBD = " + stepBD.gameObject);
                    if(stepBD.NextMove !=null)
                        print(i + "  NextstepBD = " + stepBD.NextMove.gameObject);

                    if (stepBD.NextMove != null)
                    {
                        GameObject myStep = stepBD.gameObject;
                        if (myStep == entryGO)
                        {
                            print("EntryGooooooo");
                            value++;
                        }
                            
                        else if (myStep == exitGO)
                        {
                            print("ExitGooooooo");
                            value++;
                        }
                            
                        else if (myStep == keyGO)
                        {
                            print("KeyGooooooo");
                            value++;
                        }
                            

                    }
                }
                GameObject lastStep = GridManager.instance.ListOfMovement[GridManager.instance.ListOfMovement.Count-1].EventAssocier.gameObject;
                if (lastStep == entryGO)
                {
                    print("EntryGooooooo");
                    value++;
                }

                else if (lastStep == exitGO)
                {
                    print("ExitGooooooo");
                    value++;
                }

                else if (lastStep == keyGO)
                {
                    print("KeyGooooooo");
                    value++;
                }

            }
        }
        if (value > 3)
        {
            CanvasManager.instance.SetActiveMoveButton(true);
        }
        else
        {
            CanvasManager.instance.SetActiveMoveButton(false);
        }
        print("Valeue is : " + value);
    }

    public void GameOver()
    {
        CanvasManager.instance.PlayerLooseTheGame();
    }


}
