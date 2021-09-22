using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face_Manager : MonoBehaviour
{
    public Sprite[] allEyebrows;
    public Sprite[] allMouths;
    public SpriteRenderer[] allEyebrowsRenderer;
    public SpriteRenderer mouthRenderer;
    
    int LeftEyebrowIndex=-1;
    int RightEyebrowIndex=-1;

    public Color[] emotionColors;
    public SpriteRenderer faceRenderer;

    DemoScript inputManager;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<DemoScript>();
    }

    public void UpdateMouth(int mouthIndex)
    {
        mouthRenderer.sprite = allMouths[mouthIndex];
        faceRenderer.color = emotionColors[mouthIndex];
    }

    public void UpdateFaceColorAmount(float triggerAmount)
    {
        float alphaAmount = Mathf.Clamp(triggerAmount,0f,0.8f) + 0.1f;
        faceRenderer.color = new Color(faceRenderer.color.r, faceRenderer.color.g, faceRenderer.color.b,alphaAmount);
    }

    // Changement du sprite correspondant.
    public void UpdateEyebrow(int direction)
    {
        //Sprite de gauche
        if (direction == 0)
        {
            LeftEyebrowIndex = UpdateEyebrowIndex(LeftEyebrowIndex);
            allEyebrowsRenderer[0].sprite = allEyebrows[LeftEyebrowIndex];
        }
        //Sprite de droite
        else
        {
            RightEyebrowIndex = UpdateEyebrowIndex(RightEyebrowIndex);
            allEyebrowsRenderer[1].sprite = allEyebrows[RightEyebrowIndex];
        }
        Debug.Log("direction : " + direction + " " + "Left : " + LeftEyebrowIndex + " Right : " + RightEyebrowIndex);
    }
   
    // Update de l'index du sourcil
    int UpdateEyebrowIndex(int index)
    {
        int tempindex = index;
        if (tempindex < allEyebrows.Length-1)
        {
            tempindex++;
            return tempindex;
        }
        else
        {
            Debug.Log("Reset");
            tempindex = 0;
            return tempindex;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFaceColorAmount(inputManager.intensityValue);
    }
}
