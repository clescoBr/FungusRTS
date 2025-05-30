/*****************************************************************************
// File Name: TileIdentity
// Author: Cristian Lesco
// Creation Date: March 10th, 2025
//
// Description: Tracks mouse clicks and the actions that would be taken
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileIdentity : MonoBehaviour
{
    public bool busy; // tile already ocupied
    private MeshRenderer meshRenderer;

    private int randomDraw; // variable that will store a random number
    public bool selected; // is the tile selected?
    //public Color originalCl; // what color was the tile before beein changed or selected
    public Material originalCl; // what color was the tile before beein changed or selected

    [SerializeField] private Material forest; // dark green color
    public string tileType; // what type of tile is that

    [SerializeField] public int indexX;
    [SerializeField] public int indexY;
    [SerializeField] bool tutorial;

    [SerializeField] private GameObject forestCenter; // forest that is spawned on top of a tile

    private GameObject forestCopy;
    public GameObject enemyLink; // if infected this wil be the enemy forest 
    public GameObject playerLink; // if infected this wil be the enemy forest 

    [SerializeField] GameObject defensePheromones;// will reactivate growth for nearby mushrooms
    public MapGeneration mapGeneration; // teh empty object responsable for generating map

    /// <summary>
    /// generating the map and setting up the variables
    /// </summary>
    void Start()
    {
        mapGeneration = GameObject.FindAnyObjectByType<MapGeneration>();
        originalCl = gameObject.GetComponent<MeshRenderer>().material;
        
        meshRenderer = GetComponent<MeshRenderer>(); // gets the phisical proprieties of the tile
        busy = false; // the tile is not empty field (forest/ resource / building)

        if (!tutorial && !busy && tileType != "forest" && Random.Range(1, 26) == 15) // if field empty take a chance of mutating
        { 
            randomDraw = Random.Range(1, 11); // if mutated draw 1/10 to see what it will become (forest / rocks/ city)

            if (randomDraw == 1 || randomDraw == 2) // 2 out of 10 times it should be a forest
            {
                meshRenderer.material = new Material(forest); // change its color to dark green
                tileType = "forest";
                forestCopy = Instantiate(forestCenter, transform.position, Quaternion.identity);               
                mutateAround(0); // biger number = less forest
            }
        }   
    }

    /// <summary>
    /// if a tile became a forest, all nearby tiles have a chance aswell
    /// </summary>
    public void mutateAround(int generation)
    {
        mapGeneration = GameObject.FindAnyObjectByType<MapGeneration>();

        if (indexX < 39 && indexY < 29)  // tring to prevent geting an index outside of an array
        {
            GameObject closeTile = mapGeneration.map[indexX + 1, indexY + 1];
            if (closeTile.GetComponent<TileIdentity>().busy == false)
            {
                closeTile.GetComponent<TileIdentity>().changeNear(generation);
            }
        }

        //  checks if the nearby tile is busy, if not run function that that by a decreasing chance turns the tile in the same type
        if (indexX < 39)  // tring to prevent geting an index outside of an array
        {
            GameObject closeTile = mapGeneration.map[indexX + 1, indexY];
            if (closeTile.GetComponent<TileIdentity>().busy == false)
            {
                closeTile.GetComponent<TileIdentity>().changeNear(generation);
            }
        }

        //  checks if the nearby tile is busy, if not run function  that by a decreasing chance turns the tile in the same type
        if ( indexY < 29)  // tring to prevent geting an index outside of an array
        {
            GameObject closeTile = mapGeneration.map[indexX, indexY + 1];
            if (closeTile.GetComponent<TileIdentity>().busy == false)
            {
                closeTile.GetComponent<TileIdentity>().changeNear(generation);
            }
        }

        //  checks if the nearby tile is busy, if not run function that that by a decreasing chance turns the tile in the same type
        if (indexX > 1 )  // tring to prevent geting an index outside of an array
        {
            GameObject closeTile = mapGeneration.map[indexX - 1, indexY];
            if (closeTile.GetComponent<TileIdentity>().busy == false)
            {
                closeTile.GetComponent<TileIdentity>().changeNear(generation);
            }
        }

        //  checks if the nearby tile is busy, if not run function that that by a decreasing chance turns the tile in the same type
        if (indexY > 1)  // tring to prevent geting an index outside of an array
        {
            GameObject closeTile = mapGeneration.map[indexX, indexY - 1];
            if (closeTile.GetComponent<TileIdentity>().busy == false)
            {
                closeTile.GetComponent<TileIdentity>().changeNear(generation);
            }
        }

        //  checks if the nearby tile is busy, if not run function that that by a decreasing chance turns the tile in the same type
        if (indexX > 1 && indexY > 1)  // tring to prevent geting an index outside of an array
        {
            GameObject closeTile = mapGeneration.map[indexX - 1, indexY - 1];
            if (closeTile.GetComponent<TileIdentity>().busy == false)
            {
                closeTile.GetComponent<TileIdentity>().changeNear(generation);
            }
        }

        //  checks if the nearby tile is busy, if not run function that that by a decreasing chance turns the tile in the same type
        if (indexX > 1 && indexY < 29)  // tring to prevent geting an index outside of an array
        {
            GameObject closeTile = mapGeneration.map[indexX - 1, indexY + 1];
            if (closeTile.GetComponent<TileIdentity>().busy == false)
            {
                closeTile.GetComponent<TileIdentity>().changeNear(generation);
            }
        }

        //  checks if the nearby tile is busy, if not run function that that by a decreasing chance turns the tile in the same type
        if (indexX < 39 && indexY > 1)  // tring to prevent geting an index outside of an array
        {
            GameObject closeTile = mapGeneration.map[indexX + 1, indexY - 1];
            if (closeTile.GetComponent<TileIdentity>().busy == false)
            {
                closeTile.GetComponent<TileIdentity>().changeNear(generation);
            }
        }
    }

    /// <summary>
    /// using the number parameter take a chance of turning the tile into a forest, then do the same for nearby tiles
    /// but with lower chances
    public void changeNear(int gen)
    {
        int generation = gen;
        if (!busy && tileType != "forest" && Random.Range(1, 3+ generation) == 2) // if tile not busy and the chance was suceseful
        {
            meshRenderer = GetComponent<MeshRenderer>(); // gets the phisical proprieties of the tile

            meshRenderer.material = new Material(forest); // turns the tile into a darker color
            tileType = "forest"; // gives the tile the new type
            forestCopy = Instantiate(forestCenter, transform.position, Quaternion.identity); // spawns a forest
            mutateAround(gen+2); //try to grow forest in the nearby tiles but with lower chances
        }
    }

    /// <summary>
    /// Destroy the forest above this tile
    /// </summary>
    public void clearForest()
    {
        Destroy(forestCopy);
    }

    ///////^^^ Map generation^^^//////////
    //////////////////////////////////////
    ///////vvv Tile behaviour vvv/////////

    /// <summary>
    /// if tile colides with left pointer mark it as selected but with lower chances
    /// <summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "MousePointer") // if left pointer
        {
            selected = true;
        }
    }

    /// <summary>
    /// if tile colides with left pointer mark it as deselected but with lower chances
    /// <summary>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "MousePointer")
        {
            selected = false;
        }
    }

    /// <summary>
    /// will be used in future for selecting the main tile
    /// </summary>
    public void checkActionsOnSelected()
    { 
        if (selected)
        {
            if (tileType == "Overmind")
            {
                print("overmindTileHothered");
            }
        }
    }

    /// <summary>
    /// destroy the enemy mushrooms if thouse are present
    /// </summary>
    public void destroyEnemy()
    { 
        if (tileType == "EnemyForest")// if the tile is a captured forest return its original type
        {
            Destroy(enemyLink);
            tileType = "forest";
            busy = false;
            Instantiate(defensePheromones, transform.position, Quaternion.identity);
        }
        else if (tileType == "EnemyInfected")// if the tile is a captured field return its original type
        {
            Destroy(enemyLink);
            tileType = "";
            busy = false;
            Instantiate(defensePheromones, transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// destroy the player mushrooms on the tiles and return the tile its original proprieties
    /// </summary>
    public void destroyPlayer()
    {
        if (tileType == "PlForest" && playerLink) // if the tile is a captured forest return its original type
        {
            Destroy(playerLink.GetComponent<AdultMushroomsScript>().chargedIcon); // destroys the enrgy simbol
            Destroy(playerLink); // destroys the forest

            tileType = "forest";
            busy = false;

            Instantiate(defensePheromones, transform.position, Quaternion.identity);
        }
        else if (tileType == "PlInfected" && playerLink)// if the tile is a captured field return its original type
        {
            Destroy(playerLink.GetComponent<AdultMushroomsScript>().chargedIcon); // destroys the enrgy simbol
            Destroy(playerLink); // destroys the forest

            tileType = "";
            busy = false;

            Instantiate(defensePheromones, transform.position, Quaternion.identity);
        }
    }
}
