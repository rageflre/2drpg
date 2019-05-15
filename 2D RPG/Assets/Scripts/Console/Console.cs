using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    [SerializeField]
    GameObject consoleObject;
    [SerializeField]
    InputField commandsObject;
    [SerializeField]
    static Text commandsDisplay;

    public static bool consoleOpened = false;

    void Start()
    {
        commandsObject = consoleObject.transform.GetChild(0).GetComponent<InputField>();

        commandsDisplay = consoleObject.transform.GetChild(1).GetComponent<Text>();
    }

    void Update()
    {
        OpenConsole();

        EnterCommand();
    }

    void OpenConsole()
    {
        //Checks if the user pressed the console key
        if (GameManager.GetInput().consolePressed)
        {
            //Sets the console opened to the oppesite
            consoleOpened = !consoleOpened;
            //Opens or closes the console
            consoleObject.SetActive(consoleOpened);
            //Focus on the input field
            commandsObject.ActivateInputField();
        }
    }

    void EnterCommand()
    {
        //Checks if the user pressed enter and has the console opened
        if(GameManager.GetInput().enterPressed && consoleOpened)
        {
            //Grabs the input from the input field
            string input = commandsObject.text;
            //Adds the command to the display commands text box
            AddTextToConsole(input);
            //Converts the input to lower case for handling the commands
            string[] args = input.ToLower().Split(' ');
            //Handles the commands
            Commands.HandleCommands(args);
            //Clears the input field's text
            commandsObject.text = "";
            //Focus on the input field
            commandsObject.ActivateInputField();
        }
    }

    public static void AddTextToConsole(string text)
    {
        commandsDisplay.text += text + "\n";
    }

}
