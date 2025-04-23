/*****************************************************************************
// File Name: EnemyGrowthSpore
// Author: Cristian Lesco
// Creation Date: March 27th, 2025
//
// Description: The code that allows spores to colonize a tile if it is empty
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExpands : MonoBehaviour
{
    private float result;
    [SerializeField] private GameObject enemyTile;
    public int sporeLifeTime;
    private bool alreadyColided;
    public int radious;

    /// <summary>
    /// Upon the start of this script the spore will be launched in a random location with certain boundaries
    /// </summary>
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        // Create a random velocity in 3D space with random signs on all axes (X, Y, Z)
        rb.velocity = new Vector3(radious * randomSign(), 10, radious * randomSign());
        StartCoroutine(wait(sporeLifeTime)); // start the count down after which the spore will destroy itself
    }

    /// <summary>
    /// picks a random float in the range, used for randomizing the location and speed of the launch
    /// </summary>
    /// <returns> random float</returns>
    private float randomSign()
    {
        result = Random.Range(-0.5f, 0.6f);
        result = Random.Range(-0.5f, 0.6f); // caled a 2nd time to avoid the first number beeing the same
        return result;
    }

    /// <summary>
    /// if touching an empty tile it gets infected by the enemy
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Tile(Clone)" ) // if right pointer on a tile
        {
            if (collision.gameObject.GetComponent<TileIdentity>().busy == false && !alreadyColided)
            {

                if (collision.gameObject.GetComponent<TileIdentity>().tileType == "forest")
                {
                    collision.gameObject.GetComponent<TileIdentity>().tileType = "EnemyForest";
                }
                else
                {
                    collision.gameObject.GetComponent<TileIdentity>().tileType = "EnemyInfected";
                }

                collision.gameObject.GetComponent<TileIdentity>().busy = true;
                GameObject spawn = Instantiate(enemyTile, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 0.5f,
                    collision.gameObject.transform.position.z), Quaternion.identity); // spawn enemy mushrooms

                collision.gameObject.GetComponent<TileIdentity>().enemyLink = spawn; // stores the spwaned mushrooms in tole identity so it can be deleated in tile identity
                Destroy(gameObject); // destroy spore aftre the forest was spawned

            }
            else if (collision.gameObject.GetComponent<TileIdentity>().tileType == "PlInfected" || collision.gameObject.
                GetComponent<TileIdentity>().tileType == "PlForest") // if player tile call "destroyPl" from TileIdentity
            {
                alreadyColided = true;

                collision.gameObject.GetComponent<TileIdentity>().destroyPlayer(); // a function in TileIdentity
                Destroy(gameObject); // destroy spore aftre the forest was spawned
            }
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
