using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("References"), Space(5)]
    [SerializeField] private TMP_Text _npcName;
    [SerializeField] private TMP_Text _npcHierarchie;
    [SerializeField] private TMP_Text _npcDialogues;
    
    private Queue<string> _sentences;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of DialogueManager");
            return; 
        }
         _sentences = new Queue<string>();
    }

    public void StartDialogue(NPC npc)
    {
        _npcName.text = npc.NPCName;
        _npcHierarchie.text = npc.NPCHierarchie;
        
        _sentences.Clear();

        foreach (string sentence in npc.NPCDilogue)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();
        _npcDialogues.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("Dialogue End");
    }
}
