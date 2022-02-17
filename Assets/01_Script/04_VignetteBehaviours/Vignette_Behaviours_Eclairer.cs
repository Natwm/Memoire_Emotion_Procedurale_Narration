using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Eclairer : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.ECLAIRER;
    protected string m_VignetteName = "<color=#B5935A>+2<sprite=1 color=#B5935A></color=#B5935A><br><size=100%>Éclairer";


    public override void ApplyVignetteEffect()
    {
        print("Trouve deux objet alea");
        for (int i = 0; i < 2; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, ObjectManager.instance._BasisPullOfObject.Count);

            //InventoryManager.instance.PageInventory.Add(new UsableObject(LevelManager.instance.UnlockableObject[randomIndex]));
        }
    }

}
