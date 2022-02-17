using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Explorer : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.EXPLORER;
    protected string m_VignetteName = "<color=#B5935A>+1<sprite=1 color=#B5935A></color=#B5935A><br><size=100%>Explorer";

    public override void ApplyVignetteEffect()
    {
        print("EXPLORER");

        //SoundManager.instance.PlaySound_GainObject();

        int randomIndex = Random.Range(0, ObjectManager.instance._BasisPullOfObject.Count);
        UsableObject_SO newItem = ObjectManager.instance._BasisPullOfObject[randomIndex];

        GameObject item = CanvasManager.instance.NewItemInLevelInventory(newItem);
        item.GetComponent<UsableObject>().Data = newItem;

        InventoryManager.instance.PageInventory.Add(item.GetComponent<UsableObject>());

        /*if (InventoryManager.instance.PageInventory.Count == InventoryManager.instance.amoutOfObjectBeforeTake)
            TakeEffect();*/

        CanvasManager.instance.SetUpLevelIndicator();
    }

}
