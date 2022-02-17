using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Debrouillard : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.DEBROUILLARD;
    protected string m_VignetteName = "<color=#B5935A>Utilisable sur toute les cases</color=#B5935A><br><size=90%>Débrouillard";


    public override void ApplyVignetteEffect()
    {
        print("DEBROUILLARD");
    }

}
