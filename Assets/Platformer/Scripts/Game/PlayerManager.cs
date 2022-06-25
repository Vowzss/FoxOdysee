using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
    [HideInInspector] public PlayerLife playerLife;
    private ActualLevel actualLevel;
    private DataSaver inventory;

    [Header("Player States")]
    [HideInInspector] public bool isInBushBox;
    [HideInInspector] public bool isHiddenInBush;
    [HideInInspector] public bool canBeHit;
    [HideInInspector] public bool isOutOfBorder;
    [HideInInspector] public bool isOnCrate;
    private bool isOnPlatform;

    [Header("Player Movement")]
    private bool isJumping;
    private bool isGrounded;
    [HideInInspector] public bool isOnJumpad;

    private Rigidbody2D rb2d;
    private bool isFlip = false;
    private float horizontal;
    private Vector3 velocity = Vector3.zero;

    public float playerSpeed;
    public float jumpForce;

    [Header("Player Collisions")]
    [HideInInspector] public Transform groundCheckOne;
    [HideInInspector] public Transform groundCheckTwo;
    public LayerMask mask;

    [Header("Player UI/UX")]
    private Animator animator;
    [HideInInspector] public SpriteRenderer[] display;

    private void Awake()
    {
        inventory = GameObject.FindObjectOfType<DataSaver>();
        actualLevel = GameObject.FindObjectOfType<ActualLevel>();
        playerLife = GameObject.FindObjectOfType<PlayerLife>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        display = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (inventory.crownBought && !isHiddenInBush) display[1].enabled = true;
        else display[1].enabled = false;

        CheckIfFlip();
        CheckIfCanJump();
        CheckIfCanHide();
        CheckIfHidden();
        CheckIfInBorders();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;

        float velocityFixed = Mathf.Abs(rb2d.velocity.x);
        animator.SetFloat("playerSpeed", velocityFixed);
        animator.SetBool("isJumping", isJumping);

        isGrounded = Physics2D.OverlapArea(groundCheckOne.position, groundCheckTwo.position, mask);

        Move(horizontal);
    }

    private void Move(float _hozitonal)
    {
        if (isHiddenInBush)
        {
            rb2d.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            Vector3 newVelocity = new Vector2(_hozitonal, rb2d.velocity.y);
            rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, newVelocity, ref velocity, .05f);

            if (isJumping)
            {
                rb2d.AddForce(new Vector2(0, jumpForce));
                isJumping = false;
            }
        }
    }
    private void CheckIfFlip()
    {
        if (horizontal > 0 && isFlip)
            Flip();
        else if (horizontal < 0 && !isFlip)
            Flip();
    }
    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFlip = !isFlip;
    }

    private void CheckIfCanJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isHiddenInBush && !isOnJumpad)
            isJumping = true;

        else if(isOnPlatform && Input.GetKeyDown(KeyCode.Space))
            isJumping = true;

        else if (isOnCrate && Input.GetKeyDown(KeyCode.Space))
            isJumping = true;
    }

    private void CheckIfCanHide()
    {
        if (isInBushBox && Input.GetKeyDown(KeyCode.E))
            isHiddenInBush = !isHiddenInBush;
    }

    private void CheckIfHidden()
    {
        if (isHiddenInBush)
        {
            display[0].sortingOrder = -1;
            canBeHit = false;
        }
        else
        {
            display[0].sortingOrder = 5;
            canBeHit = true;
        }
    }

    private void CheckIfInBorders()
    {
        if (isOutOfBorder)
            playerLife.currentLife = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
            gameObject.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
            gameObject.transform.parent = actualLevel.transform;
        }
    }
}
