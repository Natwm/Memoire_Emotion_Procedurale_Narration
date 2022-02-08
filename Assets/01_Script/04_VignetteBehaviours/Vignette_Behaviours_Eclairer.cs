using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Eclairer : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.ECLAIRER;
    protected string m_VignetteName = "<color=#B5935A>+2<sprite=1 color=#B5935A></color=#B5935A><br><size=100%>Éclairer";
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
