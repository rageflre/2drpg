using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{

    public float horizontalMovement
    {
        get;
        set;
    }

    public float verticalMovement
    {
        get;
        set;
    }

    public bool inventoryPressed
    {
        get;
        set;
    }

    public bool quitButtonPressed
    {
        get;
        set;
    }

    private void Update()
    {
        bool controllerConnected = false;

        //Loops though all connected joysticks
        foreach (string name in Input.GetJoystickNames())
        {
            //Wireless gamepad = nintendo pro controller/joycons
            if (name.Equals("Wireless Gamepad"))
            {
                controllerConnected = true;
            }
        }

        //Nintendo joycon connected
        if (controllerConnected)
        {
            horizontalMovement = Input.GetAxisRaw("LeftJoyStickHorizontalJoycon");
            verticalMovement = Input.GetAxisRaw("LeftJoyStickVerticalJoycon");
        }
        //No controller connected so use keyboard
        else
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetAxisRaw("Vertical");
            inventoryPressed = Input.GetKeyDown(KeyCode.I);
        }

        if (quitButtonPressed)
        {
            ReturnToMenu();
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainWorld");
    }

}