using UnityEngine;
using System.Collections;

public class lookAtItemScript : MonoBehaviour
{
    public string message;
    private bool showMessage;
    private GameObject SpeechBubble;
    private TextMesh Speech;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }


   void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<PlayerMovement>().showMessage = false;
    }
}
