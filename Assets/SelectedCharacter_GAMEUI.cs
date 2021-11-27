using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedCharacter_GAMEUI : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image characterRender;

    [Space]
    [Header("Panel")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject textPanel;

    [Space]
    [Header("Text")]
    [SerializeField] private TMP_Text lifeText;
    [SerializeField] private TMP_Text mentalLifeText;
    [SerializeField] private TMP_Text nameText;

    [Space]
    [Header("Prefabs")]
    [SerializeField] private GameObject inventoryObject;

    public GameObject InventoryPanel { get => inventoryPanel; set => inventoryPanel = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpUI()
    {
        lifeText.text = PlayerManager.instance.Health.ToString();
        mentalLifeText.text = PlayerManager.instance.MentalHealth.ToString();
        nameText.text = PlayerManager.instance.CharacterData.CharacterName;

        characterRender.sprite = PlayerManager.instance.CharacterData.Render;

        SetUpInventoryUI(PlayerManager.instance.Inventory);
    }

    void SetUpInventoryUI(List<Object_SO> listOfObject)
    {
        foreach (var item in listOfObject)
        {
            GameObject inventoryElt = Instantiate(inventoryObject, InventoryPanel.transform);
            inventoryElt.GetComponent<Image>().sprite = item.Sprite;
        }
    }
}
