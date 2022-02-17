using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Neutre : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.NEUTRE;
    protected string m_VignetteName = "<br>Neutre";
    

    public override void ApplyVignetteEffect()
    {
        print("neutre");
    }

}
