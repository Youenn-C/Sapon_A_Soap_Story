using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("References"), Space(5)]
    public NPC npc;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private bool _isInRange;
    
    
    void Update()
    {
        if (_isInRange && PlayerController.Instance.player.GetButtonDown("Interaction"))
        {
            TriggerDialoque();
        }
    }

    void TriggerDialoque()
    {
        DialogueManager.Instance.StartDialogue(npc);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = false;
        }
    }
}
