using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public Item(int id, int amount)
    {
        this.itemId = id;
        this.itemAmount = amount;
    }

    public Item(int id)
    {
        this.itemId = id;
        this.itemAmount = 1;
    }

    int itemId;

    public int GetId()
    {
        return itemId;
    }

    public void setId(int id)
    {
        itemId = id;
    }

    int itemAmount;

    public int GetAmount()
    {
        return itemAmount;
    }

    public void setAmount(int amount)
    {
        itemAmount = amount;
    }

    public ItemInfo getDefinition()
    {
        return ItemLoader.itemInfo.ItemInfo[itemId];
    }

    override
    public string ToString()
    {
        return string.Format("name: {0}\ndescription: {1}\nstackable: {2}", getDefinition().itemName, getDefinition().itemDescription, getDefinition().stackable);
    }
}
