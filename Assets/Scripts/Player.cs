using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public bool isGrounded = false;
    public float speedMulti;
    private float speedMileStoneCount;
    private float speedMileStoneCountStore;
    public float speedIncreaseMileStone;
    public float speedIncreaseMileStoneStore;
    public bool isSliding = false;
    public float slideTime = 0;
    public float maxSlideTime = 1f;
    public GameObject ragdoll;

    public LayerMask groundLayer;

    public CapsuleCollider2D col;
    private Rigidbody2D rb;
    public Animator anim;
    public bool isDead;

    private Vector3 lastVel;
    private Vector3 lastAngVel;

    public GameManager gameManager;
    public GameObject clone;
    public Vector2 colSize;
    public Vector2 colOffset;
    private Vector2 colSizeOld;
    private Vector2 colOffsetOld;
    public Transform groundCheck;
    public float groundCheckRad;



    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        ragdoll = Resources.Load("stick_ragdoll Variant") as GameObject;
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        if (transform.position.x > speedMileStoneCount)
        {
            speedMileStoneCount += speedIncreaseMileStone * speedMulti;
            moveSpeed = moveSpeed * speedMulti;
        }
        if (data.Direction == SwipeDirection.Up && isGrounded && !isDead)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }
        if (data.Direction == SwipeDirection.Down && !isSliding && !isDead)
        {
            if (isGrounded)
            {
                slideTime = 0;
                anim.SetBool("isSliding", true);
                isSliding = true;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
            }
        }
        if (isSliding)
        {
            slideTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("isSliding", false);
                isSliding = false;
                col.offset = colOffset;
                col.size = colSize;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            if (slideTime > maxSlideTime)
            {
                isSliding = false;
                anim.SetBool("isSliding", false);
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
                col.offset = colOffset;
                col.size = colSize;
            }

        }
    }

    private void FixedUpdate()
    {
        lastVel = rb.velocity;
    }

    private void ActiveRagdoll(float force, bool isSharp)
    {
        clone = (GameObject)Instantiate(ragdoll, transform.position, transform.rotation);
        Collider2D[] colliders = clone.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            Physics2D.IgnoreCollision(col, colliders[colliders.Length - 1]);
        }
        Rigidbody2D[] rigidbodies = clone.GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D child in rigidbodies)
        {
            child.velocity = lastVel;
            if(isSharp)
            {
                child.AddForce(Vector3.right * moveSpeed * force);
                child.AddForce(Vector3.up * force);
            }
            else
            {
                child.AddForce(Vector3.right * force);
                child.AddForce(Vector3.down * force);
            }
           
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "KillZone":
                isDead = true;
                ActiveRagdoll(5f, false);
                speedMileStoneCount = speedMileStoneCountStore;
                speedIncreaseMileStone = speedIncreaseMileStoneStore;
                moveSpeed = 5.0f;
                isSliding = false;
                gameManager.RestartGame();
                break;
            case "Saw":
                isDead = true;
                ActiveRagdoll(150f, true);
                speedMileStoneCount = speedMileStoneCountStore;
                speedIncreaseMileStone = speedIncreaseMileStoneStore;
                moveSpeed = 5.0f;
                isSliding = false;
                gameManager.RestartGame();
                break;
            case "Spike":
                isDead = true;
                ActiveRagdoll(5f, false);
                speedMileStoneCount = speedMileStoneCountStore;
                speedIncreaseMileStone = speedIncreaseMileStoneStore;
                moveSpeed = 5.0f;
                isSliding = false;
                gameManager.RestartGame();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "KillZone":
                isDead = true;
                ActiveRagdoll(5f,false);
                speedMileStoneCount = speedMileStoneCountStore;
                speedIncreaseMileStone = speedIncreaseMileStoneStore;
                moveSpeed = 5.0f;
                isSliding = false;
                gameManager.RestartGame();
                break;
            case "Saw":
                isDead = true;
                ActiveRagdoll(150f, true);
                speedMileStoneCount = speedMileStoneCountStore;
                speedIncreaseMileStone = speedIncreaseMileStoneStore;
                moveSpeed = 5.0f;
                isSliding = false;
                gameManager.RestartGame();
                break;
            case "Spike":
                isDead = true;
                ActiveRagdoll(5f, false);
                speedMileStoneCount = speedMileStoneCountStore;
                speedIncreaseMileStone = speedIncreaseMileStoneStore;
                moveSpeed = 5.0f;
                isSliding = false;
                gameManager.RestartGame();
                break;
        }

    }



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        speedMileStoneCount = speedIncreaseMileStone;
        isDead = false;
        speedIncreaseMileStoneStore = speedIncreaseMileStone;
        speedMileStoneCountStore = speedMileStoneCount;
        moveSpeed = 5.0f;
        colSize = col.size;
        colOffset = col.offset;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        //isGrounded = Physics2D.IsTouchingLayers(col, groundLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRad, groundLayer);
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        anim.SetBool("Grounded", isGrounded);
    }

    private void Moving()
    {
   
        if (transform.position.x > speedMileStoneCount)
        {
            speedMileStoneCount += speedIncreaseMileStone * speedMulti;
            moveSpeed = moveSpeed * speedMulti;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && !isDead)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
     
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding && !isDead)
        {
            if(isGrounded)
            {
                slideTime = 0;
                anim.SetBool("isSliding", true);
                isSliding = true;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
            }

        }
        if (isSliding)
        {
            slideTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                anim.SetBool("isSliding", false);
                isSliding = false;
                col.offset = colOffset;
                col.size = colSize;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            }
            if (slideTime > maxSlideTime)
            {
                isSliding = false;
                anim.SetBool("isSliding", false);
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
                col.offset = colOffset;
                col.size = colSize;
            }

        }

  
    }

}

