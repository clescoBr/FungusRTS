/*****************************************************************************
// File Name: SelectionBriefing
// Author: Cristian Lesco
// Creation Date: April 28th, 2025
//
// Description: Shows what button is clicked at the moment
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectioBriefing : MonoBehaviour
{
    public Texture Sporicite;

    public void changeImage(int index)
    {
        if (index == 0)
        {
            gameObject.GetComponent<RawImage>().texture = null;
        }
        else if (index == 1)
        {
            gameObject.GetComponent<RawImage>().texture = Sporicite;
        }
    }
}
