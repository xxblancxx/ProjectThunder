using UnityEngine;
using System.Collections;
using Assets;

public class DoorScript : MonoBehaviour
{
    public int sceneToChangeTo;
    public Transform destination;
    public AudioClip desiredMusic;
    public float maxXPos;
    public float minXPos;
    public float maxYPos;
    public float minYPos;
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
        //Debug.Log(other.gameObject.name);
        other.transform.position = destination.position;
        // Boundary Control Objects.

        // set music to different song
        other.GetComponent<AudioSource>().Stop();
        other.GetComponent<AudioSource>().clip = desiredMusic;
        other.GetComponent<AudioSource>().Play();

        CameraScript cam = other.transform.parent.Find("Main Camera").GetComponent<CameraScript>();
        //Boundary Control
        cam.maxXPos = maxXPos;
        cam.maxYPos = maxYPos;
        cam.minXPos = minXPos;
        cam.minYPos = minYPos;


    }
}
