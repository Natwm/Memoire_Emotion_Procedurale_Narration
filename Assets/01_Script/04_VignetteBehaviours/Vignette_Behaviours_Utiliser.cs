using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Utiliser : Vignette_Behaviours
{

    protected VignetteCategories initCategorie = VignetteCategories.UTILISER;

    protected string m_VignetteName = "<br>Utiliser";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ApplyVignetteEffect()
    {
        print('o');
    }

}
