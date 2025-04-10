/*****************************************************************************
// File Name: PlayerControler
// Author: Cristian Lesco
// Creation Date: March 8th, 2025
//
// Description: Controls the camera movement
*****************************************************************************/


using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction scroll;
    private InputAction quit;


    [SerializeField] private float speed; // camera speed



    private Rigidbody rb;

    
    private Vector3 playerMovement; // curent player position
    /// <summary>
    /// Action Map Setup
    /// </summary>
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        playerInput.currentActionMap.Enable();


        scroll = playerInput.currentActionMap.FindAction("Scroll");
        quit = playerInput.currentActionMap.FindAction("Quit");
        quit.performed += Quit_performed;
        scroll.started += scroled;
        scroll.canceled -= scroled;
    }
    /// <summary>
    /// uses the new input system to move the camera
    /// </summary>
    private void OnMove(InputValue iValue)
    {
        Vector2 inputMovement = iValue.Get<Vector2>();
        playerMovement.x = inputMovement.x;
        playerMovement.z = inputMovement.y;
        
    }
    
    /// <summary>
    /// Makes the camera zoom in and out using the scroll button on the mouse
    /// </summary>
    /// <param name="obj"></param>
    public void scroled(InputAction.CallbackContext obj)
    {
        rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y - 
            scroll.ReadValue<Vector2>().y / 10, rb.transform.position.z);
    }

    
    private void Quit_performed(InputAction.CallbackContext obj)
    {
       
        Application.Quit();

    }


    /// <summary>
    /// Updates camera's location
    /// </summary>
    void  FixedUpdate()
    {
        rb.velocity = new Vector3(playerMovement.x * speed , rb.velocity.y, playerMovement.z*speed ); // velocity y is kept the sa
        

    }

}
