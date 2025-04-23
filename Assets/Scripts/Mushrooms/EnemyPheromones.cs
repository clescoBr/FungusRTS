/*****************************************************************************
// File Name: EnemyGrowthSpore
// Author: Cristian Lesco
// Creation Date: April 20th, 2025
//
// Description: the controler of a invisible radius that activates all enemies in its surroundings
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPheromones : MonoBehaviour
{
    /// <summary>
    /// make it move up than down to ensure the colision with evry tile
    /// </summary>
    void Start()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y +4f, gameObject.transform.position.z); // move up
        for (int i = 0; i < 20; i++)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y - 0.2f, gameObject.transform.position.z); // move down
        }
        Destroy(gameObject); // then deleate it
    }
}
