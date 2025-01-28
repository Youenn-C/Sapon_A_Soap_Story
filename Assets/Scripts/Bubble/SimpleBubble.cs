using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBubble : MonoBehaviour
{
    //[Header("References"), Space(5)]

    [Header("Variables"), Space(5)]
    [SerializeField] private float _propultionForce = 500;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Propulsion();
        }
    }
    
    public void Propulsion()
    {
        //PlayerController.Instance._playerRb.AddForce(transform.up * _propultionForce);
        PlayerController.Instance.Jump(_propultionForce);
    }
}
