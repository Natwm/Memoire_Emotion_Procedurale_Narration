using System.Collections;
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
        switch (CreationManager.instance.Pen)
        {
            case CreationManager.m_PenStatus.NONE:
                myButton.image.color = Color.white;
                ResetObjectStatus();
                break;

            case CreationManager.m_PenStatus.CLAIM:
                if (CreationManager.instance.ReduceNegociationTime(75))
                {
                    if (Status != ObjectStatus.CLAIM)
                    {

                        myButton.image.color = Color.green;
                        ClaimObject();
                    }
                    else
                    {
                        myButton.image.color = Color.white;
                        ResetObjectStatus();
                    }
                }
                else if(CreationManager.instance.NegociationTime<0)
                    CreationManager.instance.NegociationTime = 0;

                CanvasManager.instance.UpdateInkSlider(-75);
                break;

            case CreationManager.m_PenStatus.WANT:
                if (CreationManager.instance.ReduceNegociationTime(33))
                {
                    if (Status != ObjectStatus.WANT)
                    {
                        myButton.image.color = Color.gray;
                        WantObject();
                    }
                    else
                    {
                        myButton.image.color = Color.white;
                        ResetObjectStatus();
                    }
                }
                else if(CreationManager.instance.NegociationTime < 0)
                    CreationManager.instance.NegociationTime = 0;

                CanvasManager.instance.UpdateInkSlider(-33);
                break;

            case CreationManager.m_PenStatus.REJECT:
                if (CreationManager.instance.ReduceNegociationTime(33))
                {
                    if (Status != ObjectStatus.REJECT)
                    {
                        myButton.image.color = Color.yellow;
                        RejectObject();
                    }
                    else
                    {
                        myButton.image.color = Color.white;
                        ResetObjectStatus();
                    }
                }
                else if (CreationManager.instance.NegociationTime < 0)
                    CreationManager.instance.NegociationTime = 0;

                CanvasManager.instance.UpdateInkSlider(-33);
                break;

            case CreationManager.m_PenStatus.EXCLUDE:
                if (CreationManager.instance.ReduceNegociationTime(75))
                {
                    if (Status != ObjectStatus.EXCLUDE)
                    {
                        myButton.image.color = Color.red;
                        ExcludeObject();
                    }
                    else
                    {
                        myButton.image.color = Color.white;
                        ResetObjectStatus();
                    }
                }
                else if (CreationManager.instance.NegociationTime < 0)
                    CreationManager.instance.NegociationTime = 0;

                CanvasManager.instance.UpdateInkSlider(-75);
                break;

            default:
                ResetObjectStatus();
                CanvasManager.instance.SetInkSlider();
                break;
        }
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

    #endregion

}
