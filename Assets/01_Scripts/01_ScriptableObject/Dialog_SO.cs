using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [TextArea(5, 5)]
    [SerializeField] private string m_ActionPhrase;
    [TextArea(5, 5)]
    [SerializeField] private string[] m_Answers;

    public string Action_Phrase { get => m_ActionPhrase; set => m_ActionPhrase = value; }
    public string[] Answers { get => m_Answers; set => m_Answers = value; }

    public string GetAnswer()
    {
        return Answers[Random.Range(0, Answers.Length)];
    }
}

[CreateAssetMenu(fileName = "New Dialog", menuName = "New Scriptable Object/New Dialog")]
public class Dialog_SO : ScriptableObject
{
    [SerializeField] private Dialog[] m_ActionsPhrases;
    public Dialog[] ActionPhrases { get => m_ActionsPhrases; set => m_ActionsPhrases = value; }

    public Dialog GetDialog()
    {
        return ActionPhrases[Random.Range(0, ActionPhrases.Length )];
    }
}
