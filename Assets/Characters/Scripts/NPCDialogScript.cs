using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Media;
//using UnityEditor.Animations;

public class NPCDialogScript : MonoBehaviour
{
    public AudioClip SoundClip;
    private AudioSource soundPlayer;
    private bool talking;
    public int xSize;
    public int ySize;
    public int xPos;
    public string[] startQuestDialog;
    private int sentenceNumber;
    public string npcQuest;
    public string[] alreadyOnQuestDialog;
    private bool alreadyOnQuest;
    public string[] finishQuestDialog;
    private bool finishQuest;
    private GameObject SpeechBubble;
    private TextMesh Speech;
    // Use this for initialization
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
        SpeechBubble = transform.FindChild("SpeechBubble").gameObject;
        Speech = SpeechBubble.transform.FindChild("Speech").transform.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (talking)
        {
            SpeechBubble.SetActive(true);
            Speech.text = TextWrappingHandler.SplitByNewline(startQuestDialog[sentenceNumber]);
        }
        if (finishQuest)
        {
            SpeechBubble.SetActive(true);
            Speech.text = TextWrappingHandler.SplitByNewline(finishQuestDialog[sentenceNumber]);
       }
        if (alreadyOnQuest)
        {
            SpeechBubble.SetActive(true);
            Speech.text = TextWrappingHandler.SplitByNewline(alreadyOnQuestDialog[sentenceNumber]);
        }
        else if (!talking && !finishQuest && !alreadyOnQuest)
        {
            SpeechBubble.SetActive(false);
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Player") && Input.GetButtonDown("Fire1"))
        {
            if (finishQuest)
            {
                if (sentenceNumber >= finishQuestDialog.Length - 1)
                {
                    finishQuest = false;
                    sentenceNumber = 0;
                }
                else sentenceNumber++;
            }
            else if ((other.gameObject.GetComponent<PlayerInventory>().tomatoCount >= 8 && other.gameObject.GetComponent<PlayerInventory>().currentQuest == npcQuest) || other.gameObject.GetComponent<PlayerInventory>().finishedQuestList.Contains(npcQuest))
            {
                finishQuest = true;
                if (!other.gameObject.GetComponent<PlayerInventory>().finishedQuestList.Contains(npcQuest))
                {
                    ParticleSystem partsys = GetComponent<ParticleSystem>();
                    soundPlayer.PlayOneShot(SoundClip);
                    partsys.Play(true);
                    other.gameObject.GetComponent<PlayerInventory>().tomatoCount = 0;
                    other.gameObject.GetComponent<PlayerInventory>().finishedQuestList.Add(npcQuest);
                    other.gameObject.GetComponent<PlayerInventory>().currentQuest = "";
                }
            }


            if (other.gameObject.GetComponent<PlayerInventory>().tomatoCount < 8 && other.gameObject.GetComponent<PlayerInventory>().currentQuest == npcQuest && !alreadyOnQuest)
            {
                talking = false;
                alreadyOnQuest = true;
            }
            else if (alreadyOnQuest)
            {
                if (sentenceNumber >= alreadyOnQuestDialog.Length - 1)
                {
                    alreadyOnQuest = false;
                    sentenceNumber = 0;
                }
                else sentenceNumber++;
            }
            else if (!talking && !alreadyOnQuest && !finishQuest && !other.gameObject.GetComponent<PlayerInventory>().finishedQuestList.Contains(npcQuest))
            {
                talking = true;
            }
            else if (talking)
            {
                // sentenceNumber should always be at max, at length-1.
                // Because index starts at 0
                if (sentenceNumber >= startQuestDialog.Length - 1)
                {
                    other.gameObject.GetComponent<PlayerInventory>().currentQuest = npcQuest;
                    talking = false;
                    sentenceNumber = 0;
                }
                else sentenceNumber++;
            }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        sentenceNumber = 0;
        talking = false;
        alreadyOnQuest = false;
        finishQuest = false;
    }


}
