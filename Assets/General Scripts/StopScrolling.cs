using UnityEngine;
using System.Collections;

public class StopScrolling : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var control = other.GetComponent<ForestPlayerMovement>();
            control.canScroll = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var control = other.GetComponent<ForestPlayerMovement>();
            control.canScroll = false;
        }
    }
}
