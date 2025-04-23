/*****************************************************************************
// File Name: AdultMushroomsScript
// Author: Cristian Lesco
// Creation Date: March 18th, 2025
//
// Description: Will be used to generate resources
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultMushroomsScript : MonoBehaviour
{
    public GameObject nutrientSpawn;
    private GameObject hostTile; // the tile mushroom is curently on
    private int loopCap;
    private int forestCap;
    private bool charged;
    public GameObject chargedIcon;
    private GameObject mousePointer;
    public float nutrinetSPawnBreak;
    private MeshRenderer hostMeshRenderer;

    [SerializeField] private Material plDepleted; // dark green color

    /// <summary>
    /// triing to colide the mushroom with the tile it will be on to acces its stats
    /// </summary>
    private void Start()
    {
        forestCap = 5;
        loopCap = 3;

        chargedIcon = Instantiate(nutrientSpawn, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3.5f,
            gameObject.transform.position.z), Quaternion.identity); // spawn icon

        StartCoroutine(wait(nutrinetSPawnBreak)); // start the loop of generating resources
    }

    /// <summary>
    /// if colided with a pressed mouse collect the resources and start generating them again
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "MousePointer" && charged)
        {
            mousePointer = collision.gameObject;
            charged = false;
            collision.gameObject.GetComponent<Economy>().addNutrients(10);

            //  chargedIcon.SetActive(false);
            chargedIcon.transform.position = new Vector3(chargedIcon.transform.position.x,
                chargedIcon.transform.position.y - 5, chargedIcon.transform.position.z); // make the icon disaper under the map
            StartCoroutine(wait(nutrinetSPawnBreak));
        }
        else if (collision.gameObject.name == "Tile(Clone)") // when colided with the host tie save it as a reference
        {
            hostTile = collision.gameObject;
           // print(collision.gameObject.GetComponent<TileIdentity>().tileType);
            if (collision.gameObject.GetComponent<TileIdentity>().tileType == "PlForest")
            {
                loopCap = forestCap;
            }
            else
            {
                collision.gameObject.GetComponent<TileIdentity>().tileType = "PlInfected";
            }
        }
    }

/// <summary>
/// The random cooldown till the next harvest, also check if it reached the harvest limit yet
/// </summary>
    IEnumerator wait(float wait)
    {
        if (loopCap <= 0)
        {
            if (hostTile.GetComponent<TileIdentity>().tileType == "PlForest") //  remove the forest from the tile once depleated                                                                            //  
            {
                                                                    // (win condition is x amount of tiles)
                hostTile.GetComponent<TileIdentity>().clearForest();
            }
            hostMeshRenderer = hostTile.GetComponent<MeshRenderer>();
            hostTile.GetComponent<TileIdentity>().tileType = "PlDeplited";
            hostMeshRenderer.material = new Material(plDepleted);
            hostTile.GetComponent<TileIdentity>().originalCl = new Material (plDepleted);

            mousePointer.GetComponent<Economy>().assimilated(); // increase the amount of tiles assimilated

            Destroy(gameObject);            
        }
        yield return new WaitForSeconds(wait+ Random.Range(-2f,5f)); // pause and wait

        if (loopCap > 0 && !charged) // stop producing spores on that tile to reduce lag
        {
            loopCap--;
            chargedIcon.transform.position = new Vector3(chargedIcon.transform.position.x,
                chargedIcon.transform.position.y + 5, chargedIcon.transform.position.z); // make the icon emerge again
            charged = true;
        }       
    }
    /// <summary>
    /// steping stone to continue the loop
    /// </summary>
    private void growAgain()
    {
        StartCoroutine(wait(5));

    }
}


    