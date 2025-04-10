/*****************************************************************************
// File Name: MapGeneration
// Author: Cristian Lesco
// Creation Date: March 5th, 2025
//
// Description: Generates the map and its terrain
*****************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    // map size
    [SerializeField] private int length = 40;
    [SerializeField] private int width = 30;
    public GameObject[,] map = new GameObject[40, 30];// !make the map, dont forget to update the size in TileIDentity!   !!!
    // change the sized in the if statemnts to prevent out of bounds indexes

    [SerializeField] private GameObject Overmind; // Player Spawn
    [SerializeField] private GameObject EnemyTile; // Enemy Spawn


    
    [SerializeField] private GameObject tile;

    /// <summary>
    /// Generates the tiles, stores them in a 2-dimensional array, calls the functions that spawn the player and enemy
    /// </summary>
    void Start()
    {


        //generates the tiles of the map
        for (int i = 0; i < map.GetLength(0); i++) // Loop through rows
        {
            for (int j = 0; j < map.GetLength(1); j++) // Loop through columns
            {
                // Instantiate a new GameObject at a specific position in the world
                GameObject copyTile = Instantiate(tile, new Vector3(i * 5.0f, 0, j * 5.0f), Quaternion.identity);

                copyTile.GetComponent<TileIdentity>().indexX = i;
                copyTile.GetComponent<TileIdentity>().indexY = j;


                // Assign the newly instantiated GameObject to the array
                map[i, j] = copyTile;
            }
        }
        spawnEnemy();

        spawnPl();

    }

    /// <summary>
    /// 
    /// </summary>
    public void spawnPl()
    {
        GameObject spawnTile = map[Random.Range(1, length - 1), Random.Range(1, width - 1)]; // get a random tile

        spawnTile = map[Random.Range(1, 40), Random.Range(1, 30)]; // get random numbers the 2nd time for more random


        if (spawnTile.GetComponent<TileIdentity>().busy == false ) // if the selected tile is empty 
        {
            spawnTile.GetComponent<TileIdentity>().tileType = "Overmind"; // spawn player
            spawnTile.GetComponent<TileIdentity>().busy = true; // mark the tile as busy

            GameObject spawn = Overmind;


            spawn.transform.position = new Vector3(spawnTile.transform.position.x, spawnTile.transform.position.y + 1,
                spawnTile.transform.position.z); // raises the overlord above the tile level so they don't colide
        }
        else
        {
            spawnPl(); // if the tile is not empty, pick a new one

        }
    }

    /// <summary>
    /// Does the same thing as the spawnPl() just for an enemy so comment not needed
    /// </summary>
    public void spawnEnemy()
    {
        GameObject spawnTile = map[Random.Range(1, length - 1), Random.Range(1, width - 1)];

        spawnTile = map[Random.Range(1, 40), Random.Range(1, 30)]; // get random numbers the 2nd time for more random


        if (spawnTile.GetComponent<TileIdentity>().busy == false )
        {

            // GameObject spawn = Instantiate(Overmind, spawnTile.transform.position, Quaternion.identity);
            spawnTile.GetComponent<TileIdentity>().tileType = "EnemyTile";
            spawnTile.GetComponent<TileIdentity>().busy = true;

            GameObject copyTile = Instantiate(EnemyTile, new Vector3(spawnTile.transform.position.x, 
                spawnTile.transform.position.y + 1,spawnTile.transform.position.z), Quaternion.identity);

        }
        else
        {

            spawnEnemy(); // pick a new tile if last one is busy

        }
    }


}
