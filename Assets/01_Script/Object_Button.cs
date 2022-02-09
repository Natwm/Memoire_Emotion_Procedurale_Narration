using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Button : MonoBehaviour
{
    public enum ObjectStatus
    {
        NONE,
        CLAIM,
        WANT,
        REJECT,
        EXCLUDE
    }

    [SerializeField] private ObjectStatus m_Status;

    [SerializeField] private UsableObject m_Data;

    [SerializeField] private ReclameStatus negociationState;

    public UsableObject Data { get => m_Data; set => m_Data = value; }
    public ObjectStatus Status { get => m_Status; set => m_Status = value; }
    public ReclameStatus NegociationState { get => negociationState; set => negociationState = value; }

    public void AffectByPlayer(UnityEngine.UI.Button myButton, Character_Button player)
    {
        if (NegociationManager.instance.selectedPlayer != null)
        {
            ObjectStatus objStatus = objStatus = ObjectStatus.NONE;

            switch (Status)
            {
                case ObjectStatus.NONE:
                    break;
                case ObjectStatus.CLAIM:
                    NegociationManager.instance.IncreaseNegociationTime(NegociationManager.instance.PrendreValue);
                    break;
                case ObjectStatus.WANT:
                    NegociationManager.instance.IncreaseNegociationTime(NegociationManager.instance.ReclamerValue);
                    break;
                case ObjectStatus.REJECT:
                    NegociationManager.instance.IncreaseNegociationTime(NegociationManager.instance.DeclinerValue);
                    break;
                case ObjectStatus.EXCLUDE:
                    NegociationManager.instance.IncreaseNegociationTime(NegociationManager.instance.RefuserValue);
                    break;
                default:
                    break;
            }

            switch (CanvasManager.instance.Action)
            {
                case CanvasManager.m_NegociationActions.NONE:
                    myButton.image.color = Color.white;
                    ResetObjectStatus();
                    objStatus = ObjectStatus.NONE;
                    break;

                case CanvasManager.m_NegociationActions.CLAIM:
                    objStatus = ObjectStatus.CLAIM;
                    ClaimEffect(objStatus, player, myButton);
                    break;

                case CanvasManager.m_NegociationActions.WANT:
                    objStatus = ObjectStatus.WANT;
                    WantEffect(objStatus, player, myButton);
                    break;

                case CanvasManager.m_NegociationActions.REJECT:
                    objStatus = ObjectStatus.REJECT;
                    RejectEffect(objStatus, player, myButton);
                    break;

                case CanvasManager.m_NegociationActions.EXCLUDE:
                    objStatus = ObjectStatus.EXCLUDE;
                    ExcludeEffect(objStatus, player, myButton);
                    break;

                default:
                    objStatus = ObjectStatus.NONE;
                    ResetObjectStatus();
                    //CanvasManager.instance.SetInkSlider();
                    break;
            }

            ReclameStatus status = new ReclameStatus(NegociationManager.instance.selectedPlayer, objStatus);

            negociationState = status;
            //CanvasManager.instance.SetInkSlider();
        }
    }

    #region Negociation Action Logics
    private void ClaimEffect(ObjectStatus status, Character_Button player, UnityEngine.UI.Button myButton)
    {
        if (Status != ObjectStatus.CLAIM)
        {
            if (NegociationManager.instance.ReduceNegociationTime(NegociationManager.instance.PrendreValue))
            {
                //SoundManager.instance.PlaySound_SelectedNegociation();
                //CanvasManager.instance.NegociationText.text = NegociationManager.instance.NegociationTime.ToString();
                status = ObjectStatus.CLAIM;
                ClaimObject(player);
                //Negociation_Dialog.instance.StartDialog(Negociation_Dialog.instance.Prendre_SO, player.AssignedElement, NegociationManager.instance.GlobalCrew.ToArray(), Data.NarrationName);

            }
            /*else
                SoundManager.instance.PlaySound_CantUseNegociation();*/
        }
        else
        {
            myButton.image.color = Color.white;
            ResetObjectStatus();
            status = ObjectStatus.NONE;
        }
    }

    private void WantEffect(ObjectStatus status, Character_Button player, UnityEngine.UI.Button myButton)
    {
        if (Status != ObjectStatus.WANT)
        {
            if (NegociationManager.instance.ReduceNegociationTime(NegociationManager.instance.ReclamerValue))
            {  
                status = ObjectStatus.WANT;
                /*SoundManager.instance.PlaySound_SelectedNegociation();
                CanvasManager.instance.NegociationText.text = NegociationManager.instance.NegociationTime.ToString();*/
                //myButton.image.color = Color.gray;
                WantObject(player);
                //Negociation_Dialog.instance.StartDialog(Negociation_Dialog.instance.Demander_SO, player.AssignedElement, NegociationManager.instance.GlobalCrew.ToArray(), Data.NarrationName);

            }
           /* else
                SoundManager.instance.PlaySound_CantUseNegociation();*/
        }
        else
        {
            status = ObjectStatus.NONE;
            myButton.image.color = Color.white;
            ResetObjectStatus();

        }
    }

    private void RejectEffect(ObjectStatus status, Character_Button player, UnityEngine.UI.Button myButton)
    {
        if (Status != ObjectStatus.REJECT)
        {
            if (NegociationManager.instance.ReduceNegociationTime(NegociationManager.instance.DeclinerValue))
            {
                status = ObjectStatus.REJECT;
                /*SoundManager.instance.PlaySound_SelectedNegociation();
                CanvasManager.instance.NegociationText.text = NegociationManager.instance.NegociationTime.ToString();*/
                //myButton.image.color = Color.yellow;
                RejectObject(player);
                //Negociation_Dialog.instance.StartDialog(Negociation_Dialog.instance.Decliner_SO, player.AssignedElement, NegociationManager.instance.GlobalCrew.ToArray(), Data.NarrationName);
            }
            /*else
                SoundManager.instance.PlaySound_CantUseNegociation();*/
        }
        else
        {
            status = ObjectStatus.NONE;
            myButton.image.color = Color.white;
            ResetObjectStatus();
        }
    }

    private void ExcludeEffect(ObjectStatus status, Character_Button player, UnityEngine.UI.Button myButton)
    {
        if (Status != ObjectStatus.EXCLUDE)
        {
            if (NegociationManager.instance.ReduceNegociationTime(NegociationManager.instance.RefuserValue))
            {
                status = ObjectStatus.EXCLUDE;
                /*SoundManager.instance.PlaySound_SelectedNegociation();
                CanvasManager.instance.NegociationText.text = NegociationManager.instance.NegociationTime.ToString();*/
                //myButton.image.color = Color.red;
                ExcludeObject(player);
                //Negociation_Dialog.instance.StartDialog(Negociation_Dialog.instance.Refuser_SO, player.AssignedElement, NegociationManager.instance.GlobalCrew.ToArray(), Data.NarrationName);
            }
            /*else
                SoundManager.instance.PlaySound_CantUseNegociation();*/
        }
        else
        {
            status = ObjectStatus.NONE;
            myButton.image.color = Color.white;
            ResetObjectStatus();
        }
    }

    #endregion

    #region Negociation Object UI
    public void ClaimObject(Character_Button player)
    {
        Status = ObjectStatus.CLAIM;
        ResetImage();
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().color = NegociationManager.instance.selectedPlayer.CharacterData.AssignedElement.Color;
    }

    public void WantObject(Character_Button player)
    {
        Status = ObjectStatus.WANT;
        ResetImage();
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().color = NegociationManager.instance.selectedPlayer.CharacterData.AssignedElement.Color; ;
    }

    public void RejectObject(Character_Button player)
    {
        Status = ObjectStatus.REJECT;
        ResetImage();
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.GetComponent<UnityEngine.UI.Image>().color = NegociationManager.instance.selectedPlayer.CharacterData.AssignedElement.Color; ;
    }

    public void ExcludeObject(Character_Button player)
    {
        Status = ObjectStatus.EXCLUDE;
        ResetImage();
        transform.GetChild(4).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.GetComponent<UnityEngine.UI.Image>().color = NegociationManager.instance.selectedPlayer.CharacterData.AssignedElement.Color; ;
    }

    #endregion

    public void ResetObjectStatus()
    {
        switch (Status)
        {
            case ObjectStatus.NONE:
                break;
            case ObjectStatus.CLAIM:
                //CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString();
                break;
            case ObjectStatus.WANT:
                //CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString();
                break;
            case ObjectStatus.REJECT:
                //CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString();
                break;
            case ObjectStatus.EXCLUDE:
                //CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString();
                break;
            default:
                break;
        }
        Status = ObjectStatus.NONE;

        ResetImage();
    }

    private void ResetImage()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
