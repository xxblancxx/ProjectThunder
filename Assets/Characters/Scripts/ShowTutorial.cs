using UnityEngine;
using System.Collections;

public class ShowTutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Fire1"))
	    {
	        this.gameObject.SetActive(false);
	    }
	
	}
}
