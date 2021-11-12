using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenObject : MonoBehaviour
{
    public void SelectedButton()
    {
        if(GetComponent<UnityEngine.UI.Image>().color != Color.red)
            GetComponent<UnityEngine.UI.Image>().color = Color.red;
        else
            GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }
}
