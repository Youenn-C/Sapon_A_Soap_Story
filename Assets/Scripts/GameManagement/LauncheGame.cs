using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncheGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); ;
    }

    public void CallStartGame()
    {
        _gameManager.StartGame();
    }
}
