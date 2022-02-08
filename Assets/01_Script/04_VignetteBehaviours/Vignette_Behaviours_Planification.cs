using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Planification : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.PLANIFICATION;
    protected string m_VignetteName = "<color=#B5935A>+30<sprite=5 color=#B5935A></color=#B5935A><br><size=90%>Planification";

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
