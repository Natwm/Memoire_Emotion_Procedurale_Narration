using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterReader : MonoBehaviour
{
    public Character assignedCharacter;
    TMP_Text CharacterName;
    TMP_Text CharacterJauge;
    GameObject CharacterFace;
    GameObject CharacterIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitialiseUi()
    {
        CharacterName = transform.GetChild(0).GetComponent<TMP_Text>();
        CharacterJauge = transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        CharacterIcon = transform.GetChild(2).gameObject;
    }

    public void ReadCharacter()
    {
        CharacterName.text = assignedCharacter.characterName;
        CharacterJauge.text = MakeJauge();
        CharacterFace = assignedCharacter.CreateFace(gameObject);
        SetIconToFace();
        
        
    }

    public void SetIconToFace()
    {
        CharacterIcon.GetComponent<Image>().sprite = CharacterFace.GetComponent<SpriteRenderer>().sprite;
        CharacterIcon.GetComponent<Image>().color = assignedCharacter.characterColor;
       
        //Emotion
        //A remplacer par variable Sprite CurrentEmotionSprite
        CharacterIcon.transform.GetChild(0).GetComponent<Image>().sprite = CharacterFace.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;
        CharacterIcon.transform.GetChild(0).GetComponent<Image>().color = assignedCharacter.characterColor;

        //FaceFeature
        CharacterIcon.transform.GetChild(1).GetComponent<Image>().sprite = assignedCharacter.faceFeature;
        
    }

    public string MakeJauge()
    {
        return assignedCharacter.currentJauge.ToString() + " : " + assignedCharacter.jaugeNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            
            ReadCharacter();
        }
    }
}
