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
    public TMP_Text LifeText { get => lifeText; set => lifeText = value; }
    public TMP_Text MentalLifeText { get => mentalLifeText; set => mentalLifeText = value; }
    public TMP_Text NameText { get => nameText; set => nameText = value; }

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
        ClearInventoryUI();
        LifeText.text = PlayerManager.instance.Health.ToString();
        MentalLifeText.text = PlayerManager.instance.MentalHealth.ToString();
        NameText.text = PlayerManager.instance.CharacterData.CharacterName;

        characterRender.sprite = PlayerManager.instance.CharacterData.Render;

        SetUpInventoryUI(PlayerManager.instance.InventoryObj);
    }

    void SetUpInventoryUI(List<UsableObject> listOfObject)
    {
        foreach (var item in listOfObject)
        {
            GameObject inventoryElt = Instantiate(inventoryObject, InventoryPanel.transform);
            inventoryElt.GetComponent<Image>().sprite = item.Data.Sprite;
            if (item.IsCurse)
                inventoryElt.GetComponent<Image>().color = new Color(104, 46, 68, 255);
        }
    }

    void ClearInventoryUI()
    {
        for (int i = 0; i < InventoryPanel.transform.childCount; i++)
        {
            Destroy(InventoryPanel.transform.GetChild(i).gameObject);
        }
    }
}
