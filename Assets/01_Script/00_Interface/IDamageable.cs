using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    void GetDamage(int amountOfDamage);
    void Death();
    bool IsDead();
}
