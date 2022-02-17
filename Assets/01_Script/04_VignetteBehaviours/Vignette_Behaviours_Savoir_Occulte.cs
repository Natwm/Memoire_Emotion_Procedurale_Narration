using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Savoir_Occulte : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.SAVOIR_OCCULTE;
    protected string m_VignetteName = "<color=#B5935A>-1<sprite=2 color=#B5935A><br>+1<sprite=1 color=#B5935A></color=#B5935A><br>Savoir Occulte";
    // Start is called before the first frame update

    public override void ApplyVignetteEffect()
    {
        print("Savoir_OcculteEffect");
        for (int i = 0; i < 2; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, ObjectManager.instance._OccultsPullOfObject.Count);

            //InventoryManager.instance.PageInventory.Add(new UsableObject(LevelManager.instance.UnlockableObject[randomIndex]));
        }
    }

}
