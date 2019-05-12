using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryObject;
    [SerializeField]
    InputManager input;
    [SerializeField]
    bool inventoryOpen = false;

    void Start()
    {
        //Initialze the input manager from player
        input = GameObject.Find("Player").GetComponent<InputManager>();
    }

    void Update()
    {
        //Checks if the inventory button was pressed
        if(input.inventoryPressed)
        {
            //Sets inventory open to the oppesite
            inventoryOpen = !inventoryOpen;
            //Activates the inventory ui
            inventoryObject.SetActive(inventoryOpen);
        }
    }
}
