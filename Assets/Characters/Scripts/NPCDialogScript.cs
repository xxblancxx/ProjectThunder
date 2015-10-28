using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDialogScript : MonoBehaviour
{
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
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (talking)
        {
            GUIStyle centerstyle = new GUIStyle();
            centerstyle.alignment = TextAnchor.UpperCenter;
            GUILayout.BeginArea(new Rect(new Vector2((Screen.width - 120) / 2, Screen.height - 50), new Vector2(120, 200)), centerstyle);
            GUILayout.TextArea(startQuestDialog[sentenceNumber]);
            GUILayout.EndArea();
        }
        if (finishQuest)
        {
            GUIStyle centerstyle = new GUIStyle();
            centerstyle.alignment = TextAnchor.UpperCenter;
            GUILayout.BeginArea(new Rect(new Vector2((Screen.width - 120) / 2, Screen.height - 50), new Vector2(120, 200)), centerstyle);
            GUILayout.TextArea(finishQuestDialog[sentenceNumber]);
            GUILayout.EndArea();
        }
        if (alreadyOnQuest)
        {
            GUIStyle centerstyle = new GUIStyle();
            centerstyle.alignment = TextAnchor.UpperCenter;
            GUILayout.BeginArea(new Rect(new Vector2((Screen.width - 120) / 2, Screen.height - 50), new Vector2(120, 200)), centerstyle);
            GUILayout.TextArea(alreadyOnQuestDialog[sentenceNumber]);
            GUILayout.EndArea();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Player") && Input.GetButtonDown("Fire1"))
        {
            var tomatoList = new List<GameObject>();
            var allTomatoList = GameObject.FindGameObjectsWithTag("TomatoPlant");
            foreach (var t in allTomatoList)
            {
                if (!t.GetComponent<TomatoScript>().isPicked)
                {
                    tomatoList.Add(t);
                }
            }
            if (finishQuest)
            {
                if (sentenceNumber >= finishQuestDialog.Length - 1)
                {
                    finishQuest = false;
                    sentenceNumber = 0;
                }
                else sentenceNumber++;
            }
            else if (tomatoList.Count == 0 && other.gameObject.GetComponent<PlayerInventory>().currentQuest == npcQuest)
            {
                finishQuest = true;
                if (!other.gameObject.GetComponent<PlayerInventory>().finishedQuestList.Contains(npcQuest))
                {
                    other.gameObject.GetComponent<PlayerInventory>().finishedQuestList.Add(npcQuest);
                }
            }
            

            if (tomatoList.Count>0 && other.gameObject.GetComponent<PlayerInventory>().currentQuest == npcQuest && !alreadyOnQuest)
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
