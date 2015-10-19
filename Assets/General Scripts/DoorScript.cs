using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    public int sceneToChangeTo;
    public Vector2 positionInNextScene;
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
        var player = other.transform.FindChild("Player");
        Application.LoadLevel(sceneToChangeTo);
        player.transform.position = positionInNextScene;
    }
}
