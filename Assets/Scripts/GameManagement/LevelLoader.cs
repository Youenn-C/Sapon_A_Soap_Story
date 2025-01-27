using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private string _levelToLoad;
    [SerializeField] private GameObject _uiIteraction;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private bool _playerIsTrigger;
    private ActivateInteractUI _activateInteractUI;

    void Start()
    {
        // Assure que le UI est bien relié
        _uiIteraction = GameObject.FindWithTag("UiIteraction");
        _activateInteractUI = _uiIteraction.GetComponent<ActivateInteractUI>();
    }
    
    void Update()
    {
        // Vérifie si le joueur appuie sur le bouton d'interaction
        if (PlayerController.Instance.player.GetButtonDown("Interaction") && _playerIsTrigger)
        {
            GameManager.Instance.LoadScene(_levelToLoad);
        }        
    }

    void OnTriggerEnter2D(Collider2D collidedElement)
    {
        if (collidedElement.CompareTag("Player"))
        {
            _playerIsTrigger = true;
            // Active l'UI d'interaction
            _activateInteractUI.SetInteractUIActive(_playerIsTrigger);
        }
    }

    void OnTriggerExit2D(Collider2D collidedElement)
    {
        if (collidedElement.CompareTag("Player"))
        {
            _playerIsTrigger = false;
            // Désactive l'UI d'interaction
            _activateInteractUI.SetInteractUIActive(_playerIsTrigger);
        }
    }
}