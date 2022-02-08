using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette_Behaviours_Prendre : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.PRENDRE;
    protected string m_VignetteName = "<color=#B5935A><sprite=3 color=#B5935A></color=#B5935A><br>Prendre";
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
