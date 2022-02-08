using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Big_Heal : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.BIG_HEAL;
    protected string m_VignetteName = "<color=#B5935A>+1<sprite=0 color=#B5935A></color=#B5935A><br>Soin Léger";
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
