using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator Anim;


    [SerializeField]private float moveSpeed;
    [SerializeField]private float jumpForce;

    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    //在unity配置Layer
    [SerializeField] private LayerMask whatIsGround;
    //这个是做地面检测
    private bool isGrounded;


    private float xInput;

    private int facingDir = 1;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckInput();

        CollisionChecks();

        FlipController();
        AnimatorController();

    }

    private void CollisionChecks()
    {
        //做Layer的碰撞检测
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
      
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        
    }

    private void Jump()
    {
        if (isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void AnimatorController()
    {
        Anim.SetBool("isMoving", rb.velocity.x != 0);
        Anim.SetBool("isGrounded", isGrounded);
        Anim.SetFloat("yVelocity", rb.velocity.y);
    }


    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    //画线确定角色与地面相隔的位置
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
