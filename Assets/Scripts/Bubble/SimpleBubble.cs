using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBubble : MonoBehaviour
{
    [Header("Variables"), Space(5)]
    [SerializeField] private float _basePropulsionForce = 500;
    [SerializeField] private Transform target; // GameObject cible pour la hauteur
    [SerializeField] private float gravity = 9.81f; // Gravité utilisée pour le calcul
    [SerializeField] private float playerMass = 1.0f; // Masse du joueur
    [SerializeField] private float playerGravityScale = 1.0f; // Gravity Scale du joueur

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Propulsion();
        }
    }
    
    public void Propulsion()
    {
        if (target == null)
        {
            Debug.LogWarning("Aucun target assigné à SimpleBubble");
            return;
        }
        
        Rigidbody2D playerRb = PlayerController.Instance._playerRb;
        float playerY = playerRb.position.y;
        float targetY = target.position.y;
        
        float requiredForce = CalculateRequiredForce(playerY, targetY, playerRb.mass);
        
        playerRb.AddForce(Vector2.up * requiredForce, ForceMode2D.Impulse);
    }
    
    private float CalculateRequiredForce(float startY, float targetY, float mass)
    {
        float heightDifference = targetY - startY;
        if (heightDifference <= 0) return _basePropulsionForce; // Si la cible est plus basse, utiliser la force de base
        
        float effectiveGravity = gravity * playerGravityScale;
        float requiredVelocity = Mathf.Sqrt(2 * effectiveGravity * heightDifference);
        return mass * requiredVelocity; // Calcul de la force nécessaire
    }
}