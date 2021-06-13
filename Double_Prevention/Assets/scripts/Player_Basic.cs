using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Basic : MonoBehaviour
{
    protected float _moveSpeed = 7;
    protected float _jumpForce = 25;
    protected float horizontalInput;
    protected float canShoot;
    protected float shootCooldown = 1;

    protected Rigidbody2D _rb;

    public BoxCollider2D _boxCollider;

    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] protected GameObject boy;
    [SerializeField] protected GameObject girl;
    [SerializeField] protected GameObject alcohol;

    private static int lives = 3;

    private bool isBoy;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        if(gameObject.name == "Boy")
        {
            isBoy = true;
        }
        else
        {
            isBoy = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Jump();
        SwitchCharacter();

        if (!isBoy && Input.GetKeyDown(KeyCode.Z) && canShoot < Time.time)
        {
            ThrowAlcohol();
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                TakeDamage(-1);
            }
            else
            {
                TakeDamage(1);
            }
        }
    }

    private void Movement()
    {

        Vector2 direction = new Vector2(horizontalInput, 0);

        transform.Translate(direction * _moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(0.08f, 0.08f, 1);
            //_playerAnimations.SetBool("Walking", true);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-0.08f, 0.08f, 1);
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

    private void SwitchCharacter()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _boxCollider.enabled = false;
            _rb.gravityScale = 0;
            if (isBoy)
            {
                girl.SetActive(true);
                girl.transform.parent.DetachChildren();
                gameObject.transform.SetParent(girl.transform);
                girl.GetComponent<BoxCollider2D>().enabled = true;
                girl.GetComponent<Rigidbody2D>().gravityScale = 10;
                girl.GetComponent<Rigidbody2D>().velocity = _rb.velocity;
                gameObject.SetActive(false);
            }
            else
            {
                boy.SetActive(true);
                boy.transform.parent.DetachChildren();
                gameObject.transform.SetParent(boy.transform);
                boy.GetComponent<BoxCollider2D>().enabled = true;
                boy.GetComponent<Rigidbody2D>().gravityScale = 10;
                boy.GetComponent<Rigidbody2D>().velocity = _rb.velocity;
                gameObject.SetActive(false);
            }
        }
    }

    protected void TakeDamage(int hitDirection)
    {
        KnockBack(2, hitDirection, 3);
        lives--;
        if(lives <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void KnockBack(float hitDirection, float horizontalForce, float verticalForce)
    {
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = 2.5f;
        _rb.AddForce(new Vector2(horizontalForce * hitDirection, verticalForce), ForceMode2D.Impulse);
    }

    protected void ThrowAlcohol()
    {
        if(transform.localScale.x > 0)
        {
            Instantiate(alcohol, transform.position + new Vector3(1, 0.3f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(alcohol, transform.position + new Vector3(-1, 0.3f, 0), Quaternion.Euler(0, 180, 0));
        }
        canShoot = Time.time + shootCooldown;
    }
}
