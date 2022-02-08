using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Explorer : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.EXPLORER;
    protected string m_VignetteName = "<color=#B5935A>+1<sprite=1 color=#B5935A></color=#B5935A><br><size=100%>Explorer";
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
