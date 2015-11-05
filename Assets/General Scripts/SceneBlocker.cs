using UnityEngine;
using System.Collections;

public class SceneBlocker : MonoBehaviour
{
    public string unlockOnQuest;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerInventory>().finishedQuestList.Contains(unlockOnQuest))
        {
            gameObject.SetActive(false);
        }
       
       
    }
}
