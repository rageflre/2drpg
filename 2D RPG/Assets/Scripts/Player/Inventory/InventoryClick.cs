using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryClick : MonoBehaviour
{
    [SerializeField]
    int slot;

    private void Start()
    {
        string number = transform.parent.name.Replace("Slot ", "");
        print("number: " + number);
        slot = int.Parse(number);
    }
    public void onClick()
    {
        print("Clicked the inventory slot " + slot);
    }
}
