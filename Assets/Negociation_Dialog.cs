using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Negociation_Dialog : MonoBehaviour
{
    private TMP_Text m_textMeshPro;

    public Dialog_SO Prendre_SO;
    public Dialog_SO Demander_SO;
    public Dialog_SO Decliner_SO;
    public Dialog_SO Refuser_SO;
    [Space]
    public string testString;

    public Character_SO[] charaTest;

    Dialog CurrentDialog;
    string Action;
    string Answer;

    Color Chara1;
    Color Chara2;

    public static Negociation_Dialog instance;

    void Awake()
    {
        // Get Reference to TextMeshPro Component
        m_textMeshPro = GetComponent<TMP_Text>();
        instance = this;
        m_textMeshPro.enableWordWrapping = true;
        m_textMeshPro.alignment = TextAlignmentOptions.Top;

    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            GetDialog(Prendre_SO);
            GetCharacters(charaTest[3], charaTest);
            Action = SwapObject(CurrentDialog.Action_Phrase, testString);
            StartCoroutine(ShowActionLine(CurrentDialog));
        }
    }

    public void StartDialog(Dialog_SO action,Character_SO firstCharacter,Character_SO[] allCharacter,string objet)
    {
        GetDialog(action);
        GetCharacters(firstCharacter, allCharacter);
        Action = SwapObject(CurrentDialog.Action_Phrase, objet);
        StartCoroutine(ShowActionLine(CurrentDialog));
    }

    public string SwapObject(string _toReplace,string by)
    {
        return _toReplace.Replace("_", by);
    }

    public void GetDialog(Dialog_SO _dialog)
    {
        CurrentDialog = _dialog.GetDialog();
    }

    public void GetCharacters(Character_SO firstCharacter,Character_SO[] _Allcharacters)
    {
        Chara1 = firstCharacter.Color;
        for (int i = 0; i < _Allcharacters.Length; i++)
        {
            if (_Allcharacters[i] == firstCharacter)
            {
                if (i == _Allcharacters.Length-1)
                {
                    Chara2 = _Allcharacters[0].Color;
                }
                else
                {
                    Chara2 = _Allcharacters[i+1].Color;
                }
            }
        }
    }

    IEnumerator ShowAnswerLine(Dialog _dialog)
    {

        m_textMeshPro.text = _dialog.GetAnswer();
        // Force and update of the mesh to get valid information.
        m_textMeshPro.ForceMeshUpdate();
        m_textMeshPro.color = Chara2;
        bool stop = false;
        int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount; // Get # of Visible Character in text object
        int counter = 0;
        int visibleCount = 0;

        while (!stop)
        {
            visibleCount = counter % (totalVisibleCharacters + 1);
            m_textMeshPro.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?
            // Once the last character has been revealed, wait 1.0 second and start over.
            if (visibleCount >= totalVisibleCharacters)
            {

                stop = true;
                yield return new WaitForSeconds(2f);
                m_textMeshPro.text = "";

            }

            counter += 1;

            yield return new WaitForSeconds(0.05f);
        }


    }

    IEnumerator ShowActionLine(Dialog _dialog)
    {

        m_textMeshPro.text = Action;
        // Force and update of the mesh to get valid information.
        m_textMeshPro.ForceMeshUpdate();
        m_textMeshPro.color = Chara1;
        bool stop=false;
        int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount; // Get # of Visible Character in text object
        int counter = 0;
        int visibleCount = 0;

        while (!stop)
        {
            visibleCount = counter % (totalVisibleCharacters + 1);
            m_textMeshPro.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?
            // Once the last character has been revealed, wait 1.0 second and start over.
            if (visibleCount >= totalVisibleCharacters)
            {

                stop = true;
                yield return new WaitForSeconds(2f);
                StartCoroutine(ShowAnswerLine(_dialog));
            }

            counter += 1;

            yield return new WaitForSeconds(0.05f);
        }

        
    }
}
