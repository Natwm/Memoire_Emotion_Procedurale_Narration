using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private List<Character_Button> m_OrderCharacter;

    public List<Character_Button> OrderCharacter { get => m_OrderCharacter; set => m_OrderCharacter = value; }

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
        GameObject keyGO = null;

        if (eventgenerator.EntryTile.GetComponent<TileElt_Behaviours>().EventAssocier != null)
            entryGO = eventgenerator.EntryTile.GetComponent<TileElt_Behaviours>().EventAssocier.gameObject;

        if (eventgenerator.ExitTile.GetComponent<TileElt_Behaviours>().EventAssocier != null)
            exitGO = eventgenerator.ExitTile.GetComponent<TileElt_Behaviours>().EventAssocier.gameObject;

        if (eventgenerator.occupiedTiles[eventgenerator.occupiedTiles.Count - 1].GetComponent<TileElt_Behaviours>().EventAssocier != null)
            keyGO = eventgenerator.occupiedTiles[eventgenerator.occupiedTiles.Count - 1].GetComponent<TileElt_Behaviours>().EventAssocier.gameObject;

        bool isEntryConnected = eventgenerator.EntryTile.GetComponent<TileElt_Behaviours>().EventAssocier != null;
        bool isExitConnected = eventgenerator.ExitTile.GetComponent<TileElt_Behaviours>().EventAssocier != null;
        bool isKeyConnected = keyGO != null;

        //      print("  keyGO = " + keyGO.gameObject);
        //      print("  exitGO = " + exitGO.gameObject);

        //    print(" EntryTile  " + eventgenerator.EntryTile.GetComponent<TileElt_Behaviours>().gameObject);
        //  print(" ExitTile   " + eventgenerator.ExitTile.GetComponent<TileElt_Behaviours>().gameObject);

        if (isEntryConnected && isExitConnected && isKeyConnected)
        {
            GridManager.instance.SortList();

            if (entryGO != null && exitGO != null && keyGO != null)
            {
                if (entryGO.GetComponent<Vignette_Behaviours>().OnGrid && exitGO.GetComponent<Vignette_Behaviours>().OnGrid && keyGO.GetComponent<Vignette_Behaviours>().OnGrid)
                {
                    for (int i = 0; i < GridManager.instance.ListOfMovement.Count; i++)
                    {
                        Vignette_Behaviours stepBD = GridManager.instance.ListOfMovement[i].EventAssocier;
                        //print(i + "  stepBD = " + stepBD.gameObject);
                        if (stepBD.NextMove != null)
                            print(i + "  NextstepBD = " + stepBD.NextMove.gameObject);

                        GameObject myStep = stepBD.gameObject;
                        if (myStep == entryGO)
                        {
                            //print("EntryGooooooo");
                            value++;
                        }

                        if (myStep == exitGO)
                        {
                            //print("ExitGooooooo");
                            value++;
                        }

                        if (myStep == keyGO)
                        {
                            //print("KeyGooooooo");
                            value++;
                        }

                        /*print("Exit   = " + myStep.name + "  ==  " + exitGO + " ||   " + (myStep == exitGO));
                        print("Entry   = " + myStep.name + "  ==  " + entryGO + " ||   " + (myStep == entryGO));
                        print("Key   = " + myStep.name + "  ==  " + keyGO + " ||   " + (myStep == keyGO));
                        print("value " + value);*/

                    }
                    GameObject lastStep = GridManager.instance.ListOfMovement[GridManager.instance.ListOfMovement.Count - 1].EventAssocier.gameObject;

                    //print("entryGO " + entryGO.name + " || exitGO " + exitGO + " || keyGO " + keyGO.name);

                    if (lastStep == entryGO)
                    {
                        //print("EntryGooooooo 2");
                        value++;
                    }

                    else if (lastStep == exitGO)
                    {
                        //print("ExitGooooooo 2");
                        value++;
                    }

                    else if (lastStep == keyGO)
                    {
                        //print("KeyGooooooo 2");
                        value++;
                    }
                    /*print("Nathan   2= " + lastStep.name + "  ==  " + keyGO + " ||   " + (lastStep == keyGO));
                    print("value " + value);*/
                }
            }


        }
        else
        {
            //print("IsMovementvalid not ");
            CanvasManager.instance.SetActiveMoveButton(false);
        }

        if (value >= 3)
        {
            //print("IsMovementvalid yes ");
            CanvasManager.instance.SetActiveMoveButton(true);
        }
        else
        {
           // print("IsMovementvalid no");
            CanvasManager.instance.SetActiveMoveButton(false);
        }
        //print("Valeue is : " + value);
    }

    public void IsGameOver()
    {
        PlayerManager.instance.Health--;
        if (PlayerManager.instance.Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            LevelManager.instance.NewPage();
        }
    }

    public void GameOver()
    {
        //CanvasManager.instance.PlayerLooseTheGame();
    }

    public void NextPlayer()
    {
        if(m_OrderCharacter.Count > 0)
        {
            print("next");
            CreationManager.instance.LaunchGame();
        }
        else
        {
            CanvasManager.instance.SetUpCreationPanel();
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
