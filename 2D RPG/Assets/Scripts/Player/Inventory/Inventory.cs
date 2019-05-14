using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //The container for the inventory
    [SerializeField]
    Container inventory = new Container(30);

    Container equipment = new Container(10);
    //The game object for the inventory
    [SerializeField]
    GameObject inventoryObject;
    //The game object for the bag
    [SerializeField]
    GameObject bagObject;
    //Used to check if the user has his inventory open
    [SerializeField]
    bool inventoryOpen = false;

    InputManager input;

    private void Update()
    {
        openInventory();
    }

    private void Start()
    {
        //Grabs the input manager that is placed on the player
        input = GameObject.Find("Player").GetComponent<InputManager>();
    }

    void openInventory()
    {
        //Checks if the console is opened
        if (Console.consoleOpened) return;

        if (input.inventoryPressed)
        {
            //Refreshes the inventory
            Refresh();
            //Sets inventory open to the oppesite
            inventoryOpen = !inventoryOpen;
            //Activates the inventory ui
            inventoryObject.SetActive(inventoryOpen);
        }
    }

    public void Add(Item item, int slot = -1)
    {
        //Adds the item to the container
        inventory.add(item, slot);
        //Refreshes the inventory
        Refresh();
    }

    public void Remove(Item item, int slot = -1)
    {
        //Removes the item from the container
        inventory.remove(item, slot);
        //Refreshes the inventory
        Refresh();
    }

    void Refresh()
    {
        for (int index = 0; index < inventory.toArray().Length; index++)
        {
            Item item = inventory.toArray()[index];
            //Grabs the slot object
            GameObject slotObject = bagObject.transform.GetChild(index).gameObject;
            //Grabs the slot image object
            GameObject slotImage = slotObject.transform.GetChild(0).gameObject;
            //Grabs the slot text object
            GameObject slotStack = slotObject.transform.GetChild(1).gameObject;
            //Grabs the image component
            Image image = slotImage.GetComponent<Image>();
            //Grabs the text amount component
            Text stackAmount = slotStack.GetComponent<Text>();
            //Checks if the item isnt null
            if (item == null || slotObject == null || image == null || stackAmount == null)
            {
                //Disables the image
                slotImage.SetActive(false);
                //Disables the text
                slotStack.SetActive(false);
                continue;
            }
            int id = item.getId();
            //Grabs the path of the item sprite
            string spritePath = "Items/Sprites/sprite_" + id;
            //Grabs the item sprite
            Sprite sprite = Resources.Load<Sprite>(spritePath);
            //Sets the image in the item image
            image.sprite = sprite;
            //Sets the image parent active
            slotImage.SetActive(true);
            //Checks if the item is stackable
            if(item.getDefinition().stackable)
            {
                //Sets the amount of items the player has
                stackAmount.text = item.getAmount().ToString();
                //Actives the text
                slotStack.SetActive(true);
            }
        }
    }
}
