/*****************************************************************************
// File Name: MenuControler
// Author: Cristian Lesco
// Creation Date: March 31st, 2025
//
// Description: Controls the scenes in the game
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{

    /// <summary>
    /// start the game itself
    /// </summary>
    public void startLvl1()
    {
        SceneManager.LoadScene("FieldScene");
    }


    /// <summary>
    /// load main menu
    /// </summary>
    public void toMenu() 
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// load tutorial
    /// </summary>
    public void toTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
