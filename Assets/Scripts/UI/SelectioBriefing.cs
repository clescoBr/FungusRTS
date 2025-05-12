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
    [SerializeField] private Texture Sporicite;
    [SerializeField] private Texture GiantAcid;
    [SerializeField] private Texture Spire;
    [SerializeField] private Texture SmallAcid;

    /// <summary>
    /// sees what button was presed and changes to the coresponding texture
    /// </summary>

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
        else if (index == 2)
        {
            gameObject.GetComponent<RawImage>().texture = Spire;

        }
        else if (index == 3)
        {
            gameObject.GetComponent<RawImage>().texture = SmallAcid;

        }
        else if (index == 4)
        {
            gameObject.GetComponent<RawImage>().texture = GiantAcid;
        }
    }
}
