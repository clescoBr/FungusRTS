/*****************************************************************************
// File Name: TutorialControler
// Author: Cristian Lesco
// Creation Date: May 8th, 2025
//
// Description: THe code that controls the flow of the tutorial
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControler : MonoBehaviour
{
    public TMP_Text Task;
    private int progress; // the steps of tutorial, every number is the next step 
    [SerializeField] GameObject ping; // object that will start highlighting the Sporicite button
    [SerializeField] GameObject enemySpore;
    [SerializeField] GameObject enemyLoc; // the locationg where the enemy will apear

    void Start()
    {
        progress = -1;
        Task.text = "You are a sentient fungus in a lab";

        StartCoroutine(Advance(5));
        //Task.text = "Task: " + "Click the 1st button then right click on a captured tile";
    }

    /// <summary>
    /// checks what must be done at this step in the tutorial
    /// </summary>
    public void nextStep()
    {
        switch (progress)
        {
            case 0:
                Task.text = "Navigate by WASD and zoom in with the mouse wheel, R = Menu  P = pause  Q = exit";
                StartCoroutine(Advance(6));
            break;

            case 1:
                Task.text = "Now that you have a starting point let's expand our territory";
                StartCoroutine(Advance(5));
            break;

            case 2:
                Task.text = "Select the first button that is being highlighted, then right click on a colonized tile";            
                ping.GetComponent<HighlightUiButton>().LoopLight(100);              
            break;

            case 3:
                Task.text = "Awesome this is a sporicite, it will capture tiles and destroy enemy mushrooms";
                StartCoroutine(Advance(5));
                ping.GetComponent<HighlightUiButton>().StopLoop();
            break;

            case 4:
                Task.text = "You need nutrients to build more, click and drag over your tiles that have a yellow circle to harvest 150";
            break;

            case 5:
                Task.text = "Forests have more nutrients available, a depleted tile is blue and can't be stolen from you";
                StartCoroutine(Advance(5));
            break;

            case 6:
                Task.text = "Here is the enemy, try to destroy it with your sporicites, hint you will fail ;)";
                Instantiate(enemySpore, enemyLoc.transform.position, Quaternion.identity);
                StartCoroutine(Advance(5));
            break;

            case 7:
                Task.text = "Press R when ready to choose the next level, deplete 250 tiles to win";
                Instantiate(enemySpore, enemyLoc.transform.position, Quaternion.identity);
            break;
        }
    }

    /// <summary>
    ///  go to the next step
    /// </summary>

    IEnumerator Advance(float wait)
    {
        yield return new WaitForSeconds(wait); // pause and wait, give time for player to read
        progress += 1;
        nextStep();
    }

    /// <summary>
    /// step 2 requires the player to place a sporicite
    /// </summary>
    public void sporicitePlaced()
    {
        if(progress == 2)
        {
            progress = 3;
            nextStep();
        }
    }

    /// <summary>
    /// step 4 requires player to gather a good amount of nutrients
    /// </summary>
    public void resourcesGathered()
    {
        if (progress == 4)
        {
            progress = 5;
            nextStep();
        }
    }
}
