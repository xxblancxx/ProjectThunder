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
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movement_vector = new Vector2();
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
            anim.SetFloat("input_y", movement_vector.y);
        }
        else { anim.SetBool("isWalking", false); }

        rb.MovePosition(rb.position + (movement_vector * runSpeed) * Time.deltaTime);
       
    }

    public void RunOrWalk()
    {
        if (Input.GetKey(KeyCode.LeftShift))
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
        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
        {
            movement_vector = new Vector2(0, Input.GetAxisRaw("Vertical"));
        }
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
        { // check for left or right
            movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        }


    }

}
