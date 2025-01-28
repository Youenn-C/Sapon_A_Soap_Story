using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("References"), Space(5)]
    [SerializeField] private GameObject _playerGo;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    public Rigidbody2D _playerRb;

    [Header("Variables"), Space(5)]
    [SerializeField] private int _jumpForce;
    [SerializeField] private int _moveSpeed;
    [Space(5)]
    [SerializeField] private string[] _isGroundedTags;
    [Space(5)]
    public bool canMove = true;
    [SerializeField] private bool _isAlive;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _asDobleJump;
    [SerializeField] private bool _isDying = false;
    [Space(5)]
    [SerializeField] private float _timeTillRespawn = 0;
    [SerializeField] private float _respawnTime = 1.0f;
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
    
    void Update()
    {
        MovePlayer();

        // Die duration
        if (_isDying)
        {
            if (_timeTillRespawn < _respawnTime)
            {
                _timeTillRespawn += Time.deltaTime;
                canMove = false;
            }
            else
            {
                _isAlive = false;
                _timeTillRespawn = 0;
                _isDying = false;
            }
        }
    }

    /// <summary> ------------------------------------------------------------------------------------------------------
    /// MOVEMENT -------------------------------------------------------------------------------------------------------
    /// </summary> -----------------------------------------------------------------------------------------------------
    
    void MovePlayer()
    {
        if (canMove)
        {
            float horizontalMovement = player.GetAxis("HorizontalMovement") * _moveSpeed * Time.deltaTime;
            FlipPlayer(horizontalMovement);
            _animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

            Vector3 targetVelocity = new Vector2(horizontalMovement, _playerRb.velocity.y);
            _playerRb.velocity = Vector3.SmoothDamp(_playerRb.velocity, targetVelocity, ref velocity, 0.05f);
                
            if (player.GetButtonDown("Jump") && _isGrounded)
            {
                Jump(_jumpForce);
            }
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
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var tag in _isGroundedTags)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                SetIsGrounded(true);
            }
        }
    }
    
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
        SetIsAlive(false);
        canMove = false;
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
            _isDying = true;
            _timeTillRespawn = 0;
        }
    }
}
