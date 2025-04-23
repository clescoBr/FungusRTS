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
    /// start lvl 2
    /// </summary>
    public void startLvl1()
    {
        SceneManager.LoadScene("FieldScene");
    }

    /// <summary>
    /// start lvl 2
    /// </summary>
    public void startLvl2()
    {
        SceneManager.LoadScene("FieldScene2");
    }

    /// <summary>
    /// start lvl3
    /// </summary>
    public void startLvl3()
    {
        SceneManager.LoadScene("FieldScene3");
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
