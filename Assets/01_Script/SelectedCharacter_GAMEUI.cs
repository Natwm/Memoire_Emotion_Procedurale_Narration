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

    [Space]
    [Header("Prefabs")]
    [SerializeField] private Animator feedBackAnimator;

    public GameObject InventoryPanel { get => inventoryPanel; set => inventoryPanel = value; }
    public TMP_Text LifeText { get => lifeText; set => lifeText = value; }
    public TMP_Text MentalLifeText { get => mentalLifeText; set => mentalLifeText = value; }
    public TMP_Text NameText { get => nameText; set => nameText = value; }
    public Animator FeedBackAnimator { get => feedBackAnimator; set => feedBackAnimator = value; }

    // Start is called before the first frame update
    void Start()
    {
        //SetUpUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpUI()
    {
        ClearInventoryUI();
        LifeText.text = "<sprite=0> " + GameManager.instance.CurrentCharacter.Life.ToString();
        MentalLifeText.text = "<sprite=2> " + GameManager.instance.CurrentCharacter.MentalHealth.ToString();
        NameText.text = GameManager.instance.CurrentCharacter.AssignedElement.CharacterName;

        characterRender.sprite = GameManager.instance.CurrentCharacter.AssignedElement.Render;

        SetUpInventoryUI(GameManager.instance.CurrentCharacter.InventoryObj);
    }

    void SetUpInventoryUI(List<UsableObject> listOfObject)
    {
        foreach (var item in listOfObject)
        {
            GameObject inventoryElt = Instantiate(inventoryObject, InventoryPanel.transform);
            inventoryElt.GetComponent<Image>().sprite = item.Data.Sprite;

            inventoryElt.GetComponent<Button>().onClick.AddListener(delegate
            {
                LevelManager.instance.SpawnObject(item.Data.DrawParam);
                GameManager.instance.CurrentCharacter.InventoryObj.Remove(item);
                //GameManager.instance.CurrentCharacter.CharacterContener.InventoryObj.Remove(item);
                //item.Data.PlaySound();

                item.AmountOfUse--;

                if(item.AmountOfUse <= 0)
                {
                    item.gameObject.SetActive(false);
                    inventoryElt.gameObject.SetActive(false);
                    //Destroy(item.gameObject);
                    //Destroy(inventoryElt.gameObject);
                }
                
            });

            if (item.IsCurse)
            {
                inventoryElt.GetComponent<Image>().color = new Color32(104, 46, 68, 255);
            }
                
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
