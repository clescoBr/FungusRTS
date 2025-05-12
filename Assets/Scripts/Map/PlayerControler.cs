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
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction scroll;
    private InputAction quit;
    private InputAction restart;
    private InputAction pause;


    [SerializeField] private float speed; // camera speed
    private Rigidbody rb;
    private Vector3 playerMovement; // curent player position
    private float scrolledAmount;
    private float rolled;
    private bool isPaused;
    [SerializeField] private GameObject pauseScreen;

    /// <summary>
    /// Action Map Setup
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput.currentActionMap.Enable();
        rolled = 0;

        scroll = playerInput.currentActionMap.FindAction("Scroll");
        quit = playerInput.currentActionMap.FindAction("Quit");
        restart = playerInput.currentActionMap.FindAction("Restart");
        pause = playerInput.currentActionMap.FindAction("Pause");
        scroll.started += scroled;
        scroll.canceled -= scroled;
        quit.performed += Quit_performed;
        restart.performed += Restart_performed;
        pause.performed += Pause_performed;
        isPaused = false;
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
    public void scroled(InputAction.CallbackContext obj)
    {
        rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y - 
            scroll.ReadValue<Vector2>().y / 10, rb.transform.position.z);
        scrolledAmount += scroll.ReadValue<Vector2>().y;
        rolled = scroll.ReadValue<Vector2>().y / 120f; // one scroll is 120 units, so I devide it by 120 to get it numbers of 1
        speed += rolled * -4; //prevents the speed from gooing to high
        if (speed > 42)
        {
            speed = 42;
        }
        else if (speed < 14)//prevents the speed from gooing to low
        {
            speed = 14;
        }

        if (gameObject.transform.position.y < 5) //prevents the camera from gooing to low
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 5, gameObject.transform.position.z);
        }
        else if (gameObject.transform.position.y > 89)//prevents the camera from gooing to high
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 89, gameObject.transform.position.z);

        }
    }

    /// <summary>
    /// quiting the game
    /// </summary>
    private void Quit_performed(InputAction.CallbackContext obj)
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false; //delete this when exporting
    }

    /// <summary>
    /// returning to the main menu
    /// </summary>
    private void Restart_performed(InputAction.CallbackContext obj)
    {    
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// pause / unpause the game
    /// </summary>
    private void Pause_performed(InputAction.CallbackContext obj)
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Updates camera's location
    /// </summary>
    void  FixedUpdate()
    {
        rb.velocity = new Vector3(playerMovement.x * speed , rb.velocity.y, playerMovement.z*speed ); // velocity y is kept the same
    }
}
