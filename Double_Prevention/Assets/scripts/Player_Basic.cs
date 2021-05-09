using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Basic : MonoBehaviour
{
    protected float _moveSpeed = 7;
    protected float _jumpForce = 16;

    protected Rigidbody2D _rb;

    private BoxCollider2D _boxCollider;

    [SerializeField] private LayerMask _groundLayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector2 direction = new Vector2(horizontalInput, 0);

        transform.Translate(direction * _moveSpeed * Time.deltaTime);

        /*if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(9f, 9f, 1);
            //_playerAnimations.SetBool("Walking", true);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-9f, 9f, 1);
            //_playerAnimations.SetBool("Walking", true);
        }
        /*else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            //_playerAnimations.SetBool("Walking", false);
        }*/
    }

    private void Jump()
    {
        float jumpSpeed = _rb.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                _rb.AddForce(new Vector2(0, _jumpForce - jumpSpeed), ForceMode2D.Impulse);
            }
            /*else if (_canDoubleJump)
            {
                _rb.AddForce(new Vector2(0, _jumpForce - jumpSpeed), ForceMode2D.Impulse);
                _canDoubleJump = false;
            }*/
        }
    }

    private bool IsGrounded()
    {
        float height = 0.3f;
        RaycastHit2D rayCastHit = Physics2D.BoxCast(
            _boxCollider.bounds.center,
            _boxCollider.bounds.size - new Vector3(0.5f, 0.5f, 0),
            0f,
            Vector2.down,
            height,
            _groundLayer);
        return rayCastHit == true;
    }
}
