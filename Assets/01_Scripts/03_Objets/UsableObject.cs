﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class abstractUsableObject : MonoBehaviour
{
    public abstract void ClaimObject();
    public abstract void WantObject();
    public abstract void RejectObject();
    public abstract void ExcludeObject();
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

    [SerializeField] private Object_SO m_Data;

    [SerializeField] private ReclameStatus stat;

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

    public void AffectByPlayer(UnityEngine.UI.Button myButton)
    {
        ObjectStatus objStatus = objStatus = ObjectStatus.NONE;

        switch (Status)
        {
            case ObjectStatus.NONE:
                break;
            case ObjectStatus.CLAIM:
                CreationManager.instance.IncreaseNegociationTime(75);
                break;
            case ObjectStatus.WANT:
                CreationManager.instance.IncreaseNegociationTime(33);
                break;
            case ObjectStatus.REJECT:
                CreationManager.instance.IncreaseNegociationTime(33);
                break;
            case ObjectStatus.EXCLUDE:
                CreationManager.instance.IncreaseNegociationTime(75);
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
                    if (CreationManager.instance.ReduceNegociationTime(75))
                    {

                        CanvasManager.instance.UpdateInkSlider(-75);
                        myButton.image.color = Color.green;
                        objStatus = ObjectStatus.CLAIM;
                        ClaimObject();

                    }
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
                    if (CreationManager.instance.ReduceNegociationTime(33))
                    {
                        objStatus = ObjectStatus.WANT;
                        CanvasManager.instance.UpdateInkSlider(-33);
                        myButton.image.color = Color.gray;
                        WantObject();

                    }
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
                    if (CreationManager.instance.ReduceNegociationTime(33))
                    {
                        objStatus = ObjectStatus.REJECT;
                        CanvasManager.instance.UpdateInkSlider(-33);
                        myButton.image.color = Color.yellow;
                        RejectObject();

                    }
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
                    if (CreationManager.instance.ReduceNegociationTime(75))
                    {
                        objStatus = ObjectStatus.EXCLUDE;
                        CanvasManager.instance.UpdateInkSlider(-75);
                        myButton.image.color = Color.red;
                        ExcludeObject();

                    }
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

        ReclameStatus status = new ReclameStatus(CreationManager.instance.selectedPlayer,objStatus);

        Stat = status;
        CanvasManager.instance.SetInkSlider();
    }

    #region abstract Methodes

    public override void ClaimObject()
    {
        Status = ObjectStatus.CLAIM;
    }

    public override void WantObject()
    {
        Status = ObjectStatus.WANT;
    }

    public override void RejectObject()
    {
        Status = ObjectStatus.REJECT;
    }

    public override void ExcludeObject()
    {
        Status = ObjectStatus.EXCLUDE;
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
                CanvasManager.instance.UpdateInkSlider(75);
                break;
            case ObjectStatus.WANT:
                CanvasManager.instance.UpdateInkSlider(33);
                break;
            case ObjectStatus.REJECT:
                CanvasManager.instance.UpdateInkSlider(33);
                break;
            case ObjectStatus.EXCLUDE:
                CanvasManager.instance.UpdateInkSlider(75);
                break;
            default:
                break;
        }
        Status = ObjectStatus.NONE;
    }

    #endregion

    #region Getter && Setter

    public ObjectStatus Status { get => m_Status; set => m_Status = value; }
    public Object_SO Data { get => m_Data; set => m_Data = value; }
    public ReclameStatus Stat { get => stat; set => stat = value; }

    #endregion

}
