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
    private GameObject chargedIcon;
    public float nutrinetSPawnBreak;
    private MeshRenderer hostMeshRenderer;

    [SerializeField] private Material plDepleted; // dark green color

    /// <summary>
    /// triing to colide the mushroom with the tile it will be on to acces its stats
    /// </summary>
    /// <param name="collision"></param>
    /// 


    private void Start()
    {

        forestCap = 1;
        loopCap = 3;

        chargedIcon = Instantiate(nutrientSpawn, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2f,
            gameObject.transform.position.z), Quaternion.identity); // spawn icon
        chargedIcon.SetActive(false);

        StartCoroutine(wait(nutrinetSPawnBreak));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "MousePointer" && charged)
        {
            charged = false;
            collision.gameObject.GetComponent<Economy>().addNutrients(10);
            
            chargedIcon.SetActive(false);
            StartCoroutine(wait(nutrinetSPawnBreak));

        }
        else if (collision.gameObject.name == "Tile(Clone)")
        {
            hostTile = collision.gameObject;
           // print(collision.gameObject.GetComponent<TileIdentity>().tileType);
            if (collision.gameObject.GetComponent<TileIdentity>().tileType == "PlForest")
            {
                print("forestIsRich");
                loopCap = forestCap;
            }
            else
            {
                collision.gameObject.GetComponent<TileIdentity>().tileType = "PlInfected";

            }

        }
    }


    IEnumerator wait(float wait)
    {

        if (loopCap <= 0)
        {
            if (hostTile.GetComponent<TileIdentity>().tileType == "PlForest") //  remove the forest from the tile once
                                                                              //  depleated
            {
                hostTile.GetComponent<TileIdentity>().clearForest();
            }
            hostMeshRenderer = hostTile.GetComponent<MeshRenderer>();
            hostTile.GetComponent<TileIdentity>().tileType = "PlDeplited";
            hostMeshRenderer.material = new Material(plDepleted);
            hostTile.GetComponent<TileIdentity>().originalCl = new Material (plDepleted);
            
            Destroy(gameObject);
            
        }

        yield return new WaitForSeconds(wait+ Random.Range(-2f,5f)); // pause and wait



        if (loopCap > 0 && !charged) // stop producing spores on that tile to reduce lag
        {
            loopCap--;
            chargedIcon.SetActive(true);
            charged = true;

        }
        



    }
    private void growAgain()
    {
        StartCoroutine(wait(5));

    }

}


    