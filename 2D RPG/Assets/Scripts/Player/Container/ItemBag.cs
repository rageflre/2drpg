using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag
{
    Container container;

    public Container GetContainer()
    {
        return container;
    }

    GameObject parent;

    bool opened;

    public ItemBag(Container container, GameObject parent)
    {
        this.container = container;
        this.parent = parent;
    }

    public void Open()
    {
        //Checks if the console is opened
        if (Console.consoleOpened) return;
        //Sets inventory open to the oppesite
        opened = !opened;
        //Activates the inventory ui
        parent.SetActive(opened);
    }

    public void Add(Item item, int slot = -1)
    {
        container.Add(item, slot);
    }
    
    public void Remove(Item item, int slot = -1)
    {
        container.Remove(item, slot);
    }
}
