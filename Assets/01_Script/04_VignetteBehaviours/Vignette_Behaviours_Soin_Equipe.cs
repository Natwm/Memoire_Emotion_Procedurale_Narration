using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Soin_Equipe : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.SOIN_EQUIPE;
    protected string m_VignetteName = "<color=#B5935A>(Tous) +1<sprite=0 color=#B5935A></color=#B5935A><br><size=90%>Soin de l'équipe";
    // Start is called before the first frame update


    public override void ApplyVignetteEffect()
    {
        print("SOIN_EQUIPE");
    }

}
