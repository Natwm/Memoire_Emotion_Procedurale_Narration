using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Instantane : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.INSTANTANE;
    protected string m_VignetteName = "INSTANTANE";

    public override void ApplyVignetteEffect()
    {
        print("INSTANTANE");
    }

}
