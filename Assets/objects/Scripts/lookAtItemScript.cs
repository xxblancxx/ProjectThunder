using UnityEngine;
using System.Collections;

public class lookAtItemScript : MonoBehaviour
{
    public string message;
    private bool showMessage;
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
        if (showMessage)
        {
            GUIStyle centerstyle = new GUIStyle();
            centerstyle.alignment = TextAnchor.UpperCenter;
            centerstyle.fontSize = 18;
            centerstyle.normal.textColor = Color.white;
            GUILayout.BeginArea(new Rect(new Vector2((Screen.width - 360) / 2, (Screen.height - ((Screen.height - 200) / 3))), new Vector2(360, 200)), centerstyle);
            GUILayout.TextArea(message, centerstyle);
            GUILayout.EndArea();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetButtonDown("Fire1"))
        {
            if (showMessage)
            {
                showMessage = false;
            }
            else showMessage = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        showMessage = false;
    }
}
