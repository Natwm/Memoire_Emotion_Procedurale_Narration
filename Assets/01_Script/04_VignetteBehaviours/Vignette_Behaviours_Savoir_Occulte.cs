using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Savoir_Occulte : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.SAVOIR_OCCULTE;
    protected string m_VignetteName = "<color=#B5935A>-1<sprite=2 color=#B5935A><br>+1<sprite=1 color=#B5935A></color=#B5935A><br>Savoir Occulte";
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
