﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_Behaviours : abstractUsableObject
{
    [SerializeField] private CaseContener_SO caseEffects;

    public CaseContener_SO CaseEffects { get => caseEffects; set => caseEffects = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    #region Abstract
    public override void ClaimObject()
    {
        throw new System.NotImplementedException();
    }

    public override void DropObject()
    {
        throw new System.NotImplementedException();
    }

    public override void ExcludeObject()
    {
        throw new System.NotImplementedException();
    }

    public override void PickUpObject()
    {
        throw new System.NotImplementedException();
    }

    public override void RejectObject()
    {
        throw new System.NotImplementedException();
    }

    public override void ResetObjectStatus()
    {
        throw new System.NotImplementedException();
    }

    public override void UseObject()
    {
        throw new System.NotImplementedException();
    }

    public override void WantObject()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}