using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractChecker : MonoBehaviour
{
    public bool interacted = false;
    public bool pressed;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interact") && pressed)
        {
            Debug.Log("Player Touching Tree.");
            //Debug.Log("Player Interacted with Object");
            interacted = true;
        }
    }

    public void Reset()
    {
        interacted = false;
        pressed = false;
    }


}
