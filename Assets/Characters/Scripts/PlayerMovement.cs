using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement_vector;
    private float runSpeed;
    public float increasedSpeed;
    public float walkSpeed;
    private GameObject SpeechBubble;
    private TextMesh Speech;
    public bool showMessage;
  
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movement_vector = new Vector2();
        SpeechBubble = transform.FindChild("SpeechBubble").gameObject;
        Speech = SpeechBubble.transform.FindChild("Speech").transform.GetComponent<TextMesh>();
        

    }

    // Update is called once per frame
    void Update()
    {
       

        if (!showMessage && SpeechBubble != null)
        {
            SpeechBubble.SetActive(false);
        }
       

        movement_vector = new Vector2(0, 0);
        RunOrWalk();
        GetDirection();

        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", movement_vector.x);
            anim.SetFloat("input_y", movement_vector.y);
        }
        else { anim.SetBool("isWalking", false); }

        rb.MovePosition(rb.position + (movement_vector * runSpeed) * Time.deltaTime);

    }

    private void DisplayLookedAtItem(string message)
    {
        if (showMessage)
        {
            SpeechBubble.SetActive(true);
            Speech.text = TextWrappingHandler.SplitByNewline(message);
        }
    }
    public void RunOrWalk()
    {
        if (Input.GetButton("Triggers") || Input.GetAxisRaw("Triggers") < 0)
        {
            runSpeed = increasedSpeed;
            anim.speed = increasedSpeed;
        }
        else
        {
            runSpeed = walkSpeed;
            anim.speed = 1;
        }

    }

    public void GetDirection()
    {

        // Checks for horizontal input first, so player can only move left or right
        // No diagonal movement.
        // Check for up or down
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            movement_vector = new Vector2(0, -1);
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            movement_vector = new Vector2(0, 1);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        { // check for left or right
            movement_vector = new Vector2(-1, 0);
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        { // check for left or right
            movement_vector = new Vector2(1, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Object") && Input.GetButtonDown("Fire1"))
        {
            if (showMessage)
            {
                showMessage = false;
            }
            else if (!showMessage)
            {
                showMessage = true;
                DisplayLookedAtItem(other.GetComponent<lookAtItemScript>().message);
            }
        }
    }

}
