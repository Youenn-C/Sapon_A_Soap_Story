using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectionalBubble : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private SpriteRenderer _bubbleSpriteRenderer;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _bubbleTransform;
    [SerializeField] private Collider2D _bubbleCollider;
    [SerializeField] private Vector3 _initialPosition;

    [Header("Variables")]
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _initialParent;
    [SerializeField] private Transform _bubble;
    [SerializeField] private bool _playerIsInBubble;

    [Header("Directional Bubble Settings")]
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Transform _target;
    [SerializeField] private int _desPoint;
    [SerializeField] private float _speed;

    private bool _isMoving = false;  // Flag to indicate if the bubble is moving
    private bool _isAtLastWaypoint = false;  // Flag to check if the bubble has reached the last waypoint

    void Start()
    {
        _playerPrefab = GameObject.FindGameObjectWithTag("Player");
        _initialPosition = transform.position;

        MovePlayerToCurrentScene();

        if (_waypoints.Length > 0)
        {
            _target = _waypoints[0];
        }
    }

    void Update()
    {
        if (_isMoving && _playerIsInBubble)  // La bulle commence à se déplacer seulement si elle est en mouvement
        {
            PlayerController.Instance.canMove = false;

            // Si la bulle est proche de la cible, on passe au waypoint suivant
            if (Vector3.Distance(transform.position, _target.position) < 0.3f)
            {
                // Changer la cible (cycle entre waypoints)
                _desPoint = (_desPoint + 1) % _waypoints.Length;
                _target = _waypoints[_desPoint];

                // Si on atteint le dernier waypoint, on marque qu'on a atteint la destination finale
                if (_desPoint == 0 && !_isAtLastWaypoint)
                {
                    _isAtLastWaypoint = true;
                    StartCoroutine(ReleasePlayer());
                }
            }

            // Déplace la bulle vers la cible
            MoveBubbleTowardsTarget();

            // Déplace le joueur avec la bulle
            PlayerController.Instance._playerRb.transform.position = _bubbleTransform.position;
        }
    }

    void MovePlayerToCurrentScene()
    {
        // Exemple d'ajout de l'objet à la scène actuelle
        if (_playerPrefab != null)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.MoveGameObjectToScene(_playerPrefab, currentScene);
        }
    }

    void MovePlayerToDontDestroyOnLoad()
    {
        // Exemple d'ajout de l'objet à la scène actuelle
        if (_playerPrefab != null)
        {
            DontDestroyOnLoad(_playerPrefab);
        }
    }

    private void MoveBubbleTowardsTarget()
    {
        // Déplace la bulle vers la cible
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si le joueur entre dans la bulle, on le prend à bord et commence le mouvement
        if (other.CompareTag("Player"))
        {
            _playerIsInBubble = true;
            _player = other.transform;
            _initialParent = _player.parent; // Stocke le parent initial du joueur
            _player.SetParent(_bubble);  // Définit la bulle comme parent du joueur

            PlayerController.Instance._playerRb.gravityScale = 0;

            StartCoroutine(LaunchPlayer());

            // Démarre le mouvement de la bulle
            _isMoving = true;
        }
    }

    public IEnumerator LaunchPlayer()
    {
        _playerPrefab.transform.position = transform.position;
        yield return new WaitForSeconds(2f);
    }

    public IEnumerator ReleasePlayer()
    {
        // Une fois que la bulle atteint le dernier waypoint, on relâche le joueur
        Debug.Log("Relâcher le joueur !");
        _isMoving = false;

        PlayerController.Instance._playerRb.gravityScale = 5;
        _player.SetParent(_initialParent);  // Relâche le joueur de la bulle
        PlayerController.Instance.canMove = true;
        _playerIsInBubble = false;
        _player = null;

        _bubbleCollider.enabled = false;
        _bubbleSpriteRenderer.enabled = false;

        MovePlayerToDontDestroyOnLoad();

        yield return new WaitForSeconds(0.5f);

        _bubbleTransform.position = _initialPosition;

        yield return new WaitForSeconds(0.2f);

        _bubbleCollider.enabled = true;
        _bubbleSpriteRenderer.enabled = true;
        _isAtLastWaypoint = false;
    }
}
