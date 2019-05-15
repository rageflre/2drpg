using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem
{
    //The game object linked to this ground item
    private GameObject itemObject;

    public GameObject GetObject()
    {
        return itemObject;
    }

    //The item on the floor
    private Item item;

    public Item GetItem()
    {
        return item;
    }

    //The time before it shows up on the floor
    public int timer;

    public int GetTimer()
    {
        return timer;
    }

    public void SetTimer(int i)
    {
        timer = i;
    }

    public void DecreaseTimer()
    {
        timer--;
    }

    //The current stage of the item
    private string state;

    public string GetState()
    {
        return state;
    }

    public void SetState(string s)
    {
        state = s;
    }

    //If the item should respawn
    private bool respawns;

    public bool DoesRespawn()
    {
        return respawns;
    }

    public GroundItem(Item _item, GameObject _itemObject)
    {
        item = _item;
        itemObject = _itemObject;
        state = "PRIVATE";
        respawns = false;
    }

    public GroundItem(Item _item, GameObject _itemObject, bool _respawns)
    {
        item = _item;
        itemObject = _itemObject;
        state = "PUBLIC";
        timer = 5;
        respawns = _respawns;
    }
}
