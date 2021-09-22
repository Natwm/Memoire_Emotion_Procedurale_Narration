using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face_Manager : MonoBehaviour
{
    public Sprite[] allEyebrows;
    public SpriteRenderer[] allEyebrowsRenderer;
    int LeftEyebrowIndex=-1;
    int RightEyebrowIndex=-1;
    // Start is called before the first frame update
    void Start()
    {
        
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
        
    }
}
