using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Combattre : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.COMBATTRE;
    protected string m_VignetteName = "<color=#B5935A>-2<sprite=0 color=#B5935A></color=#B5935A><br>Combattre";

    public override void ApplyVignetteEffect()
    {
        print("FightEffect");
        GameManager.instance.CurrentCharacter.GetDamage(2);
    }

}
