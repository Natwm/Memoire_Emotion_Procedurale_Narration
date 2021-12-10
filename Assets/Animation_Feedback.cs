using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Feedback : MonoBehaviour
{
    string size_trigger;
    string transformation_trigger;
    Animator AnimationPlayer;
    Animator TransformationPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        AnimationPlayer = GetComponent<Animator>();
        TransformationPlayer = transform.GetChild(0).GetComponent<Animator>();
        transform.GetChild(0).gameObject.SetActive(true);
        GetSizeAndColor();

    }

    public void GetSizeAndColor()
    {
        foreach (Transform item in transform.parent)
        {
            if (item.name.Contains("Top Sprite"))
            {
                GetComponent<SpriteRenderer>().color = item.GetComponent<SpriteRenderer>().color;
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = item.GetComponent<SpriteRenderer>().color;
            }
        }
        switch (transform.parent.name)
        {
            case string a when a.Contains("1x1"):
                {
                    size_trigger = "1x1";
                    transformation_trigger = "1x1_t";
                    break;
                }
            case string a when a.Contains("1x2"):
                {
                    size_trigger = "1x2";
                    transformation_trigger = "1x2_t";
                    break;
                }
            case string a when a.Contains("2x1"):
                {
                    size_trigger = "2x1";
                    transformation_trigger = "2x1_t";
                    break;
                }
            case string a when a.Contains("2x2"):
                {
                    size_trigger = "2x2";
                    transformation_trigger = "2x2_t";
                    break;
                }
            default:
                break;
        }
    }

    public void PlayLock()
    {
        AnimationPlayer.SetTrigger(size_trigger);
    }

    public void PlayTransformation()
    {
        TransformationPlayer.SetTrigger(transformation_trigger);
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            PlayLock();
            PlayTransformation();
        }
    }

}
