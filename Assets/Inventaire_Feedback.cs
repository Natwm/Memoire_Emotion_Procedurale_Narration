using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Inventaire_Feedback : MonoBehaviour
{
    SpriteRenderer render;
    Color baseCol;
    public void Start()
    {
        render = GetComponent<SpriteRenderer>();
        baseCol = new Color(render.color.r, render.color.g, render.color.b, 1);
    }

    public void PlayStockageFeedback()
    {

        render.color = baseCol;
        render.DOFade(0, 1f);
    }

    
}
