using UnityEngine;
using System.Collections;

public class NetworkPlayerMovement : Photon.MonoBehaviour
{
    private Animator anim;
    private Vector2 realPos;
    public float lerpSpeed;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector2.Lerp(transform.position, realPos, Time.deltaTime * lerpSpeed);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext((Vector2)gameObject.transform.position);
            stream.SendNext(anim.GetBool("isWalking"));
            stream.SendNext(anim.GetFloat("input_x"));
            stream.SendNext(anim.GetFloat("input_y"));
        }
        if (stream.isReading)
        {
            realPos = (Vector2) stream.ReceiveNext();
            anim.SetBool("isWalking", (bool)stream.ReceiveNext());
            anim.SetFloat("input_x", (float)stream.ReceiveNext());
            anim.SetFloat("input_y", (float)stream.ReceiveNext());
        }
    }
}
