using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Collider2D _checkpointCollider2D;
    [SerializeField] private SpriteRenderer _checkpointSpriteRenderer;
    [SerializeField] private Animator _checkpointAnimator;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _spawnPoint.position = gameObject.transform.position;
            _checkpointCollider2D.enabled = false;
            _checkpointAnimator.SetTrigger("ActiveCheckpoint");
        }
    }
}
