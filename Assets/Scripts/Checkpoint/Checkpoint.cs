using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Collider2D _checkpointCollider2D;
    [SerializeField] private SpriteRenderer _checkpointSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collision checkpoint");
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Player entered checkpoint");
            _spawnPoint.position = gameObject.transform.position;
            _checkpointCollider2D.enabled = false;
            _checkpointSpriteRenderer.color = Color.green;
        }
    }
}
