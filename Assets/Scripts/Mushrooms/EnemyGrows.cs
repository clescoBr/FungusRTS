/*****************************************************************************
// File Name: EnemyGrows
// Author: Cristian Lesco
// Creation Date: March 25th, 2025
//
// Description: Enemy tiles spawn spores, give random velocity
*****************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    [SerializeField] private GameObject growThSpore;
    public bool boostedGrowth;// if it is the first tile give it a boost in expanding (future balance puposes)
    [SerializeField] private float timeBetweenSpores;
    private int loopCap; // the maximum amount of loops a sporicite can get throu


    /// <summary>
    /// Initializez the infinite loop of spawning spores
    /// </summary>
    void Start()
    {
        loopCap = 6;
      if (boostedGrowth) // for now every enemy teritory has boosted growth true (debug in the future)
        {
            StartCoroutine(wait(timeBetweenSpores));// initialize the loop 
        }  
    }
    /// <summary>
    /// wait for the time specified, then spawn a spore, repeat 3 times, call the function that will resume th eloop
    /// </summary>
    /// <param name="wait"></param>
    /// <returns></returns>
    IEnumerator wait(float wait)
    {
        yield return new WaitForSeconds(wait ); // pause and wait
        GameObject spawn = Instantiate(growThSpore, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2f,
            gameObject.transform.position.z), Quaternion.identity); // spawn spore
        yield return new WaitForSeconds(wait );
         spawn = Instantiate(growThSpore, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2f,
            gameObject.transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(wait );
         spawn = Instantiate(growThSpore, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2f,
            gameObject.transform.position.z), Quaternion.identity);

        if (loopCap > 0) // stop producing spores on that tile to reduce lag
        {
            loopCap--;
            growAgain(); // function will call the courutine again forming an infilite loop

        }



    }
    /// <summary>
    /// calls the courutine again to resume the growth of the mushrooms
    /// </summary>
    private void growAgain()
    {
        StartCoroutine(wait(timeBetweenSpores));

    }
}
