/*****************************************************************************
// File Name: Economy
// Author: Cristian Lesco
// Creation Date: March 30th, 2025
//
// Description: KeepsTrack of the player's resources
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class Economy : MonoBehaviour
{
    public int PlNutrients; // the only resource in the game
    public int tilesAsimilated; // the only resource in the game

    public TMP_Text NutrientText;

    /// <summary>
    /// Declares the resource variable
    /// </summary>
    private void Start()
    {
        NutrientText.text = "Nutrients: " + PlNutrients.ToString();
    }

    /// <summary>
    /// add nutrients to the public variable
    /// </summary>
    public void addNutrients(int amount)
    {
        PlNutrients += amount;
        updateNutrients();
    }
    public void assimilated()
    {
        tilesAsimilated ++;
        print("+");
        if (tilesAsimilated >= 100)
        {
            print("won!");
            SceneManager.LoadScene("WinScreen");
        }
     
    }
    /// <summary>
    /// update the text of the variable (used not only for increasing the nutrients so in a different function
    /// </summary>
    public void updateNutrients()
    {
        NutrientText.text = "Nutrients: " + PlNutrients.ToString();
    }
}
