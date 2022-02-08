using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Debrouillard : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.DEBROUILLARD;
    protected string m_VignetteName = "<color=#B5935A>Utilisable sur toute les cases</color=#B5935A><br><size=90%>Débrouillard";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ApplyVignetteEffect()
    {
        print('o');
    }

}
