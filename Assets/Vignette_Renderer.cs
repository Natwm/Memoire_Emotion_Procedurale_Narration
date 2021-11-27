using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vignette_Renderer : MonoBehaviour
{

    // Order Object 
    public Color[] TestColors;
    public GameObject testVignette;
    public string testString;
    public Color[] vignetteColors;
    public GameObject[] Reveals;
    public Material LightEffect;
    // Start is called before the first frame update

    //XX_@_#_NAME
    void Start()
    {
        CreateVignette(testString, testVignette, TestColors[0]);
    }

    char[] separator = { '_' };
    public void OrderObject(GameObject vignette)
    {
        for (int e = 0; e < 5; e++)
        {
        for (int i = 0; i < vignette.transform.childCount; i++)
        {
            //NuméroDelaVignette
            string LayerObjName = vignette.transform.GetChild(i).name;
            string[] P_name = LayerObjName.Split(separator);
            GameObject Layer;
            if (LayerObjName.Contains("@"))
            {
                Layer = vignette.transform.GetChild(i).gameObject;
                for (int n = 0; n < vignette.transform.childCount; n++)
                {

                    if (vignette.transform.GetChild(n).gameObject != Layer)
                    {
                        GameObject subObject = vignette.transform.GetChild(n).gameObject;
                        string[] subObjectName = subObject.name.Split(separator);
                        if (subObjectName[0] == P_name[0])
                        {
                            subObject.transform.parent = Layer.transform;
                            
                        }
                    }
                }
                
            }
        }
        }
    }
   public void SetToMask(GameObject vignette, string compType)
    {
        GameObject Layer = vignette.transform.GetChild(0).gameObject;
        GameObject Mask;
        for (int i = 0; i < Layer.transform.childCount; i++)
        {
            if (Layer.transform.GetChild(i).name.Contains(compType))
            {
                Mask = Layer.transform.GetChild(i).gameObject;
                Mask.AddComponent<SpriteMask>().sprite = Mask.GetComponent<SpriteRenderer>().sprite;
                Mask.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                Layer.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        for (int n = 0; n < vignette.transform.childCount; n++)
        {
            GameObject layer = vignette.transform.GetChild(n).gameObject;
            layer.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            if (layer.transform.childCount > 0)
            {
                for (int l= 0; l < layer.transform.childCount; l++)
                {
                    layer.transform.GetChild(l).GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                }
            }
        }

    }
    public void RandomiseVignette(GameObject vignette)
    {
        for (int i = 0; i < vignette.transform.childCount; i++)
        {
            GameObject layer = vignette.transform.GetChild(i).gameObject;
            if (layer.name.Contains("#"))
            {
                int RandomiseDisposition = Random.Range(0,layer.transform.childCount);
                for (int n = 0; n < layer.transform.childCount; n++)
                {
                    if (n != RandomiseDisposition)
                    {
                        layer.transform.GetChild(n).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void SetReveal(string vignetteType, GameObject vignette)
    {
        GameObject RevealToUse=null;
        switch (vignetteType)
        {
            case "1x1":
            {
                    RevealToUse = Reveals[0];
                    break;
            }
            case "1x2":
            {
                    RevealToUse = Reveals[1];
                    break;
            }
            case "2x1":
            {
                    RevealToUse = Reveals[2];
                    break;
            }
            case "2x2":
            {
                RevealToUse = Reveals[3];
                break;
            }
        }
        GameObject tempRev = Instantiate(RevealToUse);
        tempRev.transform.parent = vignette.transform;
        tempRev.transform.SetAsFirstSibling();
        tempRev.transform.localPosition = Vector3.zero;

    }

    public void Colorize(GameObject vignette, Color characolor)
    {
        for (int i = 0; i < vignette.transform.childCount; i++)
        {
            GameObject Layer = vignette.transform.GetChild(i).gameObject;
            switch (Layer.name)
            {
                case string a when a.Contains("FRONT_BOTTOM"):
                {
                        for (int n = 0; n < Layer.transform.childCount; n++)
                        {
                            GameObject child = Layer.transform.GetChild(n).gameObject;
                            child.GetComponent<SpriteRenderer>().color = vignetteColors[1];
                        }
                        break;
                }
                case string a when a.Contains("FRONT_TOP"):
                {
                        for (int n = 0; n < Layer.transform.childCount; n++)
                        {
                            GameObject child = Layer.transform.GetChild(n).gameObject;
                            child.GetComponent<SpriteRenderer>().color = vignetteColors[0];
                        }
                        break;
                }
                case string a when a.Contains("CHARA"):
                {
                        for (int n = 0; n < Layer.transform.childCount; n++)
                        {
                            GameObject child = Layer.transform.GetChild(n).gameObject;
                            child.GetComponent<SpriteRenderer>().color = characolor;
                        }
                        
                        break;
                }
                case string a when a.Contains("ACTION"):
                {
                        for (int n = 0; n < Layer.transform.childCount; n++)
                        {
                            GameObject child = Layer.transform.GetChild(n).gameObject;
                            child.GetComponent<SpriteRenderer>().color = vignetteColors[1];
                        }
                        break;
                }
                case string a when a.Contains("WINDOWS"):
                {
                        for (int n = 0; n < Layer.transform.childCount; n++)
                        {
                            GameObject child = Layer.transform.GetChild(n).gameObject;
                            child.GetComponent<SpriteRenderer>().color = vignetteColors[2];
                        }
                        break;
                }
                case string a when a.Contains("BGOBJECTS"):
                {
                        for (int n = 0; n < Layer.transform.childCount; n++)
                        {
                            GameObject child = Layer.transform.GetChild(n).gameObject;
                            child.GetComponent<SpriteRenderer>().color = vignetteColors[2];
                        }
                        break;
                }
                case string a when a.Contains("LIGHTEFFECT"):
                {
                        for (int n = 0; n < Layer.transform.childCount; n++)
                        {
                            GameObject child = Layer.transform.GetChild(n).gameObject;
                            child.GetComponent<SpriteRenderer>().material = LightEffect;
                            child.GetComponent<SpriteRenderer>().color = vignetteColors[3];
                        }
                        break;
                }
                case string a when a.Contains("BGPAINT"):
                {
                        Layer.GetComponent<SpriteRenderer>().color = vignetteColors[2];
                        break;
                }
            }

            /* switch (Layer.name)
             {
                 case  "01_0FRONT_BOTTOM_@_#" :
                 {
                         for (int n = 0; n < Layer.transform.childCount; n++)
                         {
                             GameObject child = Layer.transform.GetChild(n).gameObject;
                             child.GetComponent<SpriteRenderer>().color = vignetteColors[1];
                         }
                 break;
                 }
                 case "03_0FRONT_TOP_@_#":
                     {
                         for (int n = 0; n < Layer.transform.childCount; n++)
                         {
                             GameObject child = Layer.transform.GetChild(n).gameObject;
                             child.GetComponent<SpriteRenderer>().color = vignetteColors[0];
                         }
                         break;
                     }
                 case "02_0CHARA_@_#":
                 {
                         Layer.GetComponent<SpriteRenderer>().color = vignetteColors[2];
                         break;
                 }
                 case "04_0ACTION_@_#":
                 {
                         for (int n = 0; n < Layer.transform.childCount; n++)
                         {
                             GameObject child = Layer.transform.GetChild(n).gameObject;
                             child.GetComponent<SpriteRenderer>().color = vignetteColors[1];
                         }
                         break;
                 }
                 case "05_0WINDOWS_@_#":
                 {
                         for (int n = 0; n < Layer.transform.childCount; n++)
                         {
                             GameObject child = Layer.transform.GetChild(n).gameObject;
                             child.GetComponent<SpriteRenderer>().color = vignetteColors[2];
                         }
                         break;
                 }
                 case "06_0BGOBJECTS_@_#":
                 {
                         for (int n = 0; n < Layer.transform.childCount; n++)
                         {
                             GameObject child = Layer.transform.GetChild(n).gameObject;
                             child.GetComponent<SpriteRenderer>().color = vignetteColors[2];
                         }
                         break;
                 }
                 case "021_021LIGHTEFFECT_@":
                 {
                         for (int n = 0; n < Layer.transform.childCount; n++)
                         {
                             GameObject child = Layer.transform.GetChild(n).gameObject;
                             child.GetComponent<SpriteRenderer>().material = LightEffect;
                             child.GetComponent<SpriteRenderer>().color = vignetteColors[3];
                         }
                         break;
                 }
                 case "07_0BGPAINT_@":
                     {
                         Layer.GetComponent<SpriteRenderer>().color = vignetteColors[2];
                         break;
                     }
             }*/
        }

    }
    
    public void CreateVignette(string type,GameObject vignette,Color characterColor)
    {
        GameObject tempVignette = Instantiate(vignette);
        tempVignette.SetActive(true);
        tempVignette.transform.position = Vector3.zero;
        OrderObject(tempVignette);
        RandomiseVignette(tempVignette);
        Colorize(tempVignette,characterColor);
        SetToMask(tempVignette,type);
        CleanObject(testVignette);
        SetReveal(type, tempVignette);

    }

    void CleanObject(GameObject vignette)
    {
        List<GameObject> ToDestroy = new List<GameObject>();
        for (int i = 0; i < vignette.transform.childCount; i++)
        {
            if (vignette.transform.GetChild(i).childCount > 0)
            {
                GameObject Layer = vignette.transform.GetChild(i).gameObject;
                for (int e = 0; e < Layer.transform.childCount; e++)
                {
                    if (!Layer.transform.GetChild(e).gameObject.activeInHierarchy)
                    {
                        ToDestroy.Add(Layer.transform.GetChild(e).gameObject);
                    }
                }
            }
        }
        foreach (GameObject item in ToDestroy)
        {
            Destroy(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CreateVignette(testString, testVignette,TestColors[2]);
        }
    }
}
