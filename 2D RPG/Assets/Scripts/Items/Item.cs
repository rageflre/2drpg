using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]
    int itemId;
    [SerializeField]
    string itemName;
    [SerializeField]
    string itemDescription;
    [SerializeField]
    string itemSprite;

    public int getId()
    {
        return itemId;
    }

    public void setId(int id)
    {
        itemId = id;
    }

    public string getName()
    {
        return itemName;
    }

    public void setName(string name)
    {
        itemName = name;
    }

    public string getDescription()
    {
        return itemDescription;
    }

    public void setDescription(string desc)
    {
        itemDescription = desc;
    }

    public string getSprite()
    {
        return itemSprite;
    }

    public void setSprite(string sprite)
    {
        itemSprite = sprite;
    }
}
