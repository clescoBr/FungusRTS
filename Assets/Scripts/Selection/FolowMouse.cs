/*****************************************************************************
// File Name: FolowMouse
// Author: Cristian Lesco
// Creation Date: March 18th, 2025
//
// Description: Tracks mouse clicks and the actions that would be taken
*****************************************************************************/
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse2D : MonoBehaviour
{
    public float zPosition = 10f; // Set this to the desired depth from the camera
    
    private bool mousePressed;
    [SerializeField] private GameObject leftPointerObject;

    private InputAction mousePress;
    private InputAction mousePosition;
    public InputAction mouseRightCl;
    public PlayerInput playerInput;
    public bool leftPointer; // is it the left clik idicator or the right click?

    public GameObject overlord; // original tyle
    public GameObject sporicite; // start infecting tile
    private GameObject abilityTarget; // tile selected by the right click
    public Material selectMaterial; // light green when hovered over tiles

    /// <summary>
    /// sets up the  variables
    /// </summary>
    private void Start()
    {
        mousePressed = false;
       // mouseRight = false;
        SetupActions();
    }

    /// <summary>
    /// sets up the actions 
    /// </summary>
    private void SetupActions()
    {
        playerInput.currentActionMap.Enable();  //Enable action map
        mousePress = playerInput.currentActionMap.FindAction("Press");
        mousePosition = playerInput.currentActionMap.FindAction("Position");
        mouseRightCl = playerInput.currentActionMap.FindAction("RightClick");
        mousePress.started += Mouse_pressed;
        mousePress.canceled += Mouse_press_canceled;
        mouseRightCl.started += Mouse_right_click;


    }

    /// <summary>
    /// stops all the actions to avoid errors
    /// </summary>
    private void OnDestroy()
    {
        mousePress.started -= Mouse_pressed;
        mousePress.canceled -= Mouse_press_canceled;
        mouseRightCl.started -= Mouse_right_click;

    }

    /// <summary>
    /// records left mouse press
    /// </summary>
    private void Mouse_pressed(InputAction.CallbackContext obj)
    {
        mousePressed = true;
    }

    /// <summary>
    /// records right mouse press
    /// </summary>
    private void Mouse_right_click(InputAction.CallbackContext obj) // if rightClicked
    {
        if (!leftPointer) // there are 2 objects with this script, one of them tracks left clicks another right clicks
        {
            if (overlord.GetComponent<OvermindController>().selected == true && // if overlord curently left clicked
                abilityTarget.GetComponent<TileIdentity>().tileType == "PlInfected" || 
                abilityTarget.GetComponent<TileIdentity>().tileType ==  "PlDeplited")  // if the target tile is captured
            {
                if( leftPointerObject.GetComponent<Economy>().PlNutrients >= 100) // if enough resources
                {
                    InfectTile(abilityTarget); // spawn Sporicite ( infect tile)
                    leftPointerObject.GetComponent<Economy>().PlNutrients -= 100; // substract the cost
                    leftPointerObject.GetComponent<Economy>().updateNutrients(); // updates the label on the camera
                }
            }
        }    
    }

    /// <summary>
    /// records left mouse canceled
    /// </summary>
    private void Mouse_press_canceled(InputAction.CallbackContext obj) // left release
    {
        mousePressed = false;
    } 

    /// <summary>
    /// updates the location of the mouse pointers to the mouse itself
    /// </summary>
    private void FixedUpdate()
    {
        if (leftPointer) // if left cursor
        {
            if (mousePressed) //         pressed
            {
                Ray ray = Camera.main.ScreenPointToRay(mousePosition.ReadValue<Vector2>()); // read the location

                // Find the point where the ray intersects with the plane at Y = 0
                float distanceToPlane = -ray.origin.y / ray.direction.y; // Calculate the intersection point along the ray

                Vector3 targetPosition = ray.GetPoint(distanceToPlane);

                // Only update x and z, keep y fixed at 0
                transform.position = new Vector3(targetPosition.x, 0f, targetPosition.z);
            }
        }
        else // if it is the right cursor always update its location to the mouse
        {                  
                Ray ray = Camera.main.ScreenPointToRay(mousePosition.ReadValue<Vector2>());

                // Find the point where the ray intersects with the plane at Y = 0
                float distanceToPlane = -ray.origin.y / ray.direction.y; // Calculate the intersection point along the ray

                Vector3 targetPosition = ray.GetPoint(distanceToPlane);

                // Only update x and z, keep y fixed at 0
                transform.position = new Vector3(targetPosition.x, 0f, targetPosition.z);            
        }
    }

    /// <summary>
    /// infect a tile, the function is called by Mouse_Right_Click, when overlord is selected and a tile is rightClick
    /// </summary>
    /// <param name="tile"> the tile that was last colided with the right click</param>
    private void InfectTile(GameObject tile)
    {      
        tile.GetComponent<TileIdentity>().tileType = "Infected"; // set the tile type in data
        tile.GetComponent<TileIdentity>().busy = true; // set the tile as busy so nothing else can ocupy it
        GameObject spawn = Instantiate(sporicite, new Vector3 (tile.transform.position.x, 
            tile.transform.position.y+ 1f, tile.transform.position.z), Quaternion.identity); //spawn mycelium, the object that initiates the capturing process
        spawn.GetComponent<SporiciteControler>().setHostTile(tile);
    }

    /// <summary>
    ///          when the mouse pointer touches something highlighted it
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (!leftPointer && collision.gameObject.name == "Tile(Clone)") // if right pointer on a tile
        {
            collision.gameObject.GetComponent<MeshRenderer>().material = new Material(selectMaterial); // higlight tile
            abilityTarget = collision.gameObject; // store the tile          
        }
    }

    /// <summary>
    ///     /// when the mouse pointer exits a tile return it to original properties
    /// </summary>
    private void OnCollisionExit(Collision collision)
    {
        if (!leftPointer && collision.gameObject.name == "Tile(Clone)")// if right and stop coliding with a tile
        {
            collision.gameObject.GetComponent<MeshRenderer>().material = collision.gameObject.GetComponent<TileIdentity>().originalCl;
            // return tile to original color
        }
    }
}