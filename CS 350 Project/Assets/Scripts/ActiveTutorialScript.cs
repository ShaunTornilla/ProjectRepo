using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActiveTutorialScript : MonoBehaviour
{

    public Text tutorialText;
    public GameObject tutorialTextObject;

    private bool jumpTutorial = false;
    private bool crouchTutorial = false;
    private bool basicTutorial = false;
    private bool finishTutorial = false;
    private TutorialPlayerBehavior tp;
    // Start is called before the first frame update
    private void Start()
    {
        tp = GameObject.FindObjectOfType<TutorialPlayerBehavior>();
        basicTutorial = true;
        tutorialText.text = "Your hearts on the bottom right signify your current hp, everytime you run into an obstacle you lose a heart.\nLose all 3 and its game over.\n Press space to continue";
        tutorialTextObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if(jumpTutorial||crouchTutorial||basicTutorial)
        {
            Time.timeScale = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("trigger entered");
        GameObject collider = collision.gameObject;
        //Debug.Log(collider.tag);
        if(collider.CompareTag("CrouchingTutorial"))
        {   
            tp.TutorialCrouchAllowed = true;
            crouchTutorial = true;
            tutorialText.text = "Along with jumping, could also slide under an object to dodge it.\nPress S or down to slide";
            tutorialTextObject.SetActive(true);
            Time.timeScale = 0f;
        }

        if(collider.CompareTag("JumpTutorial"))
        {
            tp.TutorialJumpAllowed = true;
            jumpTutorial = true;
            tutorialText.text = "Jump over obstacles to dodge them.\nPress W or up to jump";
            tutorialTextObject.SetActive(true);
            
            Time.timeScale = 0f;
        }

        if(collider.CompareTag("CollectableTutorial"))
        {
            basicTutorial = true;
            tutorialText.text = "Collect cat food boxes to gain extra points.\nPress space to continue";
            tutorialTextObject.SetActive(true);
            Time.timeScale = 0f;
        }

        if(collider.CompareTag("EndTutorial"))
        {
            basicTutorial = true;
            finishTutorial = true;
            tutorialText.text = "The goal of the game is to reach your cat bed and collect as many collectables as you can on the way.\n Good Luck\nPress space to start the game";
            tutorialTextObject.SetActive(true);
            Time.timeScale = 0f;
        }

        
    }

    private void OnJump()
    {
        if (jumpTutorial)
        {
            Time.timeScale = 1f;
            jumpTutorial = false;
            tutorialTextObject.SetActive(false);
        }
    }

    private void OnCrouch()
    {
        if(crouchTutorial)
        {
            Time.timeScale = 1f;
            crouchTutorial = false;
            tutorialTextObject.SetActive(false);
        }
    }

    private void OnContinue()
    {
        if(basicTutorial)
        {
            Time.timeScale = 1f;
            if(finishTutorial)
            {
                SceneManager.LoadScene("Game");
            }
            else
            {
                basicTutorial = false;
                tutorialTextObject.SetActive(false);
            }
        }
    }    
}
