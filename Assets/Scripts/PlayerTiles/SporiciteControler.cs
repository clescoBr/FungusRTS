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
    [SerializeField] private GameObject growThSpore; // the spore itself
    [SerializeField] private float timeBetweenSpores; // atack speed
    [SerializeField] private float distPower; // multiply the distance of a spore

    
    public int loopCap; // the maximum amount of loops a sporicite can get throu
    [SerializeField] private GameObject hostTile;
    public bool sporicite;

    /// <summary>
    /// rotate it so it points up and start the loop of launching spores
    /// </summary>
    void Start()
    {
        if (sporicite) // if it is sporicite then rotate
        {
            gameObject.transform.Rotate(-90, 0, 0); // rotate the sporicite at the correct angle
        }     
        StartCoroutine(wait(timeBetweenSpores));// initialize the loop     
    }

    /// <summary>
    /// courutine that spawns 3 spores and launches them in different directions
    /// </summary>
    IEnumerator wait(float wait)
    {
        for (int i = 0; i < 3; i++)
        {
            {
                yield return new WaitForSeconds(wait); // pause and wait
                if (!sporicite)
                {

                    GameObject spawn = Instantiate(growThSpore, new Vector3(gameObject.transform.position.x,
                           gameObject.transform.position.y + 7f, gameObject.transform.position.z), Quaternion.identity); // spawn spore
                }
                else
                {
                    GameObject spawn = Instantiate(growThSpore, new Vector3(gameObject.transform.position.x,
                        gameObject.transform.position.y + 3f, gameObject.transform.position.z), Quaternion.identity); // spawn spore
                }
            }
        }

        if (loopCap > 0) // if loop limit not reached repeat it again
        {
            loopCap--;
            growAgain(); // function will call the courutine again forming an infilite loop
        }
        else 
        {
            print("4" + hostTile);
            hostTile.GetComponent<TileIdentity>().tileType = "PlInfected"; // return the property to a field
                                                                           // 
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
        if (!hostTile)
        {
            hostTile = tile;
            print("3" + tile);
            print("3.5" + hostTile);
        }        
    }
}
