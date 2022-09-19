using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchTutorialBehavior : MonoBehaviour
{
    public Sprite crouching;
    public Sprite standing;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoCrouch());
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Crouch()
    {
        
    }

    IEnumerator AutoCrouch()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            sr.sprite = crouching;
            bc.size = new Vector2(4.4f, 1.7f);
            yield return new WaitForSeconds(2f);
            sr.sprite = standing;
            bc.size = new Vector2(3.8f, 2.6f);

        }
    }
}
