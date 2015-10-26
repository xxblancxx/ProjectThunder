using UnityEngine;
using System.Collections;

public class NPCDialogScript : MonoBehaviour
{
    private bool talking;
    public int xSize;
    public int ySize;
    public int xPos;
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
            GUILayout.BeginArea(new Rect(new Vector2((Screen.width-120)/2,Screen.height-50), new Vector2(120,200)));
            GUILayout.TextArea("Hello Sønnike-Boy of you");
            GUILayout.EndArea();
        }

    }
    void OnTriggerStay2D(Collider2D other)
    {
     
        if (other.CompareTag("Player"))
        {
            talking = true;
           
        }
    }
}
