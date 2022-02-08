using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Ressemblance_Etrange : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.RESSEMBLACE_ETRANGE;
    protected string m_VignetteName = "RESSEMBLANCE_ETRANGE";
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
