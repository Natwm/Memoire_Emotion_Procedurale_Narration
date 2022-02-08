using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaitingCharacterPanel : MonoBehaviour
{

    [Header("Image")]
    [SerializeField] private Image characterRender;

    [Space]
    [Header("Panel")]
    [SerializeField] private GameObject textPanel;

    [Space]
    [Header("Text")]
    [SerializeField] private TMP_Text lifeText;
    [SerializeField] private TMP_Text mentalLifeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpUI(Character player)
    {
        lifeText.text = "<sprite=0> " + player.Life.ToString();
        mentalLifeText.text = "<sprite=2> " + player.MentalHealth.ToString();

        characterRender.sprite = player.AssignedElement.Render;
    }
}
