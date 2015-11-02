using System;
using UnityEngine;

namespace Assets
{
    public class CameraScript : MonoBehaviour
    {
        public float maxXPos;
        public float minXPos;
        public float maxYPos;
        public float minYPos;
        public float panSpeed;
        public Transform cameraTarget;
        private Camera cam;


        // Use this for initialization
        void Start()
        {
            // Can change cam properties with code.
            cam = GetComponent<Camera>();


        }

        // Update is called once per frame
        void Update()
        {
            this.gameObject.transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, Mathf.Clamp(cameraTarget.position.x, minXPos, maxXPos), panSpeed),
                Mathf.Lerp(transform.position.y, Mathf.Clamp(cameraTarget.position.y, minYPos, maxYPos), panSpeed),
                -10);

        }

    }
}
