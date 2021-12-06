using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private List<Character_Button> m_OrderCharacter;
    [SerializeField] private List<Character_Button> m_WaitingCharacter;

    public List<Character_Button> OrderCharacter { get => m_OrderCharacter; set => m_OrderCharacter = value; }
    public List<Character_Button> WaitingCharacter { get => m_WaitingCharacter; set => m_WaitingCharacter = value; }

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
                        /*if (stepBD.NextMove != null)
                            print(i + "  NextstepBD = " + stepBD.NextMove.gameObject);*/

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
        //CheckIfAllAreConnect();
        //print("Valeue is : " + value);
    }

    public void CheckIfAllAreConnect()
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

        if (isEntryConnected && isExitConnected && isKeyConnected)
        {
            GridManager.instance.SortList();

            if (entryGO != null && exitGO != null && keyGO != null)
            {
                if (entryGO.GetComponent<Vignette_Behaviours>().OnGrid && exitGO.GetComponent<Vignette_Behaviours>().OnGrid && keyGO.GetComponent<Vignette_Behaviours>().OnGrid)
                {
                    GameObject current = entryGO.GetComponent<Vignette_Behaviours>().gameObject;
                    while (current!=null)
                    {
                        if (current == entryGO || current == exitGO || current == keyGO)
                        {
                            value++;
                        }


                        if (current.GetComponent<Vignette_Behaviours>().NextMove != null)
                            if (current.GetComponent<Vignette_Behaviours>().NextMove.gameObject != current)
                                current = current.GetComponent<Vignette_Behaviours>().NextMove.gameObject;
                            else
                                current = null;
                        else
                            current = null;
                        //print(current.gameObject.name);
                    }
                }
            }
        }

        if(GridManager.instance.ListOfMovement.Count > 0)
        {
            GameObject lastStep = GridManager.instance.ListOfMovement[GridManager.instance.ListOfMovement.Count - 1].EventAssocier != null ? GridManager.instance.ListOfMovement[GridManager.instance.ListOfMovement.Count - 1].EventAssocier.gameObject : null; ;

            if (lastStep != null)
                if (lastStep == entryGO || lastStep == exitGO || lastStep == keyGO)
                    value++;
        }
        

        if (value >= 3)
        {
            //print("IsMovementvalid yes ");
            CanvasManager.instance.SetActiveMoveButton(true);
            SoundManager.instance.PlaySound_ResolutionAvailable();
        }
        else
        {
            // print("IsMovementvalid no");
            CanvasManager.instance.SetActiveMoveButton(false);
        }
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
            //LevelManager.instance.NewPage();
        }
    }

    public void GameOver()
    {
        //CanvasManager.instance.PlayerLooseTheGame();
    }

    public void NextPlayer()
    {

        if (m_OrderCharacter.Count > 0)
        {
            CreationManager.instance.LaunchGame();
        }
        else if(CreationManager.instance.listOfCharacter.Count > 0)
        {
            CreationManager.instance.ResetNegociationTime();
            CanvasManager.instance.SetUpCreationPanel();

            foreach (var item in LevelManager.instance.PageInventory)
            {
                Destroy(item.gameObject);
            }
            LevelManager.instance.PageInventory = new List<UsableObject>();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ContinueAdventure()
    {
        m_OrderCharacter.Clear();
        foreach (var item in m_WaitingCharacter)
        {
            m_OrderCharacter.Add(item);

            if(item.InventoryObj.Count > 0)
            {
                int index = Random.Range(0, item.InventoryObj.Count);
                item.InventoryObj.RemoveAt(index);
            }
            
        }
        m_WaitingCharacter.Clear();

        CanvasManager.instance.SetUpCharacterInfo();
        NextPlayer();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
