using System.Collections;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("References"), Space(5)]
    [SerializeField] private GameObject _playerGo;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundCollisionLayer;
    public Rigidbody2D _playerRb;

    [Header("Variables"), Space(5)]
    [SerializeField] private int _jumpForce;
    [SerializeField] private int _moveSpeed;
    [Space(5)]
    [SerializeField] private string[] _isGroundedTags;
    [Space(5)]
    public bool canMove = true;
    [SerializeField] private bool _isAlive = true;
    [SerializeField] private bool _canBeRespawn = true;
    [SerializeField] private bool _isGrounded;
    [Space(5)]
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _respawnTime;
    [Space(5)]
    private Vector3 velocity = Vector3.zero;
    
    [Header("Rewired"), Space(5)]
    public Player player;
    [SerializeField] private int playerId = 0;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        GameManager.Instance._deathCountText.text = "Number of Death : " + GameManager.Instance._deathCount.ToString(); 
    }
    
    void Update()
    {
        if (_isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundCollisionLayer))
        {
            SetIsGrounded(true);
        }
        else
        {
            SetIsGrounded(false);
        }
        
        if (player.GetButtonDown("Jump") && _isGrounded && canMove)
        {
            Jump(_jumpForce);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary> ------------------------------------------------------------------------------------------------------
    /// MOVEMENT -------------------------------------------------------------------------------------------------------
    /// </summary> -----------------------------------------------------------------------------------------------------
    
    void MovePlayer()
    {
        if (canMove)
        {
            float horizontalMovement = player.GetAxis("HorizontalMovement") * _moveSpeed;
            
            FlipPlayer(horizontalMovement);
            _animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

            Vector3 targetVelocity = new Vector2(horizontalMovement, _playerRb.linearVelocity.y);
            _playerRb.linearVelocity = Vector3.SmoothDamp(_playerRb.linearVelocity, targetVelocity, ref velocity, 0.05f);
        }
    }

    void FlipPlayer(float horizontalMovement)
    {
        bool flipX = _spriteRenderer.flipX;
        if (flipX && horizontalMovement > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if(!flipX && horizontalMovement < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
    public void Jump(float jumpForce)
    {
        _playerRb.AddForce(transform.up * jumpForce);
        SetIsGrounded(false);
    }
    
    /// <summary> ------------------------------------------------------------------------------------------------------
    /// GROUND CHECK ---------------------------------------------------------------------------------------------------
    /// </summary> -----------------------------------------------------------------------------------------------------
    
    void SetIsGrounded(bool value)
    {
        _isGrounded = value;
        _animator.SetBool("IsGrounded", value);
    }
    
    /// <summary> ------------------------------------------------------------------------------------------------------
    /// LIFE SYSTEM ----------------------------------------------------------------------------------------------------
    /// </summary> -----------------------------------------------------------------------------------------------------
    public void Die()
    {
        StartCoroutine(DieProcess());
    }

    public IEnumerator DieProcess()
    {
        var _canIncrement = true;
        _isAlive = false;
        _canBeRespawn = false;
        canMove = false;

        _animator.SetBool("IsAlive", _isAlive);
        
        if (_canIncrement)
        {
            GameManager.Instance._deathCount ++;
            GameManager.Instance._deathCountText.text = "Number of Death : " + GameManager.Instance._deathCount.ToString();
            _canIncrement = false;
        }
        _playerRb.constraints = RigidbodyConstraints2D.None;
        _playerRb.constraints = RigidbodyConstraints2D.FreezeRotation; 
        yield return new WaitForSecondsRealtime(2f);
        _canBeRespawn = true;
    }
    
    public void Respawn()
    {
        SetIsAlive(true);
        canMove = true;
    }
    
    public bool IsAlive()
    {
        return _isAlive;
    }
    public void SetIsAlive(bool value)
    {
        if (value)
        {
            _animator.SetBool("IsAlive", value);
            _isAlive = value;
        }
        else
        {
            _animator.SetBool("IsAlive", false);
        }
    }

    public bool GetCanBeRespawn()
    {
        return _canBeRespawn;
    }
}
