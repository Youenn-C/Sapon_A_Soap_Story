using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInteractUI : MonoBehaviour
{ [SerializeField] private GameObject _interactUI;
    [SerializeField] private bool isActivate = false;

    void Update()
    {
        // Gère l'activation/désactivation de l'UI
        _interactUI.SetActive(isActivate);
    }

    // Méthode pour activer/désactiver l'UI depuis l'extérieur
    public void SetInteractUIActive(bool activate)
    {
        isActivate = activate;
    }
}