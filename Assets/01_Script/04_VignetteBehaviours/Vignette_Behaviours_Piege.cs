using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Piege : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.PIEGE;
    protected string m_VignetteName = "<color=#B5935A>-1<sprite=0 color=#B5935A></color=#B5935A><br>Piège";
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
