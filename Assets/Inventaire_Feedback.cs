using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Inventaire_Feedback : MonoBehaviour
{
    Image render;
    Color baseCol;
    public void Start()
    {
        render = GetComponent<Image>();
        baseCol = new Color(render.color.r, render.color.g, render.color.b, 1);
    }

    public void PlayStockageFeedback()
    {

        render.color = baseCol;
        render.DOFade(0, 1f);
    }

    
}
