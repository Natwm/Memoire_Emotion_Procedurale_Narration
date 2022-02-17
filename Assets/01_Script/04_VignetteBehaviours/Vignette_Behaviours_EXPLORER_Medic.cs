using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_EXPLORER_Medic : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.EXPLORER_MEDIC;
    protected string m_VignetteName = "<color=#B5935A>+1<sprite=1 color=#B5935A></color=#B5935A><br><size=100%>Fouiller l'armoire à pharmacie";
    // Start is called before the first frame update

    public override void ApplyVignetteEffect()
    {
        print("EXPLORER_MEDIC");

        //SoundManager.instance.PlaySound_GainObject();

        int randomIndex = Random.Range(0, ObjectManager.instance._HealPullOfObject.Count);
        UsableObject_SO newItem = ObjectManager.instance._HealPullOfObject[randomIndex];

        GameObject item = CanvasManager.instance.NewItemInLevelInventory(newItem);

        InventoryManager.instance.PageInventory.Add(item.GetComponent<UsableObject>());

        if (InventoryManager.instance.PageInventory.Count == InventoryManager.instance.amoutOfObjectBeforeTake)
            TakeEffect();

        CanvasManager.instance.SetUpLevelIndicator();
    }

}
