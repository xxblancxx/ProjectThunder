using UnityEngine;
using System.Collections;

public class TomatoScript : Photon.MonoBehaviour
{
    public bool isPicked = false;
    private Animator anim;
    private int pickTicker;
    private int buttonFlicker;
    public int waitTime;
    private int currentWaitTime;
    private AudioSource sound;
    // Use this for initialization
    void Start()
    {
        buttonFlicker = 1;
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPicked)
        {
            if (currentWaitTime == 0)
            {
                isPicked = false;
                pickTicker = 0;
            }
            else
            {
               anim.SetBool("IsPicked", true);
                currentWaitTime--;
            }
            
        }
        else
        {
            anim.SetBool("IsPicked", false);
        }

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isPicked);
        }
        if (stream.isReading)
        {
            isPicked = (bool)stream.ReceiveNext();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerInventory>().currentQuest == "Picking Tomatoes")
        {
            if (pickTicker >= 20 && !isPicked)
            {
                var inv = other.GetComponent<PlayerInventory>();
                inv.tomatoCount++;
                var leavesObj = transform.FindChild("Leaves");
                var leafAnim = leavesObj.GetComponent<Animator>();
                //Debug.Log(leafAnim.runtimeAnimatorController);
                leafAnim.SetTrigger("triggerSpray");
                isPicked = true;
                sound.Play();
                currentWaitTime = waitTime;

            }
            if (other.CompareTag("Player"))
            {
                // This is our player

                if (Input.GetAxisRaw("Horizontal") == buttonFlicker && !isPicked)
                {
                    pickTicker++;
                    buttonFlicker = buttonFlicker * -1;
                }
            }
        }
    }
}
