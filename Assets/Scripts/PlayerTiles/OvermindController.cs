/*****************************************************************************
// File Name: OvermindControler
// Author: Cristian Lesco
// Creation Date: March 13th, 2025
//
// Description: Controls the player's starting base
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvermindController : MonoBehaviour
{
    public MapGeneration mapGeneration; // the empty object responsable for generating map

    public bool selected; // is the structure curently selected
    [SerializeField] private GameObject mousePointer; // left click mouse pointer
    public GameObject growThSpore;

    private Vector3 originalSize;

    /// <summary>
    /// set up variables
    /// </summary>
    void Start()
    {
        mapGeneration = GameObject.FindAnyObjectByType<MapGeneration>();
        originalSize = transform.localScale;
        StartCoroutine(wait());// spawns the first spores
    }

    /// <summary>
    /// if the overlord colides with the left pointer indicator make it bigger and mark as selected
    /// </summary>
    /// <param name="collision"> the thing it colided with</param>
    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.name == "MousePointer" ) 
        {
            selected = true;
            transform.localScale = new Vector3(3,3,3);
        }   
    }

    /// <summary>
    /// if the overlord colision ends with the left pointer indicator change it to original size and mark as delected
    /// </summary>
    /// <param name="collision"> the thing it colided with</param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "MousePointer")
        {
            selected = false;
            transform.localScale = originalSize;
        }
    }

    /// <summary>
    /// wait a litle then infect adjacent tiles to give the player initial income and a base for construction
    /// </summary>
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f); // pause and wait

        for (int i = 0; i < 20; i++) 
        {
            yield return new WaitForSeconds(0.3f); // pause and wait
            GameObject spawn = Instantiate(growThSpore, new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y + 3f, gameObject.transform.position.z), Quaternion.identity); // spawn spore
        }
    }
}
