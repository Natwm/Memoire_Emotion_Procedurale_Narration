using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Vent_Glacial : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.VENT_GLACIAL;

    protected string m_VignetteName = "VENT_GLACIAL";

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
