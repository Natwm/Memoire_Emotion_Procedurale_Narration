using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class abstractUsableObject : MonoBehaviour
{
    public abstract void ClaimObject(Character_Button player);
    public abstract void WantObject(Character_Button player);
    public abstract void RejectObject(Character_Button player);
    public abstract void ExcludeObject(Character_Button player);
    public abstract void ResetObjectStatus();
    public abstract void UseObject();
    public abstract void PickUpObject();
    public abstract void DropObject();
}

[System.Serializable]
public class ReclameStatus
{
    public Character_Button character;
    public UsableObject.ObjectStatus status;

   
    public ReclameStatus(Character_Button character, UsableObject.ObjectStatus status)
    {
        this.character = character;
        this.status = status;
    }
}


public class UsableObject : abstractUsableObject
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

    [SerializeField] protected Object_SO m_Data;

    [SerializeField] private ReclameStatus stat;

    [Space]
    [Header("Curse")]
    [SerializeField] private bool isCurse;
    [SerializeField] private Curse m_MyCurse;

    #region Awake || Start || Update
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    #endregion

    public UsableObject(Object_SO objRef)
    {
        Data = objRef;
    }

    public void AffectByPlayer(UnityEngine.UI.Button myButton, Character_Button player)
    {
        if (CreationManager.instance.selectedPlayer != null)
        {
            ObjectStatus objStatus = objStatus = ObjectStatus.NONE;

            switch (Status)
            {
                case ObjectStatus.NONE:
                    break;
                case ObjectStatus.CLAIM:
                    CreationManager.instance.IncreaseNegociationTime(CreationManager.instance.PrendreValue);
                    break;
                case ObjectStatus.WANT:
                    CreationManager.instance.IncreaseNegociationTime(CreationManager.instance.ReclamerValue);
                    break;
                case ObjectStatus.REJECT:
                    CreationManager.instance.IncreaseNegociationTime(CreationManager.instance.DeclinerValue);
                    break;
                case ObjectStatus.EXCLUDE:
                    CreationManager.instance.IncreaseNegociationTime(CreationManager.instance.RefuserValue);
                    break;
                default:
                    break;
            }

            switch (CreationManager.instance.Pen)
            {
                case CreationManager.m_PenStatus.NONE:
                    myButton.image.color = Color.white;
                    ResetObjectStatus();
                    objStatus = ObjectStatus.NONE;
                    break;

                case CreationManager.m_PenStatus.CLAIM:
                    if (Status != ObjectStatus.CLAIM)
                    {
                        if (CreationManager.instance.ReduceNegociationTime(CreationManager.instance.PrendreValue))
                        {
                            SoundManager.instance.PlaySound_SelectedNegociation();
                            CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString(); 
                            //myButton.image.color = Color.green;
                            objStatus = ObjectStatus.CLAIM;
                            ClaimObject(player);
                            Negociation_Dialog.instance.StartDialog(Negociation_Dialog.instance.Prendre_SO, player.AssignedElement, CreationManager.instance.GlobalCrew.ToArray(), Data.NarrationName);

                        }else
                            SoundManager.instance.PlaySound_CantUseNegociation();
                    }
                    else
                    {
                        myButton.image.color = Color.white;
                        ResetObjectStatus();
                        objStatus = ObjectStatus.NONE;
                    }
                    break;

                case CreationManager.m_PenStatus.WANT:
                    if (Status != ObjectStatus.WANT)
                    {
                        if (CreationManager.instance.ReduceNegociationTime(CreationManager.instance.ReclamerValue))
                        {
                            SoundManager.instance.PlaySound_SelectedNegociation();
                            objStatus = ObjectStatus.WANT;
                            CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString();
                            //myButton.image.color = Color.gray;
                            WantObject(player);
                            Negociation_Dialog.instance.StartDialog(Negociation_Dialog.instance.Demander_SO, player.AssignedElement, CreationManager.instance.GlobalCrew.ToArray(), Data.NarrationName);

                        }
                        else
                            SoundManager.instance.PlaySound_CantUseNegociation();
                    }
                    else
                    {
                        objStatus = ObjectStatus.NONE;
                        myButton.image.color = Color.white;
                        ResetObjectStatus();

                    }

                    break;

                case CreationManager.m_PenStatus.REJECT:
                    if (Status != ObjectStatus.REJECT)
                    {
                        if (CreationManager.instance.ReduceNegociationTime(CreationManager.instance.DeclinerValue))
                        {
                            SoundManager.instance.PlaySound_SelectedNegociation();
                            objStatus = ObjectStatus.REJECT;
                            CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString();
                            //myButton.image.color = Color.yellow;
                            RejectObject(player);
                            Negociation_Dialog.instance.StartDialog(Negociation_Dialog.instance.Decliner_SO, player.AssignedElement, CreationManager.instance.GlobalCrew.ToArray(), Data.NarrationName);
                        }
                        else
                            SoundManager.instance.PlaySound_CantUseNegociation();
                    }
                    else
                    {
                        objStatus = ObjectStatus.NONE;
                        myButton.image.color = Color.white;
                        ResetObjectStatus();
                    }
                    break;

                case CreationManager.m_PenStatus.EXCLUDE:
                    if (Status != ObjectStatus.EXCLUDE)
                    {
                        if (CreationManager.instance.ReduceNegociationTime(CreationManager.instance.RefuserValue))
                        {
                            SoundManager.instance.PlaySound_SelectedNegociation();
                            objStatus = ObjectStatus.EXCLUDE;
                            CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString();
                            //myButton.image.color = Color.red;
                            ExcludeObject(player);
                            Negociation_Dialog.instance.StartDialog(Negociation_Dialog.instance.Refuser_SO, player.AssignedElement, CreationManager.instance.GlobalCrew.ToArray(), Data.NarrationName);
                        }
                        else
                            SoundManager.instance.PlaySound_CantUseNegociation();
                    }
                    else
                    {
                        objStatus = ObjectStatus.NONE;
                        myButton.image.color = Color.white;
                        ResetObjectStatus();
                    }
                    break;

                default:
                    ResetObjectStatus();
                    CanvasManager.instance.SetInkSlider();
                    break;
            }

            ReclameStatus status = new ReclameStatus(CreationManager.instance.selectedPlayer, objStatus);

            Stat = status;
            CanvasManager.instance.SetInkSlider();
        }
    }

    #region abstract Methodes

    public override void ClaimObject(Character_Button player)
    {
        Status = ObjectStatus.CLAIM;
        ResetImage();
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().color = CreationManager.instance.selectedPlayer.AssignedElement.Color;
    }

    public override void WantObject(Character_Button player)
    {
        Status = ObjectStatus.WANT;
        ResetImage();
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image > ().color = CreationManager.instance.selectedPlayer.AssignedElement.Color; ;
    }

    public override void RejectObject(Character_Button player)
    {
        Status = ObjectStatus.REJECT;
        ResetImage();
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.GetComponent<UnityEngine.UI.Image>().color= CreationManager.instance.selectedPlayer.AssignedElement.Color; ;
    }

    public override void ExcludeObject(Character_Button player)
    {
        Status = ObjectStatus.EXCLUDE;
        ResetImage();
        transform.GetChild(4).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.GetComponent<UnityEngine.UI.Image>().color = CreationManager.instance.selectedPlayer.AssignedElement.Color; ;
    }

    public override void UseObject()
    {
        throw new System.NotImplementedException();
    }

    public override void PickUpObject()
    {
        throw new System.NotImplementedException();
    }

    public override void DropObject()
    {
        throw new System.NotImplementedException();
    }

    public override void ResetObjectStatus()
    {
        switch (Status)
        {
            case ObjectStatus.NONE:
                break;
            case ObjectStatus.CLAIM:
                CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString();
                break;
            case ObjectStatus.WANT:
                CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString();
                break;
            case ObjectStatus.REJECT:
                CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString();
                break;
            case ObjectStatus.EXCLUDE:
                CanvasManager.instance.NegociationText.text = CreationManager.instance.NegociationTime.ToString();
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
    #endregion

    #region Getter && Setter

    public ObjectStatus Status { get => m_Status; set => m_Status = value; }
    public Object_SO Data { get => m_Data; set => m_Data = value; }
    public ReclameStatus Stat { get => stat; set => stat = value; }
    public bool IsCurse { get => isCurse; set => isCurse = value; }
    public Curse MyCurse { get => m_MyCurse; set => m_MyCurse = value; }

    #endregion

}
