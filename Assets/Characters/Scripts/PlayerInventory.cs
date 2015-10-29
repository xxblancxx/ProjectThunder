using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Texture tomatoImage;
    public int tomatoCount;
    public string currentQuest;
    public List<string> finishedQuestList; 

    // Use this for initialization
    void Start()
    {
        finishedQuestList = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
       CheckIfHUDVisible();
    }


    void CheckIfHUDVisible()
    {
        if (tomatoCount > 0)
        {
            GUILayout.BeginArea(new Rect(new Vector2(10, Screen.height - 50), new Vector2(45, 45)), tomatoImage);
            GUILayout.Label(tomatoCount.ToString());
            GUILayout.EndArea();
        }
    }
}
