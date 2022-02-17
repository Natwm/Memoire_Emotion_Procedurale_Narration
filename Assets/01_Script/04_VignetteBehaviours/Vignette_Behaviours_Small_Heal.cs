using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Small_Heal : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.SMALL_HEAL;
    protected string m_VignetteName = "<color=#B5935A>+1<sprite=0 color=#B5935A></color=#B5935A><br>Soin Léger";
    // Start is called before the first frame update


    public override void ApplyVignetteEffect()
    {
        print("SmallHealEffect");
        GameManager.instance.CurrentCharacter.HealPlayer(1);
    }

}
