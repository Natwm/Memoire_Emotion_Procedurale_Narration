using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_EXPLORER_occult : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.EXPLORER_OCCULT;
    protected string m_VignetteName = "<color=#B5935A>+1<sprite=1 color=#B5935A></color=#B5935A><br><size=100%>Explorer";


    public override void ApplyVignetteEffect()
    {
        print("ExploreEffect");

        //SoundManager.instance.PlaySound_GainObject();

        int randomIndex = UnityEngine.Random.Range(0, ObjectManager.instance._OccultsPullOfObject.Count);
        UsableObject_SO newItem = ObjectManager.instance._OccultsPullOfObject[randomIndex];

        GameObject item = CanvasManager.instance.NewItemInLevelInventory(newItem);

        InventoryManager.instance.PageInventory.Add(item.GetComponent<UsableObject>());

        if (InventoryManager.instance.PageInventory.Count == InventoryManager.instance.amoutOfObjectBeforeTake)
            TakeEffect();

        CanvasManager.instance.SetUpLevelIndicator();
    }

}
