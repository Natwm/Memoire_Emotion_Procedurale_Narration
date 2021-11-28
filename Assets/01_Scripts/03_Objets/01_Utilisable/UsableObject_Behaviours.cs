using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableObject_Behaviours : /*UsableObject,*/ IDamageable
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()

    {
        
    }

    #region Interfaces
    public void GetDamage(int amountOfDamage)
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public bool IsDead()
    {
        throw new System.NotImplementedException();
    }

    #endregion

   
}
