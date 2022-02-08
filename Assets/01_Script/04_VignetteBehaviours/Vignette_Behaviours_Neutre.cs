using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Neutre : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.NEUTRE;
    protected string m_VignetteName = "<br>Neutre";
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
