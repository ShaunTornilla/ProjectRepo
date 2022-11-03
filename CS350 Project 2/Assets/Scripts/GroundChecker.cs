using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool grounded = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor") || collision.CompareTag("Enemy"))
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Floor") || collision.CompareTag("Enemy"))
        {
            grounded = false;
        }
    }

    
}
