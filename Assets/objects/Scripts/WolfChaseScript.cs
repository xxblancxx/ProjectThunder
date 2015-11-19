using UnityEngine;
using System.Collections;

public class WolfChaseScript : MonoBehaviour
{
    bool isChasing;
    int sentenceNumber;
    public string[] wolfDialog;
    GameObject speechBubble;
    TextMesh speech;
    public GameObject wolf;
    private GameObject player;
    public AudioClip chaseMusic;
    // Use this for initialization
    void Start()
    {
        speechBubble = wolf.transform.FindChild("SpeechBubble").gameObject;
        speech = speechBubble.transform.FindChild("Speech").transform.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            DialogHappens();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isChasing)
        {
            wolf.transform.GetComponentInParent<Wolf_Script>().walking = false;
            speechBubble.SetActive(true);
            speech.text = TextWrappingHandler.SplitByNewline(wolfDialog[sentenceNumber]);
            player = other.gameObject;
            sentenceNumber = 0;
        }
        if (other.CompareTag("Player") && isChasing)
        {
            Application.LoadLevel(Application.loadedLevel+1);
        }
    }

    public void DialogHappens()
    {
        if (!isChasing && player != null)
        {
            if (sentenceNumber >= wolfDialog.Length - 1)
            {
                speechBubble.SetActive(false);
                isChasing = true;
                StartRunning();
                var music = player.GetComponent<AudioSource>();
                music.clip = chaseMusic;
                music.volume = 0.45f;
                music.Play();
            }
            else
            {
                wolf.transform.GetComponentInParent<Wolf_Script>().anim.SetTrigger("GetMad");
                sentenceNumber++;
                speech.text = TextWrappingHandler.SplitByNewline(wolfDialog[sentenceNumber]);
            }

        }
    }

    public void StartRunning()
    {
        if (isChasing && player != null)
        {
            player.GetComponent<ForestPlayerMovement>().enabled = true;
            wolf.transform.GetComponentInParent<Wolf_Script>().walking = true;
            transform.position = transform.position + Vector3.left;
            wolf.GetComponentInParent<Wolf_Script>().walkSpeed = 2.875f;
        }
    }
}
