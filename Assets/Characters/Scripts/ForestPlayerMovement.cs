using System;
using UnityEngine;
using System.Collections;

public class ForestPlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement_vector;
    private float runSpeed;
    public float increasedSpeed;
    public float walkSpeed;
    //private GameObject SpeechBubble;
    //private TextMesh Speech;
    //public bool showMessage;
    public float bgScrollSpeed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movement_vector = new Vector2();
        //SpeechBubble = transform.FindChild("SpeechBubble").gameObject;
        //Speech = SpeechBubble.transform.FindChild("Speech").transform.GetComponent<TextMesh>();


    }

    // Update is called once per frame
    void Update()
    {

        //if (!showMessage && SpeechBubble != null)
        //{
        //    SpeechBubble.SetActive(false);
        //}

        movement_vector = new Vector2(0, 0);
        RunOrWalk();
        GetDirection();

        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", movement_vector.x);
        }
        else { anim.SetBool("isWalking", false); }

        rb.MovePosition(rb.position + (movement_vector * runSpeed) * Time.deltaTime);
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
}
