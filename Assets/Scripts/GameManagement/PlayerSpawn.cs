using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = System.Numerics.Vector2;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _spawnPointPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _targetTransform;

    void Start()
    {
        _playerPrefab = GameObject.FindGameObjectWithTag("Player");
        _targetTransform.position = _spawnPoint.position;

        PlacePlayerOnSpawnPoint();
    }

    void PlacePlayerOnSpawnPoint()
    {
        _playerPrefab.transform.position = _spawnPoint.transform.position;
    }

    void Update()
    {
        if (!PlayerController.Instance.IsAlive())
        {
            Debug.Log("Player respawn");
            PlayerController.Instance.Respawn();
            PlacePlayerOnSpawnPoint();
        }
    }
}