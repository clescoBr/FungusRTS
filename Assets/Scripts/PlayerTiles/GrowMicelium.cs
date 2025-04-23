/*****************************************************************************
// File Name: GrowMycelium
// Author: Cristian Lesco
// Creation Date: March 29st, 2025
//
// Description: Growth Animation of player mushrooms
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadMycelium : MonoBehaviour
{
    [SerializeField] private float growDuration; // time before the next transformation
    [SerializeField] GameObject mushroomColony;
    public GameObject hostTiles;

    /// <summary>
    /// rotates it in the right position and initialize the growth animation
    /// </summary>
    void Start()
    {
        gameObject.transform.Rotate(90, 0, 0); // rotate the mycelium at the correct angle
        growDuration = 4; 

        StartCoroutine(wait(growDuration));
    }

    /// <summary>
    /// wait for so much time then grow in size, at the end spawn a mushroom colony and disapear
    /// </summary>
    IEnumerator wait(float wait)
    {
        yield return new WaitForSeconds(wait / 4);
        gameObject.transform.localScale = new Vector3(4, 4, 0.4146062f); // grow in size
        yield return new WaitForSeconds(wait / 4);
        gameObject.transform.localScale = new Vector3(6, 6, 0.4146062f);// grow in size
        yield return new WaitForSeconds(wait / 2);
        GameObject spawn = Instantiate(mushroomColony, new Vector3(gameObject.transform.position.x, 
            gameObject.transform.position.y-0.5f, gameObject.transform.position.z), Quaternion.identity);
        hostTiles.GetComponent<TileIdentity>().playerLink = spawn; // stores the spwaned mushrooms in tole identity so it can be deleated in tile identity

        yield return new WaitForSeconds(wait / 4);// wait before deleating itself
        Destroy(gameObject);
    }
}
