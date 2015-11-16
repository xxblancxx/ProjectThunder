using UnityEngine;
using System.Collections;

public class Wolf_Script : MonoBehaviour
{
    public bool walking;
    public Animator anim;
    public Vector2 stopPos;
    public float walkSpeed;
    private GameObject wolf;
    private Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {

        wolf = gameObject.transform.FindChild("Wolf").gameObject;
        anim = wolf.GetComponent<Animator>();
        rb = wolf.GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        if (walking)
        {
            anim.SetBool("Walking", true);
            rb.MovePosition(rb.position + (Vector2.right * walkSpeed) * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }





    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wolf.SetActive(true);
            walking = true;
        }
    }
}
