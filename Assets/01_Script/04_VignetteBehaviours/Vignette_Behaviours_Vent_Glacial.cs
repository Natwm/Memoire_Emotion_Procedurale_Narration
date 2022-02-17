using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Vent_Glacial : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.VENT_GLACIAL;

    protected string m_VignetteName = "VENT_GLACIAL";

    public override void ApplyVignetteEffect()
    {
        print("Vent_GlacialEffect");
        InventoryManager.instance.PageInventory.Clear();
    }

}
