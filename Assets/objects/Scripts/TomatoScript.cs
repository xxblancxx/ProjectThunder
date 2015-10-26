using UnityEngine;
using System.Collections;

public class TomatoScript : Photon.MonoBehaviour
{
    public bool isPicked = false;
    private Animator anim;
    private int pickTicker;
    private int buttonFlicker;
    // Use this for initialization
    void Start()
    {
        buttonFlicker = 1;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPicked)
        {
            anim.SetBool("IsPicked", true);
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
        if (pickTicker >= 20 && !isPicked)
        {
            isPicked = true;
        }
        if (other.CompareTag("Player"))
        { // This is our player
           
            if (Input.GetAxisRaw("Horizontal") == buttonFlicker && !isPicked)
            {
                Debug.Log(pickTicker);
                pickTicker++;
                buttonFlicker = buttonFlicker *-1;
            }
        }

    }
}
