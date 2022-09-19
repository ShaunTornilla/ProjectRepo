using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTutorialVisual : MonoBehaviour
{
    private float jumpforce = 10f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoJump());
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
    }

    IEnumerator AutoJump()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            Jump();
        }
    }
}
