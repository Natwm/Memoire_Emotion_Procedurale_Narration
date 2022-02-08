using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Ui_Feedback : MonoBehaviour
{
    Animator CharacterAnimation;
    Image CharacterPortrait;
    Color baseCol;
    public void Lose_Health()
    {
        CharacterAnimation.SetTrigger("Hit_Health"); 
        

    }

    public void Lose_Sanity()
    {
        CharacterAnimation.SetTrigger("Hit_Sanity");

    }

    public void Heal_Health()
    {
        CharacterAnimation.SetTrigger("Heal_Life");
    }

    public void Heal_Sanity()
    {
        CharacterAnimation.SetTrigger("Heal_Sanity");
    }



    // Start is called before the first frame update
    void Start()
    {
        CharacterAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
