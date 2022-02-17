using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Souffler : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.SOUFFLER;
    protected string m_VignetteName = "<color=#B5935A>+ 1<sprite=2 color=#B5935A></color=#B5935A><br><size=100%>Bref Répit";

    public override void ApplyVignetteEffect()
    {
        GameManager.instance.CurrentCharacter.HealMentalPlayer(1);
    }

}
