using System;
using UnityEngine;
using System.Collections;
using Assets;

public class ForestPlayerMovement : MonoBehaviour
{
    private CharacterController2D rb;
    private Animator anim;
    private Vector2 movement_vector;
    private float runSpeed;
    public float increasedSpeed;
    public float walkSpeed;
    public float jumpHeight;
    private bool jumping;
    private bool isGrounded;

    //private GameObject SpeechBubble;
    //private TextMesh Speech;
    //public bool showMessage;
    public float bgScrollSpeed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        movement_vector = new Vector2();
        //SpeechBubble = transform.FindChild("SpeechBubble").gameObject;
        //Speech = SpeechBubble.transform.FindChild("Speech").transform.GetComponent<TextMesh>();


    }

    void FixedUpdate()
    {

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumping = true;
            isGrounded = false;
        }
        if (isGrounded)
        {
            jumping = false;
        }
        if (jumping)
        {
            rb.move(Vector2.up * jumpHeight * Time.deltaTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        movement_vector = new Vector2(0, 0);

        RunOrWalk();
        GetDirection();

        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", movement_vector.x);
            rb.move((movement_vector * runSpeed) * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }


        try
        {
            if (movement_vector.y == 0)
            {
                var bg = GameObject.Find("SlowMoveBG").transform;
                bg.position = ((Vector2)bg.position + (movement_vector * runSpeed * bgScrollSpeed) * Time.deltaTime);
            }
        }
        catch (NullReferenceException)
        {
            // do nothing. but handle.
        }



    }

    public void RunOrWalk()
    {
        if (Input.GetButton("Triggers") || Input.GetAxisRaw("Triggers") < 0)
        {
            runSpeed = increasedSpeed;
            anim.speed = 2;
        }
        else
        {
            runSpeed = walkSpeed;
            anim.speed = 1;
        }

    }



    public void GetDirection()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        { // check for left or right
            movement_vector = new Vector2(-1, 0);
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        { // check for left or right
            movement_vector = new Vector2(1, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
