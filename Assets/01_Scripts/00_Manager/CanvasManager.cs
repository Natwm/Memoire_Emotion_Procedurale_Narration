using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [Header("Panel")]
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LoosePanel;

    [Space]

    public TMP_Text logText;

    [SerializeField] private TMP_Text lifeText;
    [SerializeField] private TMP_Text staminaText;
    [SerializeField] private TMP_Text vignetteText;

    [Space]
    [Header("Button")]
    [SerializeField] private Button moveButton;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : CanvasManager");
        else
            instance = this;
    }
    private void Start()
    {
        SetActiveMoveButton(false);
    }

    public void SetActiveMoveButton(bool activeObject)
    {
        if (activeObject)
            moveButton.gameObject.GetComponent<Image>().color = Color.green;
        else
            moveButton.gameObject.GetComponent<Image>().color = Color.red;

        moveButton.interactable = activeObject;
    }


    public void NewLogEntry(string content)
    {
        logText.text = content;
    }

    public void UpdateInformationText(int life, int stamina, int vignette)
    {
        UpdateLifePoint(life);
        UpdateStaminaPoint(stamina);
        UpdateVignetteToDraw(vignette);
    }

    public void UpdateLifePoint(int life)
    {
        lifeText.text = "Life : " + life;
    }
    public void UpdateStaminaPoint(int stamina)
    {
        staminaText.text = "Stamina : " + stamina;
    }

    public void UpdateVignetteToDraw(int amoutOfVignette)
    {
        vignetteText.text = "Next Vignette : " + amoutOfVignette;
    }

    public void PlayerWinTheGame()
    {
        WinPanel.SetActive(true);
    }

    public void PlayerLooseTheGame()
    {
        LoosePanel.SetActive(true);
    }

}
