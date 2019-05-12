using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupInventory : MonoBehaviour
{
    [SerializeField]
    GameObject slotPrefab;
    [SerializeField]
    GameObject bagObject;
    [SerializeField]
    int totalSlots = 28;

    void Start()
    {
        //Initialize the bag game object
        bagObject = GameObject.Find("Bag");
        //Start x position of the slots
        float x = -116.9f;
        //Starts y position of the slots
        float y = 245.3f;
        //Create the slots
        for (int index = 0; index < totalSlots; index++, x += 78.5f)
        {
            //Every 4th slot it will reset the x value and decrease the y value
            if (index % 4 == 0 && index > 0)
            {
                x = -116.9f;
                y -= 77.8f;
            }
            //Initialize the slot object
            GameObject slot = Instantiate(slotPrefab);
            //Sets the name of the slot
            slot.name = "Slot " + index;
            //Set the parent object
            slot.transform.parent = bagObject.transform;
            //Set the position of the slot
            slot.transform.localPosition = new Vector3(x, y, 0);
        }
    }
}
