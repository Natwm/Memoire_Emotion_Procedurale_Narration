using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Prendre : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.PRENDRE;
    protected string m_VignetteName = "<color=#B5935A><sprite=3 color=#B5935A></color=#B5935A><br>Prendre";


    public override void ApplyVignetteEffect()
    {
        print("Take Effect off : " + InventoryManager.instance.PageInventory.Count + " Item");
        foreach (var item in InventoryManager.instance.PageInventory)
        {
            //CreationManager.instance.GlobalInventory.Add(item);
            //item.transform.parent = CreationManager.instance.pulledObject.transform;
            item.gameObject.SetActive(false);
        }

        InventoryManager.instance.PageInventory.Clear();
        CanvasManager.instance.SetUpLevelIndicator();
        //GameObject.Find("Feedback_Stockage").GetComponent<Inventaire_Feedback>().PlayStockageFeedback();
    }

}
