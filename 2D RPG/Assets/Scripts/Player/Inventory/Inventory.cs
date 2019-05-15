using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    [SerializeField]
    GameObject parent;
    [SerializeField]
    GameObject bagObject;
    [SerializeField]
    ItemBag inventory;
    [SerializeField]
    int slots = 30;

    public ItemBag GetInventory()
    {
        return inventory;
    }

    private void Awake()
    {
        instance = this;
        inventory = new ItemBag(new Container(slots, bagObject), parent);
    }

    private void Update()
    {
        if(GameManager.GetInput().inventoryPressed)
        {
            inventory.Open();
        }
    }
}
