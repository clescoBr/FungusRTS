/*****************************************************************************
// File Name: SporiciteControler
// Author: Cristian Lesco
// Creation Date: March 30th, 2025
//
// Description: Spreads player spores
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporiciteControler : MonoBehaviour
{
    [SerializeField] private GameObject growThSpore;
    [SerializeField] private float timeBetweenSpores;

    private int loopCap; // the maximum amount of loops a sporicite can get throu
    private GameObject hostTile;

    /// <summary>
    /// rotate it so it points up and start the loop of launching spores
    /// </summary>
    void Start()
    {
        gameObject.transform.Rotate(-90, 0, 0); // rotate the sporicite at the correct angle
        StartCoroutine(wait(timeBetweenSpores));// initialize the loop 
        loopCap = 5;
    }

    /// <summary>
    /// courutine that spawns 3 spores and launches them in different directions
    /// </summary>
    IEnumerator wait(float wait)
    {
        yield return new WaitForSeconds(wait); // pause and wait
        GameObject spawn = Instantiate(growThSpore, new Vector3(gameObject.transform.position.x, 
            gameObject.transform.position.y + 3f,gameObject.transform.position.z), Quaternion.identity); // spawn spore
        yield return new WaitForSeconds(wait);
        spawn = Instantiate(growThSpore, new Vector3(gameObject.transform.position.x, 
            gameObject.transform.position.y + 3f,gameObject.transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(wait);
        spawn = Instantiate(growThSpore, new Vector3(gameObject.transform.position.x, 
            gameObject.transform.position.y + 3f,gameObject.transform.position.z), Quaternion.identity);

        if (loopCap > 0) // if loop limit not reached repeat it again
        {
            loopCap--;
            growAgain(); // function will call the courutine again forming an infilite loop
        }
        else 
        {
            hostTile.GetComponent<TileIdentity>().tileType = "PlInfected"; // return the property to a field
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// calls the courutine again to resume the growth of the mushrooms
    /// </summary>
    private void growAgain()
    {
        StartCoroutine(wait(timeBetweenSpores));
    }
    public void setHostTile(GameObject tile)
    {
        hostTile = tile;
    }
}
