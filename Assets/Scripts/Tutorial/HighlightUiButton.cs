/*****************************************************************************
// File Name: HighlightUiButton
// Author: Cristian Lesco
// Creation Date: May 8th, 2025
//
// Description: Makes the first button in the control panel to be highlighted during the tutorial
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightUiButton : MonoBehaviour
{
    // Start is called before the first frame update
    public int button; // a variable that will be used to determine what button must be highlighted
    public int loopCount;


    [SerializeField] GameObject button1;
    void Start()
    {       
        loopCount = 4;     // gives a initial loop count that will be used if not changed otherwise
    }

    /// <summary>
    ///  wait, light the button up then call the next courutine
    /// </summary>
    IEnumerator Light(float wait)
    {
        yield return new WaitForSeconds(wait); // pause and wait
        button1.GetComponent<RawImage>().enabled = true;

        StartCoroutine(Close(wait));       
    }

    /// <summary>
    ///   wait,turn of the light than check if there should be a next loop
    /// </summary>
    IEnumerator Close(float wait)
    {
        yield return new WaitForSeconds(wait); // pause and wait
        button1.GetComponent<RawImage>().enabled = false;
        if (loopCount > 0)
        {
            LoopLight(loopCount);
            loopCount -= 1;
        }     
    }

    /// <summary>
    /// the public function that starts the loop
    /// </summary>

    public void LoopLight(int lp)
    {        
        loopCount = lp;
        StartCoroutine(Light(0.5f));           
    }

    /// <summary>
    /// a public function that sops the highlighting
    /// </summary>
    public void StopLoop()
    {
        button1.gameObject.SetActive(false);
    }
}
