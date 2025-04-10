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

public class Economy : MonoBehaviour
{
    public int PlNutrients; // the only resource in the game
    public TMP_Text NutrientText;

    private void Start()
    {
        NutrientText.text = "Nutrients: " + PlNutrients.ToString();
    }

    public void addNutrients(int amount)
    {
        PlNutrients += amount;
        updateNutrients();
    }

    public void updateNutrients()
    {
        NutrientText.text = "Nutrients: " + PlNutrients.ToString();

    }

}
