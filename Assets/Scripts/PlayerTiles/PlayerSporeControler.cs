/*****************************************************************************
// File Name: PlayerSporeControler
// Author: Cristian Lesco
// Creation Date: March 30st, 2025
//
// Description: Control the player spores that fly in random position and infect a tyle if it is empty, then disapear
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSporeControler : MonoBehaviour
{
    private float result;
    [SerializeField] private GameObject playerTile;
    public int sporeLifeTime;

    /// <summary>
    /// Upon the start of this script the spore will be launched in a random location with certain boundaries
    /// </summary>
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        // Create a random velocity in 3D space with random signs on all axes (X, Y, Z)
        rb.velocity = new Vector3(5 * randomSign(), 10, 5 * randomSign());
        StartCoroutine(wait(sporeLifeTime)); // start the count down after which the spore will destroy itself
    }

    /// <summary>
    /// picks a random float in the range, used for randomizing the location and speed of the launch
    /// </summary>
    /// <returns> random float</returns>
    private float randomSign()
    {

        result = Random.Range(-1f, 1.1f);
        result = Random.Range(-1f, 1.1f); // caled a 2nd time to avoid the first number beeing the same
        return result;

    }

    /// <summary>
    /// if touching an empty tile it gets infected by the enemy
    /// </summary>

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Tile(Clone)" && collision.gameObject.
            GetComponent<TileIdentity>().busy == false) // if right pointer on a tile
        {


            if (collision.gameObject.GetComponent<TileIdentity>().tileType == "forest")
            {
                collision.gameObject.GetComponent<TileIdentity>().tileType = "PlForest";
            }
            else
            {
                collision.gameObject.GetComponent<TileIdentity>().tileType = "PlInfected";

            }


            collision.gameObject.GetComponent<TileIdentity>().busy = true;
            GameObject spawn = Instantiate(playerTile, new Vector3(collision.gameObject.transform.position.x, 
                collision.gameObject.transform.position.y + 0.5f,collision.gameObject.transform.position.z), 
                Quaternion.identity); // spawn enemy mushrooms
            Destroy(gameObject); // destroy spore aftre the forest was spawned

        }
    }

    /// <summary>
    ///  count down to the self destruction
    /// </summary>
    IEnumerator wait(float wait)
    {
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);

    }
}
